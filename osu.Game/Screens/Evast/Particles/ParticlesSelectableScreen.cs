using osu.Framework.Screens;

namespace osu.Game.Screens.Evast.Particles
{
    public class ParticlesSelectableScreen : EvastSelectableScreen
    {
        public ParticlesSelectableScreen()
        {
            Buttons = new[]
            {
                new Button("Space Particles", () => this.Push(new SpaceParticlesScreen())),
                new Button("Horizontal Particles", () => this.Push(new HorizontalParticlesScreen())),
                new Button("Targeted Horizontal Particles", () => this.Push(new TargetedHorizontalParticlesScreen()))
            };
        }
    }
}
