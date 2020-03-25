using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Framework.Graphics;
using osuTK;

namespace osu.Game.Screens.Evast.Particles
{
    public class TargetedHorizontalParticlesScreen : EvastVisualScreen
    {
        public TargetedHorizontalParticlesScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Child = new BeatmapLogo(50, 2)
                {
                    Anchor = Anchor.Centre,
                    Scale = new Vector2(0.3f),
                }
            });
        }
    }
}
