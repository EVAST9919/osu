using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CompositeDrawable
    {
        private const int circle_size = 400;

        public BeatmapLogo()
        {
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;

            AddRangeInternal(new Drawable[]
            {
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    BarWidth = 2,
                    BarsAmount = 80,
                    CircleSize = circle_size,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 120,
                    BarWidth = 2,
                    BarsAmount = 80,
                    CircleSize = circle_size,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 240,
                    BarWidth = 2,
                    BarsAmount = 80,
                    CircleSize = circle_size,
                },
                new BeatmapCircleCard
                {
                    Size = new Vector2(circle_size),
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });
        }
    }
}
