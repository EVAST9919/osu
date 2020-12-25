using System.Collections.Generic;
using osu.Framework.Screens;

namespace osu.Game.Screens.Evast.SB
{
    public class SBSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("Player", () => this.Push(new SBPlayerScreen()))
        };
    }
}
