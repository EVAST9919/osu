using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Input.Events;
using System;
using osu.Framework.Utils;
using osu.Framework.Bindables;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RayMarchingPlayerContainer : Container
    {
        private const float fov = 1.4f;
        private const int ray_count = 150;

        private readonly Container objectsContainer;
        private readonly Container circlesContainer;
        private readonly Container fovContainer;
        private readonly Circle player;

        private readonly Bindable<Vector2> viewTarget = new Bindable<Vector2>(Vector2.Zero);

        public RayMarchingPlayerContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(1000, 500);
            Masking = true;
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                objectsContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
                fovContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
                circlesContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
                player = new Circle
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(15),
                    Position = new Vector2(100, 250)
                },
            };

            populateObjects(10);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            viewTarget.BindValueChanged(_ => updateRays(), true);
        }

        private void updateRays()
        {
            circlesContainer.Clear();
            fovContainer.Clear();

            var playerAngle = RayMarchingExtensions.RayAngle(player.Position, viewTarget.Value);
            var initialAngle = playerAngle - fov / 2;
            var angleOffset = fov / ray_count;

            fovContainer.AddRange(new Drawable[]
            {
                new Line(player.Position, initialAngle, 2000),
                new Line(player.Position, initialAngle + fov, 2000)
            });

            for (int i = 0; i < ray_count; i++)
            {
                var rayAngle = initialAngle + i * angleOffset;

                var distance = castRay(rayAngle);

                circlesContainer.AddRange(new Drawable[]
                {
                    new Line(player.Position, rayAngle, Math.Min((float)distance, 2000))
                    {
                        Colour = Color4.Red
                    }
                });
            }
        }

        private double castRay(double angle)
        {
            var sourcePosition = player.Position;
            double sum = 0;
            double closest = getClosest(player.Position);

            while (!Precision.AlmostEquals(closest, 0, 0.1) && closest < 1000)
            {
                sum += closest;
                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, angle);
                closest = getClosest(sourcePosition);
            }

            return sum;
        }

        private double getClosest(Vector2 input)
        {
            double min = 10000;

            foreach (var s in objectsContainer)
            {
                if (s is Circle c)
                {
                    min = Math.Min(min, RayMarchingExtensions.DistanceToCircle(input, c));
                }
                else if (s is Box b)
                {
                    min = Math.Min(min, RayMarchingExtensions.DistanceToSquare(input, b));
                }
            }

            return min;
        }

        protected override bool OnHover(HoverEvent e) => true;

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            base.OnMouseMove(e);
            viewTarget.Value = ToLocalSpace(e.CurrentState.Mouse.Position);
            return true;
        }

        private void populateObjects(int count)
        {
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < count; i++)
                objectsContainer.Add(createNewObject(random));
        }

        private Drawable createNewObject(Random random)
        {
            if (random.NextDouble() > 0.5)
            {
                return new Circle
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(random.Next(20, 100)),
                    Position = new Vector2(random.Next(50, 950), random.Next(50, 450))
                };
            }
            else
            {
                return new Box
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(random.Next(20, 100)),
                    Position = new Vector2(random.Next(100, 900), random.Next(100, 400))
                };
            }
        }

        private class Line : Box
        {
            public Line(Vector2 source, double angle, float distance)
            {
                Origin = Anchor.CentreLeft;
                Width = distance;
                Height = 1;
                Position = source;
                Rotation = (float)(angle * 180 / Math.PI);
                EdgeSmoothness = Vector2.One;
            }
        }
    }
}
