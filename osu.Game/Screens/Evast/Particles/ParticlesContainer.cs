using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.Particles
{
    public class ParticlesContainer : CurrentRateContainer
    {
        private ParticlesDrawable particles;

        public ParticlesContainer()
        {
            RelativeSizeAxes = Axes.Both;
            Add(particles = new ParticlesDrawable());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            IsKiai.BindValueChanged(kiai =>
            {
                if (kiai.NewValue)
                {
                    particles.SetRandomDirection();
                }
                else
                    particles.Direction.Value = MoveDirection.Forward;
            });
        }
    }
}
