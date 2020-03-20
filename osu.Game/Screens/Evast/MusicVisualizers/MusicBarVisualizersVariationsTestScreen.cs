using osu.Framework.Graphics;
using osu.Game.Screens.Evast.MusicVisualizers.Bars;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicBarVisualizersVariationsTestScreen : EvastVisualScreen
    {
        public MusicBarVisualizersVariationsTestScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new FallBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsCount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                    X = -400,
                },
                new SplittedBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsCount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                },
                new CircularBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsCount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                    X = 400,
                }
            });
        }

        private class CircularBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();
        }

        private class SplittedBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new SplittedBar();
        }

        private class FallBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new FallBar();
        }
    }
}
