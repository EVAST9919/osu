using osu.Framework.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Screens.Evast.Particles;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapPreviewScreen : EvastVisualScreen
    {
        protected override bool ShowCardOnBeatmapChange => false;

        public BeatmapPreviewScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new ParticlesContainer(),
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
