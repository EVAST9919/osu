using osu.Game.Screens.Evast.Particles;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShipScreen : EvastVisualScreen
    {
        private const int bullet_speed = 1500;
        private const int bullets_spacing = 70;

        private readonly SpaceShipArea shipArea;
        private readonly Container bulletsContainer;

        public SpaceShipScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Children = new Drawable[]
                {
                    shipArea = new SpaceShipArea
                    {
                        Shoot = () => onShoot(),
                        StopShoot = () => onShootStop()
                    },
                    bulletsContainer = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                }
            });
        }

        private void onShoot()
        {
            generateBullet();
        }

        private void generateBullet()
        {
            var bulletX = shipArea.Ship.X * 0.7f;

            var bullet = new BasicBullet
            {
                RelativePositionAxes = Axes.Both,
                Origin = Anchor.CentreLeft,
                Y = shipArea.Ship.Y,
                X = bulletX
            };

            bulletsContainer.Add(bullet);

            bullet.MoveToX(1.1f, (1 - bulletX) * bullet_speed).Expire();

            Scheduler.AddDelayed(generateBullet, bullets_spacing);
        }

        private void onShootStop()
        {
            Scheduler.CancelDelayedTasks();
        }
    }
}
