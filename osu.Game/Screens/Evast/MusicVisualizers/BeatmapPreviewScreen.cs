using osu.Framework.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Screens.Evast.Particles;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapPreviewScreen : EvastVisualScreen
    {
        public BeatmapPreviewScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new SpaceParticlesContainer(),
                new ParallaxContainer
                {
                    ParallaxAmount = -0.0025f,
                    Child = new BeatmapLogo
                    {
                        Anchor = Anchor.Centre,
                    }
                }
            });
        }
    }
}
