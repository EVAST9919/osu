using osu.Framework.Graphics;
using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.Particles
{
    public abstract class ParticlesContainer : Container
    {
        /// <summary>
        /// Number of milliseconds between addition of a new particle.
        /// </summary>
        private const float time_between_updates = 50;

        /// <summary>
        /// Maximum allowed amount of particles which can be shown at once.
        /// </summary>
        private const int max_particles_count = 350;

        protected override Container<Drawable> Content => content;

        private readonly SpeedAdjustableContainer content;

        private readonly BindableFloat rate = new BindableFloat();

        protected ParticlesContainer()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            RelativeSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                content = new SpeedAdjustableContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                },
                new MusicIntensityController
                {
                    Intensity = { BindTarget = rate }
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            rate.BindValueChanged(rate => content.Rate = rate.NewValue);

            generateParticles(true);
        }

        private void generateParticles(bool firstLoad)
        {
            var currentParticlesCount = Children.Count;

            if (currentParticlesCount < max_particles_count)
            {
                for (int i = 0; i < max_particles_count - currentParticlesCount; i++)
                    Add(CreateParticle(firstLoad));
            }

            Scheduler.AddDelayed(() => generateParticles(false), time_between_updates);
        }

        protected abstract Drawable CreateParticle(bool firstLoad);
    }
}
