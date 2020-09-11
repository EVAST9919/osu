﻿using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class LinearVisualizerTestScreen : EvastTestScreen
    {
        private MusicLinearVisualizer visualizer;
        private Settings settings;

        protected override void AddTestObject(Container parent)
        {
            parent.Children = new Drawable[]
            {
                visualizer = new MusicLinearVisualizer()
                {
                    Anchor = Anchor.CentreLeft,
                    Origin = Anchor.CentreLeft,
                    BarWidth = 2,
                },
            };
        }

        protected override void AddSettings(FillFlowContainer parent)
        {
            parent.Children = new Drawable[]
            {
                settings = new Settings(
                    visualizer.ValueMultiplier,
                    visualizer.Smoothness,
                    visualizer.BarWidth,
                    visualizer.Spacing,
                    visualizer.BarsCount,
                    visualizer.IsReversed)
            };
        }

        protected override void Connect()
        {
            settings.MultiplierBindable.ValueChanged += newValue => visualizer.ValueMultiplier = newValue.NewValue;
            settings.SmoothnessBindable.ValueChanged += newValue => visualizer.Smoothness = newValue.NewValue;
            settings.WidthBindable.ValueChanged += newValue => visualizer.BarWidth = newValue.NewValue;
            settings.ReverseBindable.ValueChanged += newValue => visualizer.IsReversed = newValue.NewValue;
            settings.SpacingBindable.ValueChanged += newValue => visualizer.Spacing = newValue.NewValue;
            settings.AmountBindable.ValueChanged += newValue => visualizer.BarsCount = newValue.NewValue;
        }

        private class Settings : PlayerSettingsGroup
        {
            public readonly BindableFloat MultiplierBindable;
            public readonly BindableInt SmoothnessBindable;
            public readonly BindableFloat WidthBindable;
            public readonly BindableBool ReverseBindable;
            public readonly BindableFloat SpacingBindable;
            public readonly BindableInt AmountBindable;

            public Settings(float multiplier, int smoothnessValue, float width, float spacing, int barsAmount, bool reverse)
                : base("settings")
            {
                Children = new Drawable[]
                {
                    new PlayerSliderBar<float>
                    {
                        LabelText = "Amplitude Multiplier",
                        Bindable = MultiplierBindable = new BindableFloat(multiplier)
                        {
                            Default = multiplier,
                            MinValue = 0,
                            MaxValue = 1000,
                        }
                    },
                    new PlayerSliderBar<int>
                    {
                        LabelText = "Smoothness Value",
                        Bindable = SmoothnessBindable = new BindableInt(smoothnessValue)
                        {
                            Default = smoothnessValue,
                            MinValue = 1,
                            MaxValue = 1000,
                        }
                    },
                    new PlayerSliderBar<float>
                    {
                        LabelText = "Bar Width",
                        Bindable = WidthBindable = new BindableFloat(width)
                        {
                            Default = width,
                            MinValue = 1,
                            MaxValue = 50,
                        }
                    },
                    new PlayerSliderBar<float>
                    {
                        LabelText = "Spacing",
                        Bindable = SpacingBindable = new BindableFloat(spacing)
                        {
                            Default = spacing,
                            MinValue = 0,
                            MaxValue = 20,
                        }
                    },
                    new PlayerSliderBar<int>
                    {
                        LabelText = "Bars Amount",
                        Bindable = AmountBindable = new BindableInt(barsAmount)
                        {
                            Default = barsAmount,
                            MinValue = 1,
                            MaxValue = 200,
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
    }
}
