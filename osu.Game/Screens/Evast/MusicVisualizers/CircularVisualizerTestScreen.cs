using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class CircularVisualizerTestScreen : EvastTestScreen
    {
        private readonly Controller controller;
        private BasicMusicVisualizerDrawable visualizer => controller.Visualizer;

        public CircularVisualizerTestScreen()
        {
            Add(controller = new Controller());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddSlider(@"Height Multiplier", 0, 1000, 400, m => visualizer.HeightMultiplier.Value = m);
            AddSlider(@"Decay Value", 1, 1000, 200, d => visualizer.Decay.Value = d);
            AddSlider(@"Bar Width", 1.0, 50, 3, w => visualizer.BarWidth.Value = w);
            AddSlider(@"Bar Count", 1, 3000, 100, c => visualizer.BarCount.Value = c);
            AddSlider(@"Degree Value", 0f, 360f, 360f, d => visualizer.DegreeValue.Value = d);
            AddSlider(@"Circle Size", 0f, 500f, 360f, d => visualizer.Size = new Vector2(d));
            AddToggle(@"Reversed", r => visualizer.Reversed.Value = r);
        }

        private class Controller : MusicAmplitudesProvider
        {
            public readonly BasicMusicVisualizerDrawable Visualizer;

            public Controller()
            {
                RelativeSizeAxes = Axes.Both;
                Child = Visualizer = new BasicMusicVisualizerDrawable
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarWidth = { Value = 2 },
                    Size = new Vector2(250)
                };
            }
            protected override void OnAmplitudesUpdate(float[] amplitudes)
            {
                Visualizer.SetAmplitudes(amplitudes);
            }
        }
    }
}
