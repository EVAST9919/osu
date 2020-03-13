using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osu.Game.Beatmaps;
using osu.Game.Screens.Backgrounds;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast
{
    public class EvastScreen : OsuScreen
    {
        private const int blur = 20;

        protected override BackgroundScreen CreateBackground() => new BackgroundScreenBeatmap();

        private readonly Bindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        public EvastScreen()
        {
            AddInternal(new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black.Opacity(0.5f)
            });
        }

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
            if (Background is BackgroundScreenBeatmap backgroundModeBeatmap)
            {
                backgroundModeBeatmap.Beatmap = beatmap.NewValue;
                backgroundModeBeatmap.BlurAmount.Value = blur;
                backgroundModeBeatmap.FadeTo(1, 250);
            }
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            beatmap.TriggerChange();
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            beatmap.TriggerChange();
        }
    }
}
