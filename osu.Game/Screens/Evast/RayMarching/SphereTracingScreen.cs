namespace osu.Game.Screens.Evast.RayMarching
{
    public class SphereTracingScreen : EvastVisualScreen
    {
        public override bool CursorVisible => true;

        public SphereTracingScreen()
        {
            AddInternal(new SphereTracingContainer());
        }
    }
}
