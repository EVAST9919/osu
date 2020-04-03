namespace osu.Game.Screens.Evast.SB
{
    public class SBPlayerScreen : EvastVisualScreen
    {
        public override bool CursorVisible => true;

        public SBPlayerScreen()
        {
            AddInternal(new SBPlayer());
        }
    }
}
