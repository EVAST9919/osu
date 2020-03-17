namespace osu.Game.Screens.Evast.Particles
{
    public class HorizontalParticlesScreen : EvastNoBackButtonScreen
    {
        public HorizontalParticlesScreen()
        {
            DimValue = 0.2f;

            AddInternal(new HorizontalParticlesContainer());
        }
    }
}
