using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osuTK;
using osu.Framework.Utils;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Bindables;
using System;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RealRendererContainer : CompositeDrawable
    {
        private const int ray_count = 200;
        private const int sphere_radius = 50;
        private const int camera_distance = 200;

        private readonly Bindable<Vector3> cameraPosition = new Bindable<Vector3>(new Vector3(300, 100, 100));
        private readonly Bindable<Vector3> spherePosition = new Bindable<Vector3>(new Vector3(500, 100, 100)); // also target
        private readonly Bindable<Vector3> sphere2Position = new Bindable<Vector3>(new Vector3(500, 100, 150));
        private readonly Bindable<Vector3> sphere3Position = new Bindable<Vector3>(new Vector3(500, 100, 200));

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
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Red
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Green
                        }
                    },
                    new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Blue
                        },
                        perspective = new PerspectiveView(ray_count)
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            cameraPosition.BindValueChanged(_ => updateView(), true);
        }

        private void updateView()
        {
            var offset = 1f / ray_count;
            var initialXAngle = RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, cameraPosition.Value.Z), new Vector2(spherePosition.Value.X, spherePosition.Value.Z)) - 0.5f;
            //var initialYAngle = RayMarchingExtensions.RayAngle(new Vector2(cameraPosition.Value.X, cameraPosition.Value.Y), new Vector2(spherePosition.Value.X, spherePosition.Value.Y)) / 2 - 0.25f;

            for (int i = 0; i < ray_count; i++)
            {
                var yAngle = Math.PI / 2 - 0.5f + i * offset;// initialYAngle + i * yOffset;

                for (int j = 0; j < ray_count; j++)
                {
                    var xAngle = initialXAngle + j * offset;
                    var distance = castRay(xAngle, yAngle);

                    perspective.Pixels[i, j].Colour = distance.HasValue ? Color4.White.Opacity(1 - Interpolation.ValueAt((float)distance.Value, 0f, 1f, camera_distance - sphere_radius * 2, camera_distance + sphere_radius * 2)) : Color4.Black;
                }
            }
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

            min = Math.Min(min, RayMarchingExtensions.DistanceToSphere(input, spherePosition.Value, sphere_radius));
            min = Math.Min(min, RayMarchingExtensions.DistanceToSphere(input, sphere2Position.Value, 40));
            min = Math.Min(min, RayMarchingExtensions.DistanceToSphere(input, sphere3Position.Value, 35));

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
    }
}
