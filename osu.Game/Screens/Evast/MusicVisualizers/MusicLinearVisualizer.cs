﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicLinearVisualizer : MusicBarsVisualizer
    {
        protected override VisualizerBar CreateNewBar() => new DefaultBar();

        private readonly FillFlowContainer flow;

        private readonly float spacing = 2;
        public float Spacing
        {
            get => flow.Spacing.X;
            set => flow.Spacing = new Vector2(value);
        }

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

        protected class DefaultBar : VisualizerBar
        {
            public DefaultBar()
            {
                Child = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                };
            }

            public override void SetValue(float amplitudeValue, float valueMultiplier, int softness, int faloff)
            {
                var newValue = amplitudeValue * valueMultiplier;

                if (newValue <= Height)
                    return;

                this.ResizeHeightTo(newValue).Then().ResizeHeightTo(0, softness);
            }
        }
    }
}
