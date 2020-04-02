using osu.Framework.Screens;

namespace osu.Game.Screens.Evast.SB
{
    public class SBSelectableScreen : EvastSelectableScreen
    {
        public SBSelectableScreen()
        {
            Buttons = new[]
            {
                new Button("Player", () => this.Push(new SBPlayerScreen()))
            };
        }
    }
}
