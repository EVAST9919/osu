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

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RealRendererContainer : CompositeDrawable
    {
        private const float fov = 1f;
        private const float view_multiplier = 0.25f;
        private const int ray_count = 100;
        private const int camera_distance = 200;

        private readonly Bindable<Vector3> cameraPosition = new Bindable<Vector3>(new Vector3(300, 100, 100));
        private readonly Bindable<Vector3> cameraTarget = new Bindable<Vector3>(new Vector3(500, 100, 100));

        private readonly IEnumerable<Sphere> spheres;

        private readonly TopView top;
        private readonly SideView side;
        private readonly PerspectiveView perspective;

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
                            RelativeSizeAxes = Axes.Both
                        },
                        perspective = new PerspectiveView(ray_count)
                    }
                }
            };

            spheres = new[]
            {
                new Sphere(new Vector3(500, 100, 50), 50),
                new Sphere(new Vector3(500, 200, 150), 40),
                new Sphere(new Vector3(550, 100, 200), 35)
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateSideViews();
            cameraPosition.BindValueChanged(_ => cameraPositionChanged(), true);
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

        private void cameraPositionChanged()
        {
            var offset = 1f / ray_count;
            var initialXAngle = RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, cameraPosition.Value.Z), new Vector2(cameraTarget.Value.X, cameraTarget.Value.Z)) + 0.5f;
            //var initialYAngle = RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, cameraPosition.Value.Y), new Vector2(cameraTarget.Value.X, cameraTarget.Value.Y)) / 2 - 0.25f;

            for (int i = 0; i < ray_count; i++)
            {
                var yAngle = Math.PI / 2 - 0.5f + i * offset;// initialYAngle + i * yOffset;

                for (int j = 0; j < ray_count; j++)
                {
                    var xAngle = initialXAngle - j * offset;
                    var distance = castRay(xAngle, yAngle);

                    perspective.Pixels[i, j].Colour = distance.HasValue ? Color4.White.Opacity(1 - Interpolation.ValueAt((float)distance.Value, 0f, 1f, camera_distance - 100, camera_distance + 100)) : Color4.Black;
                }
            }

            top.CameraPosition = new Vector2(cameraPosition.Value.X * view_multiplier, -cameraPosition.Value.Z * view_multiplier);
            side.CameraPosition = new Vector2(cameraPosition.Value.X * view_multiplier, -cameraPosition.Value.Y * view_multiplier);
            top.ViewAngle = (float)RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, cameraPosition.Value.Z), new Vector2(cameraTarget.Value.X, cameraTarget.Value.Z));
            side.ViewAngle = (float)RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, -cameraPosition.Value.Y), new Vector2(cameraTarget.Value.X, -cameraTarget.Value.Y));
        }

        private double? castRay(double xAngle, double yAngle)
        {
            var sourcePosition = cameraPosition.Value;
            double sum = 0;
            double closest = getClosest(sourcePosition);

            while (!Precision.AlmostEquals(closest, 0, 0.1) && closest < 500)
            {
                sum += closest;
                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, xAngle, yAngle);
                closest = getClosest(sourcePosition);
            }

            if (closest > 500)
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

            private float viewAngle;

            public float ViewAngle
            {
                get => viewAngle;
                set
                {
                    viewAngle = value;
                    topLine.Angle = value - fov / 2;
                    bottomLine.Angle = value + fov / 2;
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
            public Vector3 Position { get; private set; }

            public float Radius { get; private set; }

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
