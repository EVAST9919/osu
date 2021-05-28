using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.Pixels.LifeGame
{
    public class LifeGameScreen : EvastTestScreen
    {
        private readonly LifeGamePlayfield playfield;

        public LifeGameScreen()
        {
            Add(playfield = new LifeGamePlayfield(55, 55, 12)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddButton(@"Reset Simulation", playfield.Stop);
            AddButton(@"Start Simulation", playfield.Continue);
            AddButton(@"Pause Simulation", playfield.Pause);
            AddButton(@"Create Random Map", playfield.GenerateRandom);
            AddSlider(@"Update Delay", 5.0, 200, 100, d => playfield.UpdateDelay = d);
        }
    }
}
