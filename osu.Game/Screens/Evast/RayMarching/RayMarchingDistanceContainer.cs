using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Input.Events;
using System;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RayMarchingDistanceContainer : Container
    {
        private readonly Container cursorContainer;
        private readonly Container objectsContainer;
        private readonly Container cursor;
        private readonly Container resizableContainer;

        public RayMarchingDistanceContainer()
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
                cursorContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        cursor = new Container
                        {
                            Origin = Anchor.Centre,
                            Children = new Drawable[]
                            {
                                new Circle
                                {
                                    Origin = Anchor.Centre,
                                    Anchor = Anchor.Centre,
                                    Size = new Vector2(10)
                                },
                                resizableContainer = new Container
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    Child = new CircularContainer
                                    {
                                        Masking = true,
                                        RelativeSizeAxes = Axes.Both,
                                        BorderColour = Color4.White,
                                        BorderThickness = 3,
                                        Child = new Box
                                        {
                                            RelativeSizeAxes = Axes.Both,
                                            Colour = Color4.White.Opacity(0.5f)
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                objectsContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both
                }
            };

            populateObjects(8);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            cursor.FadeTo(IsHovered ? 1 : 0);
        }

        protected override bool OnHover(HoverEvent e)
        {
            base.OnHover(e);
            cursor.FadeIn(100);
            return true;
        }

        protected override void OnHoverLost(HoverLostEvent e)
        {
            base.OnHoverLost(e);
            cursor.FadeOut();
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            base.OnMouseMove(e);
            cursor.Position = cursorContainer.ToLocalSpace(e.CurrentState.Mouse.Position);

            double min = 10000;

            foreach (var s in objectsContainer)
            {
                if (s is Circle c)
                {
                    min = Math.Min(min, RayMarchingExtensions.DistanceToCircle(cursor.Position, c));
                }
                else if (s is Box b)
                {
                    min = Math.Min(min, RayMarchingExtensions.DistanceToSquare(cursor.Position, b));
                }
            }

            resizableContainer.Size = new Vector2((float)min * 2);

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
    }
}
