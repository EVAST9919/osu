﻿using System.Collections.Generic;
using osu.Framework.Screens;

namespace osu.Game.Screens.Evast.Particles
{
    public class ParticlesSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("Space Particles", () => this.Push(new SpaceParticlesScreen())),
            new Button("Horizontal Particles", () => this.Push(new HorizontalParticlesScreen()))
        };
    }
}
