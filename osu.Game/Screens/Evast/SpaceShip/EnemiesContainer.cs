using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class EnemiesContainer : Container<Ship>
    {
        private readonly Container bulletsContainer;

        public EnemiesContainer(Container bulletsContainer)
        {
            this.bulletsContainer = bulletsContainer;

            RelativeSizeAxes = Axes.Both;
        }

        public void GenerateEnemy(float size = 60, float y = 0.5f, int speed = 10000)
        {
            var enemy = new Ship(false, size)
            {
                X = 1.1f,
                Y = y,
            };

            Add(enemy);

            enemy.MoveToX(-0.2f, speed).Expire();
        }

        protected override void Update()
        {
            base.Update();

            if (!Children.Any())
                return;

            if (!bulletsContainer.Children.Any())
                return;

            bulletsContainer.Children.ForEach(bullet =>
            {
                Children.ForEach(enemy =>
                {
                    if (checkForCollision(bullet, enemy))
                    {
                        bullet.ClearTransforms();
                        bullet.Expire();

                        enemy.ClearTransforms();
                        enemy.Expire();
                    }
                });
            });
        }

        private bool checkForCollision(Drawable bullet, Drawable enemy)
        {
            if (bullet.DrawPosition.X + bullet.DrawSize.X < enemy.DrawPosition.X || bullet.DrawPosition.X > enemy.DrawPosition.X + enemy.DrawSize.X)
                return false;

            if (bullet.DrawPosition.Y > enemy.DrawPosition.Y + (enemy.DrawSize.Y / 2f) || bullet.DrawPosition.Y + bullet.DrawSize.Y < enemy.DrawPosition.Y - (enemy.DrawSize.Y / 2f))
                return false;

            return true;
        }
    }
}
