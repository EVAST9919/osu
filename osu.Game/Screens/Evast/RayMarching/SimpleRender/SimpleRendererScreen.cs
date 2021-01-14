using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.RayMarching.SimpleRender
{
    public class SimpleRendererScreen : EvastScreen
    {
        public SimpleRendererScreen()
        {
            AddInternal(new ContentAdjustmentContainer
            {
                Child = new SimpleRenderContainer()
            });
        }

        private class ContentAdjustmentContainer : Container
        {
            protected override Container<Drawable> Content => content;

            private readonly Container content;

            public ContentAdjustmentContainer()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.Both;
                InternalChild = content = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                };
            }
        }
    }
}
