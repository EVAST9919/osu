namespace osu.Game.Screens.Evast.RayMarching
{
    public class RayMarchingDistanceScreen : EvastVisualScreen
    {
        public override bool CursorVisible => true;

        public RayMarchingDistanceScreen()
        {
            AddInternal(new RayMarchingDistanceContainer());
        }
    }
}
