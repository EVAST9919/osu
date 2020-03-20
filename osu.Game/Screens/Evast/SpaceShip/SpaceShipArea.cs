using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using System;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShipArea : Container
    {
        public Action Shoot;
        public Action StopShoot;

        public SpaceShip Ship { get; private set; }

        public SpaceShipArea()
        {
            RelativeSizeAxes = Axes.Both;
            Width = 0.7f;
            Child = Ship = new SpaceShip
            {
                Shoot = () => Shoot?.Invoke(),
                StopShoot = () => StopShoot?.Invoke()
            };
        }
    }
}
