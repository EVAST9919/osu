using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using System;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.Particles
{
    public class SpaceParticlesContainer : SpeedAdjustableContainer
    {
        /// <summary>
        /// Number of milliseconds between addition of a new particle.
        /// </summary>
        private const float time_between_updates = 100;

        /// <summary>
        /// Adjusts the speed of all the particles.
        /// </summary>
        private const int absolute_time = 5000;

        /// <summary>
        /// Maximum allowed amount of particles which can be shown at once.
        /// </summary>
        private const int max_particles_count = 350;

        /// <summary>
        /// The size of a single particle.
        /// </summary>
        private const float particle_size = 2;

        /// <summary>
        /// The maximum scale of a single particle.
        /// </summary>
        private const float particle_max_scale = 5;

        public SpaceParticlesContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            generateParticles();
        }

        private void generateParticles()
        {
            var currentParticlesCount = Children.Count;

            if (currentParticlesCount < max_particles_count)
            {
                for (int i = 0; i < max_particles_count - currentParticlesCount; i++)
                    Add(new Particle());
            }

            Scheduler.AddDelayed(generateParticles, time_between_updates);
        }

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
                Size = new Vector2(particle_size);
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

                Scale = new Vector2(Depth);
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
