using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Beatmaps;
using osu.Game.Screens.Evast.MusicVisualizers.Bars;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CompositeDrawable
    {
        private readonly Bindable<WorkingBeatmap> working = new Bindable<WorkingBeatmap>();

        private readonly CircularProgress progressGlow;

        public BeatmapLogo(int radius = 350, int barsCount = 70, float barWidth = 4f)
        {
            Origin = Anchor.Centre;
            Size = new Vector2(radius);

            AddRangeInternal(new Drawable[]
            {
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 120,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new CircularVisualizer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    DegreeValue = 120,
                    Rotation = 240,
                    BarWidth = barWidth,
                    BarsCount = barsCount,
                    CircleSize = radius,
                },
                new BeatmapCircleCard
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
                (progressGlow = new CircularProgress
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    InnerRadius = 0.02f,
                }).WithEffect(new GlowEffect
                {
                    Colour = Color4.White,
                    Strength = 5,
                    PadExtent = true
                }),
            });
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<WorkingBeatmap> beatmap)
        {
            working.BindTo(beatmap);
        }

        protected override void Update()
        {
            base.Update();

            var track = working.Value?.Track;

            progressGlow.Current.Value = track == null ? 0 : (float)(track.CurrentTime / track.Length);
        }

        private class CircularVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();
        }
    }
}
