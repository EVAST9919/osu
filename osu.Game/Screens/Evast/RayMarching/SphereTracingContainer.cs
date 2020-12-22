using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Input.Events;
using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class SphereTracingContainer : Container
    {
        private const int max_iterations = 40;

        private readonly Pin source;
        private readonly Pin target;
        private readonly Container objectsContainer;
        private readonly Container<SphereTracer> spheresContainer;

        public SphereTracingContainer()
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
                spheresContainer = new Container<SphereTracer>
                {
                    RelativeSizeAxes = Axes.Both
                },
                source = new Pin
                {
                    Position = new Vector2(100, 250)
                },
                target = new Pin
                {
                    Position = new Vector2(900, 250)
                }
            };

            source.OnMove += updateRay;
            target.OnMove += updateRay;

            populateObjects(8);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateRay();
        }

        private void updateRay()
        {
            spheresContainer.Clear();

            var currentIteration = 0;

            var sourcePosition = source.Position;
            var angle = RayMarchingExtensions.RayAngle(source.Position, target.Position);
            double closest = getClosest(source.Position);

            while (!Precision.AlmostEquals(closest, 0, 0.1) && currentIteration < max_iterations)
            {
                spheresContainer.Add(new SphereTracer
                {
                    Size = new Vector2((float)closest * 2),
                    Position = sourcePosition
                });

                sourcePosition = RayMarchingExtensions.PositionOnASphere(sourcePosition, closest, angle);
                closest = getClosest(sourcePosition);

                currentIteration++;
            }
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

        private class Pin : Circle
        {
            public event Action OnMove;

            public Pin()
            {
                Origin = Anchor.Centre;
                Size = new Vector2(15);
                Colour = Color4.Red;
            }

            protected override bool OnDragStart(DragStartEvent e) => true;

            protected override void OnDrag(DragEvent e)
            {
                base.OnDrag(e);

                Position += e.Delta;
                OnMove?.Invoke();
            }
        }

        private class SphereTracer : CircularContainer
        {
            public SphereTracer()
            {
                Masking = true;
                BorderColour = Color4.White;
                Origin = Anchor.Centre;
                BorderThickness = 3;
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White.Opacity(0.4f)
                };
            }
        }
    }
}
