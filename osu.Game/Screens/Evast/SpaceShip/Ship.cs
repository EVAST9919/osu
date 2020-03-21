using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK;

namespace osu.Game.Screens.Evast.SpaceShip
{
    public class Ship : Container
    {
        private readonly Sprite sprite;

        public Ship(bool facingRight = true, float size = 60)
        {
            Origin = Anchor.CentreRight;
            Size = new Vector2(size);
            RelativePositionAxes = Axes.Both;
            Children = new Drawable[]
            {
                sprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };

            if (!facingRight)
                Rotation = 180;
        }

        [BackgroundDependencyLoader]
        private void load(TextureStore textures)
        {
            sprite.Texture = textures.Get("Evast/SpaceShip/space-ship");
        }
    }
}
