using osu.Framework.Input.Bindings;
using osu.Framework.Screens;
using osu.Game.Input.Bindings;

namespace osu.Game.Screens.Evast
{
    public class EvastNoBackButtonScreen : EvastScreen, IKeyBindingHandler<GlobalAction>
    {
        public override bool AllowBackButton => false;

        public override bool HideOverlaysOnEnter => true;

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
