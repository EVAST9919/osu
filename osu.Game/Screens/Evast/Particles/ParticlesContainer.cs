using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.MathUtils;
using osuTK;
using osu.Framework.Extensions.IEnumerableExtensions;

namespace osu.Game.Screens.Evast.Particles
{
    public class ParticlesContainer : Container
    {
        public ParticlesContainer(int xAmount, int yAmount, int size = 1, int spacing = 0)
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            Size = new Vector2(xAmount * spacing + xAmount * size, yAmount * spacing + yAmount * size);

            for (int j = 0; j < yAmount; j++)
            {
                for (int i = 0; i < xAmount; i++)
                {
                    Add(new Particle(size)
                    {
                        Position = new Vector2(i * spacing + i * size, j * spacing + j * size),
                    });
                }
            }

            Children.ForEach(t => t.MoveTo(RNG.NextBool() ? new Vector2(0, Size.X / 2) : new Vector2(Size.Y, Size.X / 2), RNG.NextDouble(100, 5000), Easing.OutQuad));
        }

        private class Particle : Container
        {
            public Particle(int size)
            {
                Anchor = Anchor.TopLeft;
                Origin = Anchor.Centre;
                Size = new Vector2(size);
                Child = new Box { RelativeSizeAxes = Axes.Both, };
            }
        }
    }
}
