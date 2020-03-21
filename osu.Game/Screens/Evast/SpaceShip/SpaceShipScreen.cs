using osu.Game.Screens.Evast.Particles;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Utils;
using osuTK;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class SpaceShipScreen : EvastVisualScreen
    {
        private const int bullet_speed = 1500;
        private const int bullets_spacing = 150;

        private readonly PlayerShipArea shipArea;
        private readonly Container bulletsContainer;
        private readonly EnemiesContainer enemiesContainer;

        private bool isShooting;

        public SpaceShipScreen()
        {
            AddInternal(new TargetedHorizontalParticlesContainer
            {
                Children = new Drawable[]
                {
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(0.9f),
                        Children = new Drawable[]
                        {
                            bulletsContainer = new Container
                            {
                                RelativeSizeAxes = Axes.Both
                            },
                            enemiesContainer = new EnemiesContainer(bulletsContainer),
                            shipArea = new PlayerShipArea
                            {
                                Shoot = () =>
                                {
                                    isShooting = true;
                                    generateBullet();
                                },
                                StopShoot = () => isShooting = false
                            },
                        }
                    }
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            generateEnemy();
        }

        private void generateEnemy()
        {
            enemiesContainer.GenerateEnemy(y: RNG.NextSingle());

            Scheduler.AddDelayed(generateEnemy, RNG.Next(500, 1000));
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

            if (!isShooting)
                return;

            Scheduler.AddDelayed(generateBullet, bullets_spacing);
        }
    }
}
