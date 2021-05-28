using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicBarVisualizersVariationsTestScreen : EvastVisualScreen
    {
        public MusicBarVisualizersVariationsTestScreen()
        {
            AddInternal(new Controller());
        }

        private class Controller : MusicAmplitudesProvider
        {
            public Controller()
            {
                RelativeSizeAxes = Axes.Both;
                AddRange(new Drawable[]
                {
                    new FallMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        BarCount = { Value = 100 },
                        BarWidth = { Value = 5 },
                        Size = new Vector2(250),
                        X = -400
                    },
                    new DotsMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        BarCount = { Value = 100 },
                        BarWidth = { Value = 5 },
                        Size = new Vector2(250)
                    },
                    new RoundedMusicVisualizerDrawable
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        BarCount = { Value = 100 },
                        BarWidth = { Value = 5 },
                        Size = new Vector2(250),
                        X = 400
                    },
                });
            }

            protected override void OnAmplitudesUpdate(float[] amplitudes)
            {
                foreach (var c in Children)
                    ((MusicVisualizerDrawable)c).SetAmplitudes(amplitudes);
            }
        }
    }
}
