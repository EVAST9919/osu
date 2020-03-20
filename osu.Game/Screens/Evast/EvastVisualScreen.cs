using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using osu.Game.Input.Bindings;

namespace osu.Game.Screens.Evast
{
    public class EvastVisualScreen : EvastScreen, IKeyBindingHandler<GlobalAction>
    {
        public override bool AllowBackButton => false;

        public override bool HideOverlaysOnEnter => true;

        public override bool CursorVisible => false;

        public EvastVisualScreen()
        {
            DimValue = 0.25f;
        }

        public bool OnPressed(GlobalAction action)
        {
            if (action == GlobalAction.Back)
            {
                this.Exit();
                return true;
            }

            return false;
        }

        public void OnReleased(GlobalAction action)
        {
        }
    }
}
