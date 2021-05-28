using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Game.Screens.Evast.Helpers;
using osu.Game.Screens.Play.PlayerSettings;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class CircularVisualizerTestScreen : EvastTestScreen
    {
        private BasicMusicVisualizerDrawable visualizer => controller.Visualizer;
        private Controller controller;
        private Settings settings;

        protected override Drawable CreateTestObject() => controller = new Controller();

        protected override Drawable[] CreateSettings() => new Drawable[]
        {
            settings = new Settings(
                    visualizer.HeightMultiplier.Value,
                    visualizer.Decay.Value,
                    visualizer.BarWidth.Value,
                    visualizer.Size.X,
                    visualizer.BarCount.Value,
                    visualizer.DegreeValue.Value,
                    visualizer.Reversed.Value)
        };

        protected override void Connect()
        {
            settings.MultiplierBindable.ValueChanged += newValue => visualizer.HeightMultiplier.Value = newValue.NewValue;
            settings.SmoothnessBindable.ValueChanged += newValue => visualizer.Decay.Value = newValue.NewValue;
            settings.WidthBindable.ValueChanged += newValue => visualizer.BarWidth.Value = newValue.NewValue;
            settings.ReverseBindable.ValueChanged += newValue => visualizer.Reversed.Value = newValue.NewValue;
            settings.SizeBindable.ValueChanged += newValue => visualizer.Size = new Vector2(newValue.NewValue);
            settings.AmountBindable.ValueChanged += newValue => visualizer.BarCount.Value = newValue.NewValue;
            settings.DegreeBindable.ValueChanged += newValue => visualizer.DegreeValue.Value = newValue.NewValue;
        }

        private class Settings : PlayerSettingsGroup
        {
            public readonly BindableInt MultiplierBindable;
            public readonly BindableInt SmoothnessBindable;
            public readonly BindableDouble WidthBindable;
            public readonly BindableBool ReverseBindable;
            public readonly BindableFloat SizeBindable;
            public readonly BindableInt AmountBindable;
            public readonly BindableFloat DegreeBindable;

            public Settings(int multiplier, int decay, double width, float circleSize, int count, float degreeValue, bool reverse)
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
                    new PlayerSliderBar<float>
                    {
                        LabelText = "Circle Size",
                        Current = SizeBindable = new BindableFloat(circleSize)
                        {
                            MinValue = 0,
                            MaxValue = 500,
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
                    new PlayerSliderBar<float>
                    {
                        LabelText = "Degree Value",
                        Current = DegreeBindable = new BindableFloat(degreeValue)
                        {
                            MinValue = 0,
                            MaxValue = 360,
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
