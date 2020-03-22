using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Framework.Allocation;

namespace osu.Game.Screens.Evast.UserInterface
{
    public class BeatmapCircleCard : Container
    {
        protected override Container<Drawable> Content => content;

        private readonly Bindable<WorkingBeatmap> working = new Bindable<WorkingBeatmap>();

        private readonly CircularContainer content;
        private BeatmapBackground background;

        public BeatmapCircleCard()
        {
            AddInternal(content = new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
            });
        }

        [BackgroundDependencyLoader]
        private void load(Bindable<WorkingBeatmap> beatmap)
        {
            working.BindTo(beatmap);
        }

        protected override void LoadComplete()
        {
            working.BindValueChanged(onBeatmapChanged, true);
            base.LoadComplete();
        }

        private void onBeatmapChanged(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
            LoadComponentAsync(new BeatmapBackground(beatmap.NewValue), newBackground =>
            {
                background?.FadeOut(200).Expire();
                background = newBackground;
                Add(newBackground);
            });
        }
    }
}
