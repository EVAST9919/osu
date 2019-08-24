using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;
using osu.Game.Beatmaps;
using osu.Game.Screens.Backgrounds;

namespace osu.Game.Screens.Evast
{
    public class EvastScreen : OsuScreen
    {
        private const int blur = 20;

        protected override BackgroundScreen CreateBackground() => new BackgroundScreenBeatmap();

        private readonly Bindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        [BackgroundDependencyLoader]
        private void load(Bindable<WorkingBeatmap> workingBeatmap)
        {
            beatmap.BindTo(workingBeatmap);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            beatmap.BindValueChanged(onBeatmapChange, true);
        }

        private void onBeatmapChange(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
            var backgroundModeBeatmap = Background as BackgroundScreenBeatmap;
            if (backgroundModeBeatmap != null)
            {
                backgroundModeBeatmap.Beatmap = beatmap.NewValue;
                backgroundModeBeatmap.BlurAmount.Value = blur;
                backgroundModeBeatmap.FadeTo(1, 250);
            }
        }

        public override void OnResuming(IScreen last)
        {
            beatmap.TriggerChange();
            base.OnResuming(last);
        }
    }
}
