// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Game.Beatmaps;
using osu.Game.Screens.Backgrounds;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast
{
    public class EvastScreen : OsuScreen
    {
        private const float background_blur = 20;

        [Resolved]
        private Bindable<WorkingBeatmap> beatmap { get; set; }

        protected override BackgroundScreen CreateBackground()
        {
            var background = new BackgroundScreenBeatmap();
            return background;
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            this.FadeInFromZero(250);
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            this.FadeIn(250);
            this.ScaleTo(1, 250, Easing.OutSine);
        }

        public override void OnSuspending(IScreen next)
        {
            this.ScaleTo(1.1f, 250, Easing.InSine);
            this.FadeOut(250);
            base.OnSuspending(next);
        }

        public override bool OnExiting(IScreen next)
        {
            if (base.OnExiting(next))
                return true;

            this.FadeOut(100);
            return false;
        }

        protected override void LoadComplete()
        {
            beatmap.BindValueChanged(updateBackground, true);
            base.LoadComplete();
        }

        private void updateBackground(ValueChangedEvent<WorkingBeatmap> beatmapValue)
        {
            if (Background is BackgroundScreenBeatmap backgroundModeBeatmap)
            {
                backgroundModeBeatmap.Beatmap = beatmapValue.NewValue;
                backgroundModeBeatmap.BlurAmount.Value = background_blur;
                backgroundModeBeatmap.FadeColour(Color4.White, 250);
            }
        }
    }
}
