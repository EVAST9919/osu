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
        private const double base_speed = 1.0 / 1500;
        private const int teleport_multiplier = 40;

        private bool isMoving;
        private bool teleport;
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
                Size = new Vector2(20),
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

                    case Key.Space:
                        teleport = true;
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

                case Key.Space:
                    teleport = false;
                    return;
            };

            base.OnKeyUp(e);
        }

        protected override void Update()
        {
            base.Update();

            var currentMousePos = ToLocalSpace(GetContainingInputManager().CurrentState.Mouse.Position);
            target = new Vector2(currentMousePos.X / RelativeToAbsoluteFactor.X, currentMousePos.Y / RelativeToAbsoluteFactor.Y);

            if (Precision.AlmostEquals(target.X, player.X, 0.001f) && Precision.AlmostEquals(target.Y, player.Y, 0.001f))
                return;

            updateRotation();

            if (isMoving)
            {
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

                if (teleport)
                {
                    xDelta *= teleport_multiplier;
                    yDelta *= teleport_multiplier;
                    createTeleportOut(player.Position);
                    teleport = false;
                }

                var positionX = Math.Clamp(player.X + xDelta, 0, 1);
                var positionY = Math.Clamp(player.Y + yDelta, 0, 1);

                player.Position = new Vector2((float)positionX, (float)positionY);
            }
        }

        private void updateRotation() => drawablePlayer.Rotation = (float)(Math.Atan2(target.Y - player.Y, target.X - player.X) * 180 / Math.PI);

        private void expand() => drawablePlayer.ScaleTo(new Vector2(1.4f, 0.6f), 400, Easing.Out);

        private void collapse() => drawablePlayer.ScaleTo(new Vector2(0.7f, 1.3f), 100, Easing.OutQuint).Then().ScaleTo(Vector2.One, 200, Easing.Out);

        private void createTeleportOut(Vector2 position)
        {
            var circle = new Circle
            {
                Size = new Vector2(80),
                RelativePositionAxes = Axes.Both,
                Origin = Anchor.Centre,
                Position = position,
                Scale = Vector2.Zero,
            };

            AddInternal(circle);

            circle.ScaleTo(1, 300, Easing.OutQuint);
            circle.FadeOut(300, Easing.OutQuint);
            circle.Expire();
        }

        #endregion
    }
}
