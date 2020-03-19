using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Screens.Evast.MusicVisualizers.Bars;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CompositeDrawable
    {
        public BeatmapLogo(int radius = 350, int barsCount = 70, float barWidth = 4f)
        {
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 120,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 240,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new BeatmapCircleCard
                {
                    Size = new Vector2(radius),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });
        }

        private class CircularVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();
        }
    }
}
