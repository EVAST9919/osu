using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osu.Framework.Input.Events;
using osuTK.Input;
using System;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.SB
{
    public class SBPlayer : CompositeDrawable
    {
        private const double base_speed = 1.0 / 2048;

        private bool isMoving;
        private Vector2 target;

        private readonly Container player;
        private readonly Box drawablePlayer;

        public SBPlayer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
            FillMode = FillMode.Fit;
            AddInternal(player = new Container
            {
                Origin = Anchor.Centre,
                RelativePositionAxes = Axes.Both,
                Position = new Vector2(0.5f),
                Size = new Vector2(15),
                Child = drawablePlayer = new Box
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    EdgeSmoothness = Vector2.One,
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
                        isMoving = true;
                        expand();
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
                    isMoving = false;
                    collapse();
                    return;
            };

            base.OnKeyUp(e);
        }

        protected override void Update()
        {
            base.Update();
            updatePlayer();
        }

        private void updatePlayer()
        {
            var currentMousePos = ToLocalSpace(GetContainingInputManager().CurrentState.Mouse.Position);
            target = new Vector2(currentMousePos.X / RelativeToAbsoluteFactor.X, currentMousePos.Y / RelativeToAbsoluteFactor.Y);

            if (Precision.AlmostEquals(target.X, player.X, 0.001f) && Precision.AlmostEquals(target.Y, player.Y, 0.001f))
                return;

            updateRotation();

            if (isMoving)
            {
                double positionX;
                double positionY;

                double xDelta;
                double yDelta;

                var ratio = Math.Abs(target.X - player.X) / Math.Abs(target.Y - player.Y);

                if (ratio > 1)
                {
                    xDelta = Math.Sign(target.X - player.X) * Clock.ElapsedFrameTime * base_speed;
                    yDelta = Math.Sign(target.Y - player.Y) * Clock.ElapsedFrameTime * base_speed / ratio;
                }
                else
                {
                    xDelta = Math.Sign(target.X - player.X) * Clock.ElapsedFrameTime * base_speed * ratio;
                    yDelta = Math.Sign(target.Y - player.Y) * Clock.ElapsedFrameTime * base_speed;
                }

                positionX = Math.Clamp(player.X + xDelta, 0, 1);
                positionY = Math.Clamp(player.Y + yDelta, 0, 1);

                player.Position = new Vector2((float)positionX, (float)positionY);
            }
        }

        private void updateRotation() => drawablePlayer.Rotation = (float)(Math.Atan2(target.Y - player.Y, target.X - player.X) * 180 / Math.PI);

        private void expand() => drawablePlayer.ScaleTo(new Vector2(1.4f, 0.6f), 400, Easing.Out);

        private void collapse() => drawablePlayer.ScaleTo(1, 400, Easing.OutElastic);

        #endregion
    }
}
