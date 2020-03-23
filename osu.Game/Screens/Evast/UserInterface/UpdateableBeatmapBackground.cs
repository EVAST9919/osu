using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Framework.Allocation;
using osuTK;
using osu.Game.Screens.Evast.MusicVisualizers;

namespace osu.Game.Screens.Evast.UserInterface
{
    public class UpdateableBeatmapBackground : Container
    {
        private const int animation_duration = 500;

        protected override Container<Drawable> Content => content;

        private readonly Bindable<WorkingBeatmap> working = new Bindable<WorkingBeatmap>();

        private readonly Container content;
        private BeatmapBackground background;
        private MusicIntensityController intensityController;

        public UpdateableBeatmapBackground()
        {
            AddRangeInternal(new Drawable[]
            {
                new CircularContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Child = content = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Size = new Vector2(1.2f),
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    }
                },
                intensityController = new MusicIntensityController()
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
            intensityController.Intensity.BindValueChanged(intensity =>
            {
                var adjustedIntensity = intensity.NewValue / 150;

                if (adjustedIntensity > 0.2f)
                    adjustedIntensity = 0.2f;

                var sizeDelta = 1.2f - adjustedIntensity;

                if (sizeDelta > content.Size.X)
                    return;

                content.ResizeTo(sizeDelta, 10, Easing.OutQuint).Then().ResizeTo(1.2f, 1500, Easing.OutQuint);

            }, true);
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
