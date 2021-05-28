using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace osu.Game.Screens.Evast
{
    public abstract class EvastTestScreen : EvastScreen
    {
        protected EvastTestScreen()
        {
            AddInternal(new GridContainer
            {
                RelativeSizeAxes = Axes.Both,
                RowDimensions = new[]
                {
                    new Dimension(),
                },
                ColumnDimensions = new[]
                {
                    new Dimension(),
                    new Dimension(GridSizeMode.AutoSize)
                },
                Content = new[]
                {
                    new Drawable[]
                    {
                        CreateTestObject(),
                        new FillFlowContainer
                        {
                            Anchor = Anchor.TopRight,
                            Origin = Anchor.TopRight,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 20),
                            Margin = new MarginPadding(20),
                            Children = CreateSettings()
                        }
                    }
                }
            });

            Connect();
        }

        protected abstract Drawable CreateTestObject();
        protected abstract Drawable[] CreateSettings();

        protected abstract void Connect();
    }
}
