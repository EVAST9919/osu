using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.Particles
{
    public class SpaceParticlesContainer : ParticlesContainer
    {
        /// <summary>
        /// Adjusts the speed of all the particles.
        /// </summary>
        private const int absolute_time = 5000;

        /// <summary>
        /// The maximum scale of a single particle.
        /// </summary>
        private const float particle_max_scale = 5;

        protected override Drawable CreateParticle(bool firstLoad) => new Particle();

        private class Particle : Circle
        {
            private Vector2 finalPosition;
            private double lifeTime;
            private float finalScale;

            public Particle()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativePositionAxes = Axes.Both;
                Colour = Color4.White.Opacity(200);
                Position = new Vector2(RNG.NextSingle(-0.5f, 0.5f), RNG.NextSingle(-0.5f, 0.5f));
                Depth = RNG.NextSingle(0.25f, 1);
                Size = new Vector2(2);
                Scale = new Vector2(Depth);
                Alpha = 0;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                calculateValues();

                this.FadeIn(500);
                this.MoveTo(finalPosition, lifeTime);
                this.ScaleTo(finalScale, lifeTime);
                Expire();
            }

            private void calculateValues()
            {
                float distanceFromZero = distance(Vector2.Zero, Position);

                float x = Position.X;
                float y = Position.Y;

                float fullDistance;

                if ((y < 0 && x + y < 0 && y < x) || (y > 0 && x + y > 0 && x < y))
                    fullDistance = Math.Abs((distance(Vector2.Zero, Position) * 0.5f) / distance(Vector2.Zero, new Vector2(0, y)));
                else
                    fullDistance = Math.Abs((distance(Vector2.Zero, Position) * 0.5f) / distance(Vector2.Zero, new Vector2(x, 0)));

                float xFinal = (Position.X * fullDistance) / distanceFromZero;
                float yFinal = (Position.Y * fullDistance) / distanceFromZero;

                finalPosition = new Vector2(xFinal, yFinal);

                float elapsedDistance = fullDistance - distanceFromZero;

                lifeTime = ((absolute_time * elapsedDistance) / fullDistance) / Depth;
                finalScale = 1 + ((particle_max_scale - 1) * Depth * (elapsedDistance / fullDistance));
            }

            private float distance(Vector2 pointFirst, Vector2 pointSecond)
            {
                float widthDiff = Math.Abs(pointFirst.X - pointSecond.X);
                float heightDiff = Math.Abs(pointFirst.Y - pointSecond.Y);

                return (float)Math.Sqrt((widthDiff * widthDiff) + (heightDiff * heightDiff));
            }
        }
    }
}
