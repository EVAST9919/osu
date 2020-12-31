﻿using System.Collections.Generic;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.RayMarching;

namespace osu.Game.Screens.Evast
{
    public class RayMarchingSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("Basic distance", () => this.Push(new RayMarchingDistanceScreen())),
            new Button("Sphere tracing", () => this.Push(new SphereTracingScreen())),
            new Button("Fake Renderer", () => this.Push(new RendererScreen())),
            new Button("Real Renderer", () => this.Push(new RealRendererScreen())),
            new Button("Calculators", () => this.Push(new CalculatorScreen()))
        };
    }
}
