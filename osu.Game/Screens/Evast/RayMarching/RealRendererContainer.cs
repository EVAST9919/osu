using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osuTK;
using osu.Framework.Utils;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Bindables;
using System;
using System.Collections.Generic;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Framework.Input.Events;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RealRendererContainer : CompositeDrawable
    {
        private const float fov = 1f;
        private const float view_multiplier = 0.25f;
        private const int ray_count = 75;
        private const int camera_distance = 300;
        private const float max_visible_distance = 800;

        private double phi;
        private double theta;

        private readonly Bindable<Vector3> cameraPosition = new Bindable<Vector3>();
        private readonly Bindable<Vector3> cameraTarget = new Bindable<Vector3>(new Vector3(500, 100, 100));

        private readonly IEnumerable<Sphere> spheres;

        private readonly TopView top;
        private readonly SideView side;
        private readonly PerspectiveView perspective;
        private readonly OsuSpriteText phiText;
        private readonly OsuSpriteText thetaText;
        private readonly OsuSpriteText cameraText;

        public RealRendererContainer()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChild = new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(GridSizeMode.Relative, 0.5f),
                    new Dimension()
                },
                ColumnDimensions = new[]
                {
                    new Dimension(GridSizeMode.Relative, 0.5f),
                    new Dimension()
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                top = new TopView(),
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Y,
                                    Width = 1,
                                    Anchor = Anchor.CentreRight,
                                    Origin = Anchor.CentreRight,
                                    EdgeSmoothness = Vector2.One,
                                    Colour = Color4.White
                                }
                            }
                        },
                        side = new SideView()
                    },
                    new Drawable[]
                    {
                        new Container
                        {
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black
                                },
                                new FillFlowContainer
                                {
                                    Margin = new MarginPadding(5),
                                    AutoSizeAxes = Axes.Both,
                                    Direction = FillDirection.Vertical,
                                    Spacing = new Vector2(0, 5),
                                    Children = new[]
                                    {
                                        phiText = new OsuSpriteText
                                        {
                                            Font = OsuFont.GetFont()
                                        },
                                        thetaText = new OsuSpriteText
                                        {
                                            Font = OsuFont.GetFont()
                                        },
                                        cameraText = new OsuSpriteText
                                        {
                                            Font = OsuFont.GetFont()
                                        }
                                    }
                                }
                            }
                        },
                        perspective = new PerspectiveView(ray_count)
                    }
                }
            };

            perspective.DragInvoked += onDrag;

            spheres = new[]
            {
                new Sphere(new Vector3(500, 100, 50), 50),
                new Sphere(new Vector3(500, 200, 100), 50),
                new Sphere(new Vector3(500, 100, 150), 50)
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateSideViews();
            updateRender();
        }

        private void updateSideViews()
        {
            foreach (var s in spheres)
            {
                top.Add(new Circle
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Size = new Vector2(s.Radius * 2 * view_multiplier),
                    Position = new Vector2(s.Position.X * view_multiplier, -s.Position.Z * view_multiplier),
                    Alpha = 0.5f
                });

                side.Add(new Circle
                {
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Size = new Vector2(s.Radius * 2 * view_multiplier),
                    Position = new Vector2(s.Position.X * view_multiplier, -s.Position.Y * view_multiplier),
                    Alpha = 0.5f
                });
            }
        }

        private void onDrag(Vector2 delta)
        {
            phi += delta.X / 500;
            theta += delta.Y / 500;
            updateRender();
        }

        private void updateRender()
        {
            cameraPosition.Value = RayMarchingExtensions.PositionOnASphere(cameraTarget.Value, camera_distance, theta, phi + Math.PI);

            var offset = fov / ray_count;

            var initialPhi = phi - fov / 2;
            var initialTheta = theta + fov / 2;

            for (int i = 0; i < ray_count; i++)
            {
                var rayPhi = initialPhi + i * offset;

                for (int j = 0; j < ray_count; j++)
                {
                    var rayTheta = initialTheta - j * offset;
                    var distance = castRay(rayTheta, rayPhi);

                    perspective.Pixels[i, j].Colour = distance.HasValue ? Color4.White.Opacity(1 - Interpolation.ValueAt((float)distance.Value, 0f, 1f, camera_distance - 100, camera_distance + 100)) : Color4.Black;
                    //perspective.Pixels[i, j].Colour = distance.HasValue ? Color4.White : Color4.Black;
                }
            }

            top.CameraPosition = new Vector2(cameraPosition.Value.X * view_multiplier, -cameraPosition.Value.Z * view_multiplier);
            side.CameraPosition = new Vector2(cameraPosition.Value.X * view_multiplier, -cameraPosition.Value.Y * view_multiplier);
            top.ViewAngle = (float)phi;
            side.ViewAngle = (float)theta;

            phiText.Text = $"phi: {phi: 0.00}";
            thetaText.Text = $"theta: {theta: 0.00}";
            cameraText.Text = $"camera: ({cameraPosition.Value.X:0.00}, {cameraPosition.Value.Y:0.00}, {cameraPosition.Value.Z:0.00})";
        }

        private double? castRay(double theta, double phi)
        {
            var sourcePosition = cameraPosition.Value;
            double closest = getClosest(sourcePosition);
            double sum = closest;

            while (!Precision.AlmostEquals(closest, 0, 0.1) && closest < max_visible_distance)
            {
                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, theta, phi);
                closest = getClosest(sourcePosition);
                sum += closest;
            }

            if (closest > max_visible_distance)
                return null;

            return sum;
        }

        private double getClosest(Vector3 input)
        {
            double min = 10000;

            foreach (var s in spheres)
                min = Math.Min(min, RayMarchingExtensions.DistanceToSphere(input, s.Position, s.Radius));

            return min;
        }

        private class PerspectiveView : CompositeDrawable
        {
            public event Action<Vector2> DragInvoked;

            public readonly Box[,] Pixels;

            public PerspectiveView(int rayCount)
            {
                Pixels = new Box[rayCount, rayCount];

                RelativeSizeAxes = Axes.Both;

                AddInternal(new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                });

                for (int i = 0; i < rayCount; i++)
                {
                    for (int j = 0; j < rayCount; j++)
                    {
                        AddInternal(Pixels[i, j] = new Box
                        {
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1f / rayCount),
                            Position = new Vector2(1f / rayCount * i, 1f / rayCount * j)
                        });
                    }
                }
            }

            protected override bool OnDragStart(DragStartEvent e) => true;

            protected override void OnDrag(DragEvent e)
            {
                base.OnDrag(e);
                DragInvoked?.Invoke(e.Delta);
            }
        }

        private abstract class Preview : CompositeDrawable
        {
            private Vector2 cameraPosition;

            public Vector2 CameraPosition
            {
                get => cameraPosition;
                set
                {
                    cameraPosition = value;
                    topLine.Position = value;
                    bottomLine.Position = value;
                }
            }

            public float ViewAngle
            {
                set
                {
                    topLine.Angle = -(value - fov / 2);
                    bottomLine.Angle = -(value + fov / 2);
                }
            }

            protected readonly Container AxesLayer;
            private readonly Container content;
            private readonly Line topLine;
            private readonly Line bottomLine;

            protected Preview(string viewName)
            {
                RelativeSizeAxes = Axes.Both;
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black
                    },
                    AxesLayer = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    content = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        Children = new[]
                        {
                            topLine = new Line
                            {
                                Width = 1000,
                                Anchor = Anchor.Centre
                            },
                            bottomLine = new Line
                            {
                                Width = 1000,
                                Anchor = Anchor.Centre
                            }
                        }
                    },
                    new OsuSpriteText
                    {
                        Margin = new MarginPadding(5),
                        Text = $"{viewName} view",
                        Colour = Color4.White,
                        Font = OsuFont.GetFont()
                    }
                };
            }

            public void Add(Drawable d) => content.Add(d);
        }

        private class TopView : Preview
        {
            public TopView()
                : base("Top")
            {
                AxesLayer.AddRange(new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Color4.Red,
                        RelativeSizeAxes = Axes.X,
                        Height = 0.1f,
                        EdgeSmoothness = Vector2.One
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.BottomRight,
                        Margin = new MarginPadding
                        {
                            Bottom = 5,
                            Right = 5
                        },
                        Text = "X",
                        Colour = Color4.Red,
                        Font = OsuFont.GetFont()
                    },
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Color4.Blue,
                        RelativeSizeAxes = Axes.Y,
                        Width = 0.1f,
                        EdgeSmoothness = Vector2.One
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopRight,
                        Margin = new MarginPadding
                        {
                            Top = 5,
                            Right = 5
                        },
                        Text = "Z",
                        Colour = Color4.Blue,
                        Font = OsuFont.GetFont()
                    }
                });
            }
        }

        private class SideView : Preview
        {
            public SideView()
                : base("Side")
            {
                AxesLayer.AddRange(new Drawable[]
                {
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Color4.Green,
                        RelativeSizeAxes = Axes.Y,
                        Width = 0.1f,
                        EdgeSmoothness = Vector2.One
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopRight,
                        Margin = new MarginPadding
                        {
                            Top = 5,
                            Right = 5
                        },
                        Text = "Y",
                        Colour = Color4.Green,
                        Font = OsuFont.GetFont()
                    },
                    new Box
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Color4.Red,
                        RelativeSizeAxes = Axes.X,
                        Height = 0.1f,
                        EdgeSmoothness = Vector2.One
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.BottomRight,
                        Margin = new MarginPadding
                        {
                            Bottom = 5,
                            Right = 5
                        },
                        Text = "X",
                        Colour = Color4.Red,
                        Font = OsuFont.GetFont()
                    }
                });
            }
        }

        private class Sphere
        {
            public Vector3 Position { get; set; }

            public float Radius { get; set; }

            public Sphere(Vector3 position, float radius)
            {
                Position = position;
                Radius = radius;
            }
        }

        private class Line : Box
        {
            private float angle;

            public float Angle
            {
                get => angle;
                set
                {
                    angle = value;
                    Rotation = (float)(value * 180 / Math.PI);
                }
            }

            public Line()
            {
                Origin = Anchor.CentreLeft;
                Height = 0.1f;
                EdgeSmoothness = Vector2.One;
            }
        }
    }
}
