using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.Pixels.Tetris
{
    public class TetrisScreen : EvastTestScreen
    {
        private TetrisPlayfield playfield;
        private SpeedSettings speedSettings;

        protected override void AddTestObject(Container parent)
        {
            parent.Child = playfield = new TetrisPlayfield
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
        }

        protected override void AddSettings(FillFlowContainer parent)
        {
            parent.Children = new Drawable[]
            {
                speedSettings = new SpeedSettings(playfield.UpdateDelay),
            };
        }

        protected override void Connect()
        {
            speedSettings.SpeedBindable.ValueChanged += value => playfield.UpdateDelay = value.NewValue;
        }
    }
}
