using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using System;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class PlayerShipArea : Container
    {
        public Action Shoot;
        public Action StopShoot;

        public PlayerShip Ship { get; private set; }

        public PlayerShipArea()
        {
            RelativeSizeAxes = Axes.Both;
            Width = 0.7f;
            Child = Ship = new PlayerShip
            {
                Shoot = () => Shoot?.Invoke(),
                StopShoot = () => StopShoot?.Invoke()
            };
        }
    }
}
