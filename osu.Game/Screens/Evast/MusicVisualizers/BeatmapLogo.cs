using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Screens.Evast.Helpers;
using osu.Game.Screens.Evast.MusicVisualizers.Bars;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CurrentBeatmapProvider
    {
        private readonly CircularProgress progressGlow;

        public BeatmapLogo(int radius = 350, int barsCount = 120, float barWidth = 3f)
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
                new UpdateableBeatmapBackground
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

        protected override void Update()
        {
            base.Update();

            var track = Beatmap.Value?.Track;

            progressGlow.Current.Value = track == null ? 0 : (float)(track.CurrentTime / track.Length);
        }

        private class CircularVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();
        }
    }
}
