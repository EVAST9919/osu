using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class RealRendererScreen : EvastScreen
    {
        public RealRendererScreen()
        {
            AddInternal(new ContentAdjustmentContainer
            {
                Child = new RealRendererContainer()
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
