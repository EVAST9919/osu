using osu.Game.Screens.Evast.NumbersGame;
using osu.Game.Screens.Evast.ApiHelper;
using osu.Game.Screens.Evast.SB;
using System.Collections.Generic;
using osu.Framework.Screens;

namespace osu.Game.Screens.Evast
{
    public class EvastMainScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("2048", () => this.Push(new NumbersGameScreen())),
            new Button("Ray Marching", () => this.Push(new RayMarchingSelectableScreen())),
            new Button("Shape & Beats", () => this.Push(new SBSelectableScreen())),
            new Button("Pixel Games", () => this.Push(new PixelsSelectableScreen())),
            new Button("Particles", () => this.Push(new ParticlesSelectableScreen())),
            new Button("Music Visualizers", () => this.Push(new MusicVisualizersSelectableScreen())),
            new Button("APIv2 Helper", () => this.Push(new ApiV2HelperScreen())),
            new Button("APIv2 Helper (public)", () => this.Push(new ApiV2PublicHelperScreen()))
        };
    }
}
