namespace osu.Game.Screens.Evast.RayMarching
{
    public class RendererScreen : EvastVisualScreen
    {
        public override bool CursorVisible => true;

        public RendererScreen()
        {
            AddInternal(new RayMarchingPlayerContainer());
        }
    }
}
