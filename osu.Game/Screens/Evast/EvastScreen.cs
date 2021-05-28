using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Screens;
using osu.Game.Beatmaps;
using osu.Game.Screens.Play;

namespace osu.Game.Screens.Evast
{
    public class EvastScreen : ScreenWithBeatmapBackground
    {
        private const int blur = 20;

        protected virtual bool ShowCardOnBeatmapChange => true;

        protected virtual float DimValue { get; } = 0.5f;

        private readonly Bindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        [BackgroundDependencyLoader]
        private void load(Bindable<WorkingBeatmap> workingBeatmap)
        {
            beatmap.BindTo(workingBeatmap);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Beatmap.BindValueChanged(b => updateComponentFromBeatmap(b.NewValue));
        }

        private void updateComponentFromBeatmap(WorkingBeatmap beatmap)
        {
            ApplyToBackground(b =>
            {
                b.IgnoreUserSettings.Value = true;
                b.Beatmap = beatmap;
                b.BlurAmount.Value = blur;
                b.Alpha = 1 - DimValue;
            });
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            updateComponentFromBeatmap(Beatmap.Value);
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            updateComponentFromBeatmap(Beatmap.Value);
        }
    }
}
