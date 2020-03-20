using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Input.Events;
using osuTK.Input;
using System;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Textures;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShip : Container
    {
        private const double base_speed = 1.0 / 2048;

        private int horizontalDirection;
        private int verticalDirection;

        private readonly Sprite sprite;

        public SpaceShip()
        {
            Origin = Anchor.CentreLeft;
            Size = new Vector2(60);
            RelativePositionAxes = Axes.Both;
            Y = 0.5f;
            Child = sprite = new Sprite
            {
                RelativeSizeAxes = Axes.Both,
            };
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get("Evast/SpaceShip/space-ship");
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
    }
}
