// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast.Pixels.LifeGame
{
    public class LifeGameScreen : EvastTestScreen
    {
        private LifeGamePlayfield playfield;
        private GeneralSettings generalSettings;
        private SpeedSettings speedSettings;

        protected override void AddTestObject(Container parent)
        {
            parent.Child = playfield = new LifeGamePlayfield(55, 55, 12)
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
            generalSettings.ResetButton.Action = playfield.Stop;
            generalSettings.StartButton.Action = playfield.Continue;
            generalSettings.PauseButton.Action = playfield.Pause;
            generalSettings.RandomButton.Action = playfield.GenerateRandom;

            speedSettings.SpeedBindable.ValueChanged += value => playfield.UpdateDelay = value.NewValue;
        }

        private class GeneralSettings : PlayerSettingsGroup
        {
            protected override string Title => @"general";

            public readonly TriangleButton ResetButton;
            public readonly TriangleButton StartButton;
            public readonly TriangleButton PauseButton;
            public readonly TriangleButton RandomButton;

            public GeneralSettings()
            {
                Children = new Drawable[]
                {
                    ResetButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Reset simulation",
                    },
                    StartButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Start simulation",
                    },
                    PauseButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Pause simulation",
                    },
                    RandomButton = new TriangleButton
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 40,
                        Text = "Create random map",
                    }
                };
            }
        }
    }
}
