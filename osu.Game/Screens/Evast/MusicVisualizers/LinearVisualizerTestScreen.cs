using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class LinearVisualizerTestScreen : EvastTestScreen
    {
        private LinearMusicVisualizerDrawable visualizer => controller.Visualizer;
        private Controller controller;
        private Settings settings;

        protected override Drawable CreateTestObject() => controller = new Controller();

        protected override Drawable[] CreateSettings() => new Drawable[]
        {
            settings = new Settings(
                visualizer.HeightMultiplier.Value,
                visualizer.Decay.Value,
                visualizer.BarWidth.Value,
                visualizer.BarCount.Value,
                visualizer.Reversed.Value)
        };

        protected override void Connect()
        {
            settings.MultiplierBindable.ValueChanged += newValue => visualizer.HeightMultiplier.Value = newValue.NewValue;
            settings.SmoothnessBindable.ValueChanged += newValue => visualizer.Decay.Value = newValue.NewValue;
            settings.WidthBindable.ValueChanged += newValue => visualizer.BarWidth.Value = newValue.NewValue;
            settings.ReverseBindable.ValueChanged += newValue => visualizer.Reversed.Value = newValue.NewValue;
            settings.AmountBindable.ValueChanged += newValue => visualizer.BarCount.Value = newValue.NewValue;
        }

        private class Settings : PlayerSettingsGroup
        {
            public readonly BindableInt MultiplierBindable;
            public readonly BindableInt SmoothnessBindable;
            public readonly BindableDouble WidthBindable;
            public readonly BindableBool ReverseBindable;
            public readonly BindableInt AmountBindable;

            public Settings(int multiplier, int decay, double width, int count, bool reverse)
                : base("settings")
            {
                Children = new Drawable[]
                {
                    new PlayerSliderBar<int>
                    {
                        LabelText = "Height Multiplier",
                        Current = MultiplierBindable = new BindableInt(multiplier)
                        {
                            MinValue = 0,
                            MaxValue = 1000,
                        }
                    },
                    new PlayerSliderBar<int>
                    {
                        LabelText = "Decay Value",
                        Current = SmoothnessBindable = new BindableInt(decay)
                        {
                            MinValue = 1,
                            MaxValue = 1000,
                        }
                    },
                    new PlayerSliderBar<double>
                    {
                        LabelText = "Bar Width",
                        Current = WidthBindable = new BindableDouble(width)
                        {
                            MinValue = 1,
                            MaxValue = 50,
                        }
                    },
                    new PlayerSliderBar<int>
                    {
                        LabelText = "Bar Count",
                        Current = AmountBindable = new BindableInt(count)
                        {
                            MinValue = 1,
                            MaxValue = 3000,
                        }
                    },
                    new PlayerCheckbox
                    {
                        LabelText = "Reversed",
                        Current = ReverseBindable = new BindableBool(reverse) { Default = reverse }
                    }
                };
            }
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
