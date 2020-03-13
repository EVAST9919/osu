using System;
using System.Collections.Generic;
using osu.Game.Screens.Evast.NumbersGame;
using osu.Framework.Graphics;

namespace osu.Game.Tests.Visual.Evast
{
    public class TestSceneDrawableNumber : OsuTestScene
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(DrawableNumber),
        };

        public TestSceneDrawableNumber()
        {
            DrawableNumber number;

            Add(number = new DrawableNumber(1, 1)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            });

            AddStep("Increase value", () => number.IncreaseValue());
        }
    }
}
