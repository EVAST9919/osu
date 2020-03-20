using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.Particles
{
    public class TargetedHorizontalParticlesContainer : Container
    {
        protected override Container<Drawable> Content => content;

        private readonly Container content;

        public TargetedHorizontalParticlesContainer(float minParticleScale = 0.1f, float maxParticleScale = 1)
        {
            RelativeSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                new Particles(minParticleScale, maxParticleScale / 2f),
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                },
                new Particles(maxParticleScale / 2f, maxParticleScale)
            });
        }

        private class Particles : HorizontalParticlesContainer
        {
            protected override int MaxParticlesCount => 175;

            public Particles(float minParticleScale, float maxParticleScale)
                : base(minParticleScale, maxParticleScale)
            {
            }
        }
    }
}
