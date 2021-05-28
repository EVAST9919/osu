using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.Pixels.Snake
{
    public class SnakeScreen : EvastTestScreen
    {
        private readonly SnakePlayfield playfield;

        public SnakeScreen()
        {
            Add(playfield = new SnakePlayfield
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddButton(@"Stop", playfield.Stop);
            AddButton(@"Restart", playfield.Restart);
            AddButton(@"Pause", playfield.Pause);
            AddButton(@"Continue", playfield.Continue);
            AddSlider(@"Update Delay", 5.0, 300, 100, d => playfield.UpdateDelay = d);
        }
    }
}
