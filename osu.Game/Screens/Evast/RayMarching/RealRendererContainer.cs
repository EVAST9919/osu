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
        private const int sphere_radius = 60;

        private readonly Bindable<Vector3> cameraPosition = new Bindable<Vector3>(new Vector3(0, 100, 100));
        private readonly Bindable<Vector3> spherePosition = new Bindable<Vector3>(new Vector3(200, 100, 100)); // also target

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

                    perspective.Pixels[i, j].Colour = distance.HasValue ? Color4.White.Opacity(1 - (float)distance.Value / 500) : Color4.Black;
                }
            }
        }

        private double? castRay(double xAngle, double yAngle)
        {
            var sourcePosition = cameraPosition.Value;
            double sum = 0;
            double closest = RayMarchingExtensions.DistanceToSphere(sourcePosition, spherePosition.Value, sphere_radius);

            while (!Precision.AlmostEquals(closest, 0, 0.1) && closest < 500)
            {
                sum += closest;
                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, xAngle, yAngle);
                closest = RayMarchingExtensions.DistanceToSphere(sourcePosition, spherePosition.Value, sphere_radius);
            }

            if (closest > 500)
                return null;

            return sum;
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
