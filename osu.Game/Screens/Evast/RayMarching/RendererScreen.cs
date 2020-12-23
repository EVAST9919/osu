using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RendererScreen : EvastVisualScreen
    {
        private const int ray_count = 400;

        public override bool CursorVisible => true;

        private readonly RayMarchingPlayerContainer topView;
        private readonly PerspectiveView perspectiveView;

        public RendererScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                topView = new RayMarchingPlayerContainer(ray_count),
                perspectiveView = new PerspectiveView()
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            topView.Rays.BindValueChanged(rays => perspectiveView.SetRays(rays.NewValue));
        }

        private class PerspectiveView : CompositeDrawable
        {
            private const int size_multiplier = 2;

            private readonly Box[] drawableRays = new Box[ray_count];

            public PerspectiveView()
            {
                Container columns;

                Anchor = Anchor.BottomRight;
                Origin = Anchor.BottomRight;
                Width = ray_count * size_multiplier;
                Height = Width * 9f / 16;
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black
                    },
                    columns = new Container
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                };

                for (int i = 0; i < ray_count; i++)
                {
                    columns.Add(drawableRays[i] = new Box
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Y,
                        Width = size_multiplier,
                        EdgeSmoothness = Vector2.One,
                        X = i * size_multiplier,
                    });
                }
            }

            public void SetRays(float[] rays)
            {
                for (int i = 0; i < ray_count; i++)
                {
                    drawableRays[i].Height = 1 - rays[i] / 1500;
                    drawableRays[i].Colour = Color4.White.Opacity(1 - rays[i] / 1000);
                }
            }
        }
    }
}
