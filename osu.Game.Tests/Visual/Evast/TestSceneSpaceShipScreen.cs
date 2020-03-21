using osu.Framework.Screens;
using osu.Game.Screens.Evast.SpaceShip;
using osu.Framework.Graphics;

namespace osu.Game.Tests.Visual.Evast
{
    public class TestSceneSpaceShipScreen : OsuTestScene
    {
        public TestSceneSpaceShipScreen()
        {
            ScreenStack screens;

            Add(screens = new ScreenStack
            {
                RelativeSizeAxes = Axes.Both
            });

            screens.Push(new SpaceShipScreen());
        }
    }
}
