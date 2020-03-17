using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicVisualizerVariationsTestScreen : EvastScreen
    {
        public MusicVisualizerVariationsTestScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new MusicLinearVisualizer
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    Spacing = 3,
                },
                new MusicLinearVisualizer
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Spacing = 3,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    X = -400,
                    BarWidth = 2,
                    BarsCount = 50,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    X = -400,
                    Rotation = 120,
                    BarWidth = 2,
                    BarsCount = 50,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 240,
                    X = -400,
                    BarWidth = 2,
                    BarsCount = 50,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 180,
                    BarsCount = 50,
                    BarWidth = 2,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 180,
                    BarsCount = 50,
                    BarWidth = 2,
                    Rotation = 180,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    ValueMultiplier = 200,
                    BarsCount = 200,
                    CircleSize = 250,
                    BarWidth = 1,
                    X = 400,
                }
            });
        }
    }
}
