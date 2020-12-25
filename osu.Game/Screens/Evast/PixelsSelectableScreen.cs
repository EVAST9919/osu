using osu.Game.Screens.Evast.Pixels.Snake;
using osu.Game.Screens.Evast.Pixels.LifeGame;
using System.Collections.Generic;
using osu.Framework.Screens;

namespace osu.Game.Screens.Evast
{
    public class PixelsSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("Snake", () => this.Push(new SnakeScreen())),
            new Button("The Game of Life", () => this.Push(new LifeGameScreen()))
        };
    }
}
