using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class BasicBullet : CompositeDrawable
    {
        public BasicBullet()
        {
            Size = new Vector2(15, 3);
            InternalChild = new CircularContainer
            {
                Masking = true,
                RelativeSizeAxes = Axes.Both,
                EdgeEffect = new EdgeEffectParameters
                {
                    Colour = Color4.Red,
                    Type = EdgeEffectType.Glow,
                    Radius = 2,
                    Roundness = 3,
                },
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both
                }
            };
        }
    }
}
