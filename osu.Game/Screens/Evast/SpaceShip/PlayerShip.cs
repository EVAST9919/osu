using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osuTK.Input;
using System;
using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class PlayerShip : Ship
    {
        private const double base_speed = 1.0 / 2048;

        public Action Shoot;
        public Action StopShoot;

        private int horizontalDirection;
        private int verticalDirection;

        public PlayerShip()
        {
            Y = 0.5f;
            X = 0.1f;
            Add(new Engine
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreRight,
                X = 4,
            });
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        verticalDirection--;
                        return true;

                    case Key.Down:
                        verticalDirection++;
                        return true;

                    case Key.Left:
                        horizontalDirection--;
                        return true;

                    case Key.Right:
                        horizontalDirection++;
                        return true;

                    case Key.Space:
                        Shoot?.Invoke();
                        return true;
                };
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    verticalDirection++;
                    return;

                case Key.Down:
                    verticalDirection--;
                    return;

                case Key.Left:
                    horizontalDirection++;
                    return;

                case Key.Right:
                    horizontalDirection--;
                    return;

                case Key.Space:
                    StopShoot?.Invoke();
                    return;
            };

            base.OnKeyUp(e);
        }

        protected override void Update()
        {
            base.Update();

            updateHorizontalPosition();
            updateVerticalPosition();
        }

        private void updateHorizontalPosition()
        {
            if (horizontalDirection == 0)
                return;

            var position = Math.Clamp(X + Math.Sign(horizontalDirection) * Clock.ElapsedFrameTime * base_speed, 0, 1);

            if (position == X)
                return;

            X = (float)position;
        }

        private void updateVerticalPosition()
        {
            if (verticalDirection == 0)
                return;

            var position = Math.Clamp(Y + Math.Sign(verticalDirection) * Clock.ElapsedFrameTime * base_speed, 0, 1);

            if (position == Y)
                return;

            Y = (float)position;
        }

        private class Engine : CompositeDrawable
        {
            private readonly MusicIntensityController intensityController;
            private readonly Triangle triangle;

            public Engine()
            {
                AutoSizeAxes = Axes.Both;
                Masking = true;
                EdgeEffect = new EdgeEffectParameters
                {
                    Colour = Color4.White.Opacity(0.3f),
                    Radius = 3,
                    Roundness = 2,
                    Type = EdgeEffectType.Glow,
                };
                InternalChildren = new Drawable[]
                {
                    intensityController = new MusicIntensityController(),
                    triangle = new Triangle
                    {
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.BottomCentre,
                        Width = 5,
                        Colour = Color4.White,
                        Rotation = -90,
                        EdgeSmoothness = new Vector2(2)
                    }
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                intensityController.Intensity.BindValueChanged(intensity => triangle.Height = 5 * (intensity.NewValue / 2f), true);
            }
        }
    }
}
