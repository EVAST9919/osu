using System;
using System.Collections.Generic;
using osu.Game.Screens.Evast.NumbersGameNew;

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

            Add(number = new DrawableNumber());

            AddStep("Increase value", number.IncreaseValue);
        }
    }
}
