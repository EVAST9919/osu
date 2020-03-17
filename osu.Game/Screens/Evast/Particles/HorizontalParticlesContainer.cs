﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.Particles
{
    public class HorizontalParticlesContainer : ParticlesContainer
    {
        /// <summary>
        /// Adjusts the speed of all the particles.
        /// </summary>
        private const int absolute_time = 8500;

        protected override Drawable CreateParticle(bool firstLoad) => new Particle(firstLoad);

        private class Particle : Circle
        {
            public Particle(bool firstLoad)
            {
                Origin = Anchor.Centre;
                RelativePositionAxes = Axes.Both;
                Colour = Color4.White;
                Position = new Vector2(firstLoad ? RNG.NextSingle(0.1f, 1) : 1.1f, RNG.NextSingle(0, 1));
                Depth = RNG.NextSingle(0.1f, 1);
                Size = new Vector2(4);
                Scale = new Vector2(Depth);
                Alpha = 0;
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                this.FadeIn(300);
                this.MoveToX(-0.1f, absolute_time / Depth * Position.X).Then().FadeOut(20);
                Expire();
            }
        }
    }
}
