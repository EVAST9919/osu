using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.Particles
{
    public class TargetedHorizontalParticlesScreen : EvastVisualScreen
    {
        public TargetedHorizontalParticlesScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Child = new BeatmapLogo(100, 50, 2)
                {
                    Anchor = Anchor.Centre,
                }
            });
        }
    }
}
