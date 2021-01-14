using System.Collections.Generic;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.RayMarching;

namespace osu.Game.Screens.Evast
{
    public class RayMarchingSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("2D distance", () => this.Push(new RayMarchingDistanceScreen())),
            new Button("Sphere tracing", () => this.Push(new SphereTracingScreen())),
            new Button("Fake Renderer", () => this.Push(new RendererScreen())),
            new Button("Ray marching", () => this.Push(new RealRendererScreen())),
            new Button("Calculators", () => this.Push(new CalculatorScreen()))
        };
    }
}
