using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicVisualizerVariationsTestScreen : EvastVisualScreen
    {
        public MusicVisualizerVariationsTestScreen()
        {
            AddInternal(new Controller());
        }

        private class Controller : MusicAmplitudesProvider
        {
            public Controller()
            {
                RelativeSizeAxes = Axes.Both;
                Children = new Drawable[]
                {
                    new LinearMusicVisualizerDrawable
                    {
                        BarWidth = { Value = 3 },
                        BarCount = { Value = 500 },
                        BarAnchor = { Value = BarAnchor.Top }
                    },
                    new LinearMusicVisualizerDrawable
                    {
                        BarWidth = { Value = 3 },
                        BarCount = { Value = 500 },
                        BarAnchor = { Value = BarAnchor.Bottom },
                        Reversed = { Value = true }
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DegreeValue = { Value = 120 },
                        X = -400,
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 50 },
                        Size = new Vector2(250),
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DegreeValue = { Value = 120 },
                        X = -400,
                        Rotation = 120,
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 50 },
                        Size = new Vector2(250),
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DegreeValue = { Value = 120 },
                        X = -400,
                        Rotation = 240,
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 50 },
                        Size = new Vector2(250),
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DegreeValue = { Value = 180 },
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 50 },
                        Size = new Vector2(250),
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        DegreeValue = { Value = 180 },
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 50 },
                        Rotation = 180,
                        Size = new Vector2(250),
                    },
                    new BasicMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        BarWidth = { Value = 2 },
                        BarCount = { Value = 200 },
                        Size = new Vector2(250),
                        X = 400
                    }
                };
            }

            protected override void OnAmplitudesUpdate(float[] amplitudes)
            {
                foreach (var c in Children)
                    ((MusicVisualizerDrawable)c).SetAmplitudes(amplitudes);
            }
        }
    }
}
