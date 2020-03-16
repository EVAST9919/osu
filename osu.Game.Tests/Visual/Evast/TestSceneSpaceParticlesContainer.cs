using System;
using System.Collections.Generic;
using osu.Game.Screens.Evast.Particles;

namespace osu.Game.Tests.Visual.Evast
{
    public class TestSceneSpaceParticlesContainer : OsuTestScene
    {
        public override IReadOnlyList<Type> RequiredTypes => new[]
        {
            typeof(SpaceParticlesContainer),
        };

        private readonly SpaceParticlesContainer particles;

        public TestSceneSpaceParticlesContainer()
        {
            Add(particles = new SpaceParticlesContainer());

            AddStep("Rate 2", () => particles.Rate = 2);
            AddStep("Rate 3", () => particles.Rate = 3);
            AddStep("Rate 4", () => particles.Rate = 4);
        }
    }
}
