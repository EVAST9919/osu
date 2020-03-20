using osu.Game.Screens.Evast.Particles;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShipScreen : EvastVisualScreen
    {
        public SpaceShipScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Child = new SpaceShipArea(),
            });
        }
    }
}
