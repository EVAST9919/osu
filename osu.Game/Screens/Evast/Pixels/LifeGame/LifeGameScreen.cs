using osu.Framework.Graphics;
using osu.Game.Graphics.UserInterface;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast.Pixels.LifeGame
{
    public class LifeGameScreen : EvastTestScreen
    {
        private LifeGamePlayfield playfield;
        private GeneralSettings generalSettings;
        private SpeedSettings speedSettings;

        protected override Drawable CreateTestObject() => playfield = new LifeGamePlayfield(55, 55, 12)
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
        };

        protected override Drawable[] CreateSettings() => new Drawable[]
        {
            generalSettings = new GeneralSettings(),
            speedSettings = new SpeedSettings(playfield.UpdateDelay),
        };

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
            public readonly TriangleButton ResetButton;
            public readonly TriangleButton StartButton;
            public readonly TriangleButton PauseButton;
            public readonly TriangleButton RandomButton;

            public GeneralSettings()
                : base("general")
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
