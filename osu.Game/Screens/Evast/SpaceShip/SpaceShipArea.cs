using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShipArea : Container
    {
        public SpaceShipArea()
        {
            RelativeSizeAxes = Axes.Both;
            Width = 0.7f;
            Child = new SpaceShip();
        }
    }
}
