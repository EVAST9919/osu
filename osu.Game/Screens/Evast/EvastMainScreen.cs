using osu.Game.Screens.Evast.NumbersGame;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.Particles;
using osu.Game.Screens.Evast.ApiHelper;

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
                new Button("Particles", () => this.Push(new ParticlesSelectableScreen())),
                new Button("Music Visualizers", () => this.Push(new MusicVisualizersSelectableScreen())),
                new Button("APIv2 Helper", () => this.Push(new ApiV2HelperScreen())),
            };
        }
    }
}
