using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osu.Framework.Input.Events;
using osuTK.Input;
using System;

namespace osu.Game.Screens.Evast.SB
{
    public class SBPlayer : CompositeDrawable
    {
        private const double base_speed = 1.0 / 2048;

        private int horizontalDirection;
        private int verticalDirection;

        private readonly Container player;

        public SBPlayer()
        {
            RelativeSizeAxes = Axes.Both;
            AddInternal(player = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.Both,
                Size = new Vector2(15),
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both
                }
            });
        }

        #region Move Logic

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.W:
                        verticalDirection--;
                        return true;

                    case Key.S:
                        verticalDirection++;
                        return true;

                    case Key.A:
                        horizontalDirection--;
                        return true;

                    case Key.D:
                        horizontalDirection++;
                        return true;
                };
            }

            return base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyUpEvent e)
        {
            switch (e.Key)
            {
                case Key.W:
                    verticalDirection++;
                    return;

                case Key.S:
                    verticalDirection--;
                    return;

                case Key.A:
                    horizontalDirection++;
                    return;

                case Key.D:
                    horizontalDirection--;
                    return;
            };

            base.OnKeyUp(e);
        }

        protected override void Update()
        {
            base.Update();
            updatePosition();
        }

        private void updatePosition()
        {
            if (horizontalDirection != 0)
            {
                var position = Math.Clamp(player.X + Math.Sign(horizontalDirection) * Clock.ElapsedFrameTime * base_speed, -0.5f, 0.5f);

                if (position == player.X)
                    return;

                player.X = (float)position;
            }

            if (verticalDirection != 0)
            {
                var position = Math.Clamp(player.Y + Math.Sign(verticalDirection) * Clock.ElapsedFrameTime * base_speed, -0.5f, 0.5f);

                if (position == player.Y)
                    return;

                player.Y = (float)position;
            }
        }

        #endregion
    }
}
