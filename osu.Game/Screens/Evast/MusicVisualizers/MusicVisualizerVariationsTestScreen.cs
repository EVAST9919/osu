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
                },
                new MusicLinearVisualizer
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsAmount = 30,
                    DegreeValue = 180,
                    BarWidth = 5,
                    CircleSize = 150,
                    X = -400,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 180,
                    CircleSize = 150,
                    BarsAmount = 30,
                    BarWidth = 5,
                    X = -400,
                    Rotation = 180,
                    IsReversed = true,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 180,
                    BarsAmount = 50,
                    BarWidth = 2,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 180,
                    BarsAmount = 50,
                    BarWidth = 2,
                    Rotation = 180,
                },
                new MusicCircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    ValueMultiplier = 200,
                    BarsAmount = 200,
                    CircleSize = 250,
                    BarWidth = 1,
                    X = 400,
                }
            });
        }
    }
}
