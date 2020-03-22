using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Framework.Allocation;

namespace osu.Game.Screens.Evast.UserInterface
{
    public class UpdateableBeatmapBackground : Container
    {
        private const int animation_duration = 500;

        protected override Container<Drawable> Content => content;

        private readonly Bindable<WorkingBeatmap> working = new Bindable<WorkingBeatmap>();

        private readonly CircularContainer content;
        private BeatmapBackground background;

        public UpdateableBeatmapBackground()
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
            LoadComponentAsync(new BeatmapBackground(beatmap.NewValue)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Alpha = 0,
            }, newBackground =>
            {
                background?.FadeOut(animation_duration, Easing.OutQuint);
                background?.RotateTo(360, animation_duration, Easing.OutQuint);
                background?.Expire();

                background = newBackground;
                Add(newBackground);
                newBackground.RotateTo(360, animation_duration, Easing.OutQuint);
                newBackground.FadeIn(animation_duration, Easing.OutQuint);
            });
        }
    }
}
