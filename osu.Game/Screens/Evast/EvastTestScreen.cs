using System;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Screens.Play.PlayerSettings;
using osuTK;

namespace osu.Game.Screens.Evast
{
    public abstract class EvastTestScreen : EvastScreen
    {
        private readonly LocalSettings settings;
        private readonly Container content;

        protected EvastTestScreen()
        {
            AddInternal(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(),
                },
                ColumnDimensions = new[]
                {
                    new Dimension(),
                    new Dimension(GridSizeMode.AutoSize)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        content = new Container
                        {
                            RelativeSizeAxes = Axes.Both
                        },
                        new FillFlowContainer
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 20),
                            Margin = new MarginPadding(20),
                            Child = settings = new LocalSettings()
                        }
                    }
                }
            });
        }

        protected void Add(Drawable d) => content.Add(d);

        protected void AddSlider(string name, float min, float max, float initial, Action<float> action)
        {
            var slider = new PlayerSliderBar<float>
            {
                LabelText = name,
                Current = new BindableFloat(initial)
                {
                    MinValue = min,
                    MaxValue = max
                }
            };
            slider.Current.BindValueChanged(v => action?.Invoke(v.NewValue), true);

            settings.Add(slider);
        }

        protected void AddSlider(string name, int min, int max, int initial, Action<int> action)
        {
            var slider = new PlayerSliderBar<int>
            {
                LabelText = name,
                Current = new BindableInt(initial)
                {
                    MinValue = min,
                    MaxValue = max
                }
            };
            slider.Current.BindValueChanged(v => action?.Invoke(v.NewValue), true);

            settings.Add(slider);
        }

        protected void AddSlider(string name, double min, double max, double initial, Action<double> action)
        {
            var slider = new PlayerSliderBar<double>
            {
                LabelText = name,
                Current = new BindableDouble(initial)
                {
                    MinValue = min,
                    MaxValue = max
                }
            };
            slider.Current.BindValueChanged(v => action?.Invoke(v.NewValue), true);

            settings.Add(slider);
        }

        protected void AddButton(string name, Action action) => settings.Add(new TriangleButton
        {
            RelativeSizeAxes = Axes.X,
            Height = 40,
            Text = name,
            Action = action
        });

        protected void AddToggle(string name, Action<bool> action)
        {
            var checkbox = new PlayerCheckbox
            {
                LabelText = name,
                Current = new BindableBool()
            };
            checkbox.Current.BindValueChanged(v => action?.Invoke(v.NewValue), true);

            settings.Add(checkbox);
        }

        private class LocalSettings : PlayerSettingsGroup
        {
            public LocalSettings()
                : base(@"Settings")
            {
            }
        }
    }
}
