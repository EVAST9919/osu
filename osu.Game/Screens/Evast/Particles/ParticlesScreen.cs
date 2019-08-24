namespace osu.Game.Screens.Evast.Particles
{
    public class ParticlesScreen : EvastScreen
    {
        public ParticlesScreen()
        {
            AddInternal(new ParticlesContainer(70, 70, 5));
        }
    }
}
