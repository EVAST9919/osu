using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Input.Events;
using System;
using osu.Framework.Utils;
using osu.Framework.Bindables;
using osuTK.Input;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RayMarchingPlayerContainer : Container
    {
        private const int width = 1000;
        private const int height = 500;
        private const float fov = 0.8f;
        private const float view_distance = 800;

        private readonly int rayCount;

        private readonly Container objectsContainer;
        private readonly Container raysContainer;
        private readonly Circle player;

        private readonly Bindable<Vector2> viewTarget = new Bindable<Vector2>(new Vector2(width - 100, height / 2f));
        public readonly Bindable<float[]> Rays = new Bindable<float[]>();

        private int forwardDirection;
        private int sideDirection;
        private int rotationDirection;

        public RayMarchingPlayerContainer(int rayCount)
        {
            this.rayCount = rayCount;

            Size = new Vector2(width, height);
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
                raysContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                },
                player = new Circle
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(15),
                    Position = new Vector2(100, height / 2)
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
            raysContainer.Clear();

            var playerAngle = RayMarchingExtensions.RayAngle(player.Position, viewTarget.Value);
            var initialAngle = playerAngle - fov / 2;
            var angleOffset = fov / rayCount;

            float[] newRays = new float[rayCount];

            for (int i = 0; i < rayCount; i++)
            {
                var rayAngle = initialAngle + i * angleOffset;

                newRays[i] = (float)Math.Min(castRay(rayAngle), width);

                if (i % 4 == 0)
                    raysContainer.Add(new Line(player.Position, rayAngle, newRays[i]));
            }

            Rays.Value = newRays;
        }

        protected override void Update()
        {
            base.Update();

            if (forwardDirection != 0)
            {
                var angle = RayMarchingExtensions.RayAngle(player.Position, viewTarget.Value) + (forwardDirection == 1 ? 0 : Math.PI);
                var distanceDiff = Time.Elapsed / 10;

                player.Position = RayMarchingExtensions.PositionOnASphere(player.Position, distanceDiff, angle);
                viewTarget.Value = RayMarchingExtensions.PositionOnASphere(viewTarget.Value, distanceDiff, angle);
            }

            if (sideDirection != 0)
            {
                var angle = RayMarchingExtensions.RayAngle(player.Position, viewTarget.Value) + (sideDirection * Math.PI / 2);
                var distanceDiff = Time.Elapsed / 10;

                player.Position = RayMarchingExtensions.PositionOnASphere(player.Position, distanceDiff, angle);
                viewTarget.Value = RayMarchingExtensions.PositionOnASphere(viewTarget.Value, distanceDiff, angle);
            }

            if (rotationDirection != 0)
            {
                var angle = RayMarchingExtensions.RayAngle(player.Position, viewTarget.Value);
                angle += rotationDirection * Time.Elapsed / 700;

                viewTarget.Value = RayMarchingExtensions.PositionOnASphere(player.Position, view_distance, angle);
            }
        }

        private double castRay(double angle)
        {
            var sourcePosition = player.Position;
            double sum = 0;
            double closest = getClosest(player.Position);

            while (!Precision.AlmostEquals(closest, 0, 0.1) && closest < width)
            {
                sum += closest;
                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, angle);
                closest = getClosest(sourcePosition);
            }

            return sum;
        }

        private double getClosest(Vector2 input)
        {
            double min = width;

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

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.W:
                        forwardDirection = 1;
                        return true;

                    case Key.S:
                        forwardDirection = -1;
                        return true;

                    case Key.A:
                        sideDirection = -1;
                        return true;

                    case Key.D:
                        sideDirection = 1;
                        return true;

                    case Key.Q:
                        rotationDirection = -1;
                        return true;

                    case Key.E:
                        rotationDirection = 1;
                        return true;
                }
            }

            return false;
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            base.OnKeyUp(e);

            switch (e.Key)
            {
                case Key.W:
                case Key.S:
                    forwardDirection = 0;
                    return;

                case Key.A:
                case Key.D:
                    sideDirection = 0;
                    return;

                case Key.Q:
                case Key.E:
                    rotationDirection = 0;
                    return;
            }
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
                    Position = new Vector2(random.Next(50, width - 50), random.Next(50, height - 50)),
                    Alpha = 0.3f
                };
            }
            else
            {
                return new Box
                {
                    Origin = Anchor.Centre,
                    Size = new Vector2(random.Next(20, 100)),
                    Position = new Vector2(random.Next(100, width - 100), random.Next(100, height - 100)),
                    Alpha = 0.3f
                };
            }
        }

        private class Line : Box
        {
            public Line(Vector2 source, double angle, float distance)
            {
                Origin = Anchor.CentreLeft;
                Width = distance;
                Height = 0.1f;
                Position = source;
                Rotation = (float)(angle * 180 / Math.PI);
                EdgeSmoothness = Vector2.One;
            }
        }
    }
}
