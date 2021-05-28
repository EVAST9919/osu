using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class LinearVisualizerTestScreen : EvastTestScreen
    {
        private readonly Controller controller;
        private LinearMusicVisualizerDrawable visualizer => controller.Visualizer;

        public LinearVisualizerTestScreen()
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
            AddToggle(@"Reversed", r => visualizer.Reversed.Value = r);
        }

        private class Controller : MusicAmplitudesProvider
        {
            public readonly LinearMusicVisualizerDrawable Visualizer;

            public Controller()
            {
                RelativeSizeAxes = Axes.Both;
                Child = Visualizer = new LinearMusicVisualizerDrawable
                {
                    BarAnchor = { Value = BarAnchor.Centre },
                    BarWidth = { Value = 2 }
                };
            }
            protected override void OnAmplitudesUpdate(float[] amplitudes)
            {
                Visualizer.SetAmplitudes(amplitudes);
            }
        }
    }
}
