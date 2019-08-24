using osu.Game.Screens.Evast.NumbersGame;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.ExportArea;
using osu.Game.Screens.Evast.Particles;

namespace osu.Game.Screens.Evast
{
    public class EvastMainScreen : EvastSelectableScreen
    {
        public EvastMainScreen()
        {
            Buttons = new[]
            {
                new Button("2048", () => this.Push(new NumbersGameScreen())),
                new Button("Pixel Games", () => this.Push(new PixelsSelectableScreen())),
                new Button("Export Area", () => this.Push(new ExportAreaScreen())),
                new Button("Particles", () => this.Push(new ParticlesScreen())),
            };
        }
    }
}
