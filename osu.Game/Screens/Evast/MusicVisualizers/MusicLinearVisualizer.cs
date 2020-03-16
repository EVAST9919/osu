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
                flow.Spacing = new Vector2(value);
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
                Spacing = new Vector2(spacing),
            };
        }

        protected override void ClearBars()
        {
            if (flow.Children.Count > 0)
                flow.Clear(true);
        }

        protected override void AddBars()
        {
            foreach (var bar in EqualizerBars)
                flow.Add(bar);

            if (!IsLoaded)
                return;

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
