using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Screens.Evast.Helpers;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapLogo : CurrentBeatmapProvider
    {
        private const int radius = 350;

        private readonly CircularProgress progressGlow;

        public BeatmapLogo()
        {
            Origin = Anchor.Centre;
            Size = new Vector2(radius);

            AddRangeInternal(new Drawable[]
            {
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

            progressGlow.Current.Value = track == null ? 0 : track.CurrentTime / track.Length;
        }
    }
}
