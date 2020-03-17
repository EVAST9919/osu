using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CompositeDrawable
    {
        public BeatmapLogo(int radius = 350, int barsCount = 80)
        {
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 90,
                    BarWidth = 1,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 90,
                    Rotation = 90,
                    BarWidth = 1,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 90,
                    Rotation = 180,
                    BarWidth = 1,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 90,
                    Rotation = 270,
                    BarWidth = 1,
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
    }
}
