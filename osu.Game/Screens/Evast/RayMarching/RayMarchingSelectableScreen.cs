using osu.Framework.Screens;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RayMarchingSelectableScreen : EvastSelectableScreen
    {
        public RayMarchingSelectableScreen()
        {
            Buttons = new[]
            {
                new Button("Basic distance", () => this.Push(new RayMarchingDistanceScreen())),
                new Button("Sphere tracing", () => this.Push(new SphereTracingScreen())),
            };
        }
    }
}
