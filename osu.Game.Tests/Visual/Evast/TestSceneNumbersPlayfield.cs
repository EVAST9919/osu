﻿using System;
using System.Collections.Generic;
using osu.Game.Screens.Evast.NumbersGameNew;
using osu.Framework.Graphics;

namespace osu.Game.Tests.Visual.Evast
{
    public class TestSceneNumbersPlayfield : OsuTestScene
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(DrawableNumber),
            typeof(NumbersPlayfield),
        };

        public TestSceneNumbersPlayfield()
        {
            Add(new NumbersPlayfield(2, 5)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });
        }
    }
}