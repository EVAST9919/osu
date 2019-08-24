using osu.Framework.Bindables;
using osu.Game.Screens.Play.PlayerSettings;

namespace osu.Game.Screens.Evast
{
    public class SpeedSettings : PlayerSettingsGroup
    {
        protected override string Title => @"speed";

        public readonly BindableDouble SpeedBindable;

        public SpeedSettings(double defaultValue)
        {
            Child = new PlayerSliderBar<double>
            {
                LabelText = "Update delay",
                Bindable = SpeedBindable = new BindableDouble(defaultValue)
                {
                    Default = defaultValue,
                    MinValue = 5,
                    MaxValue = 500,
                }
            };
        }
    }
}
