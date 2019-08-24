using osu.Framework.Screens;
using osu.Game.Screens.Evast.Pixels.Snake;
using osu.Game.Screens.Evast.Pixels.LifeGame;
using osu.Game.Screens.Evast.Pixels.Tetris;

namespace osu.Game.Screens.Evast
{
    public class PixelsSelectableScreen : EvastSelectableScreen
    {
        public PixelsSelectableScreen()
        {
            Buttons = new[]
            {
                new Button("Snake", () => this.Push(new SnakeScreen())),
                new Button("The Game of Life", () => this.Push(new LifeGameScreen())),
                new Button("Tetris", () => this.Push(new TetrisScreen())),
            };
        }
    }
}
