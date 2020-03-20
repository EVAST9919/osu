using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.Particles
{
    public class TargetedHorizontalParticlesScreen : EvastNoBackButtonScreen
    {
        public TargetedHorizontalParticlesScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Child = new BeatmapLogo
                {
                    Anchor = Anchor.Centre,
                }
            });
        }
    }
}
