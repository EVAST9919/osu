// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast.Pixels.Snake
{
    public class SnakeScreen : EvastTestScreen
    {
        private SnakePlayfield playfield;
        private SpeedSettings speedSettings;
        private GeneralSettings generalSettings;

        protected override void AddTestObject(Container parent)
        {
            parent.Child = playfield = new SnakePlayfield(20, 20, 25)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        protected override void AddSettings(FillFlowContainer parent)
        {
            parent.Children = new Drawable[]
            {
                generalSettings = new GeneralSettings(),
                speedSettings = new SpeedSettings(playfield.UpdateDelay),
            };
        }

        protected override void Connect()
        {
            generalSettings.StopButton.Action = playfield.Stop;
            generalSettings.RestartButton.Action = playfield.Restart;
            generalSettings.PauseButton.Action = playfield.Pause;
            generalSettings.ContinueButton.Action = playfield.Continue;

            speedSettings.SpeedBindable.ValueChanged += value => playfield.UpdateDelay = value.NewValue;
        }

        private class GeneralSettings : PlayerSettingsGroup
        {
            protected override string Title => @"general";

            public readonly TriangleButton StopButton;
            public readonly TriangleButton RestartButton;
            public readonly TriangleButton PauseButton;
            public readonly TriangleButton ContinueButton;

            public GeneralSettings()
            {
                Children = new Drawable[]
                {
                    StopButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Stop",
                    },
                    RestartButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Restart",
                    },
                    PauseButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Pause",
                    },
                    ContinueButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Continue",
                    },
                };
            }
        }
    }
}
