using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicLinearVisualizer : MusicBarsVisualizer
    {
        private float spacing = 2;

        public float Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                flow.Spacing = new Vector2(value, 0);
            }
        }

        private readonly FillFlowContainer flow;

        public MusicLinearVisualizer()
        {
            AutoSizeAxes = Axes.Both;
            Child = flow = new FillFlowContainer
            {
                AutoSizeAxes = Axes.Both,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(spacing, 0),
            };
        }

        protected override void ClearBars() => flow.Clear(true);

        protected override void AddBars()
        {
            EqualizerBars.ForEach(flow.Add);
            setOrigins();
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            setOrigins();
        }

        private void setOrigins()
        {
            flow.Anchor = Origin;
            flow.Origin = Origin;

            foreach (var bar in EqualizerBars)
            {
                bar.Anchor = Origin;
                bar.Origin = Origin;
            }
        }
    }
}
