using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Screens;
using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Graphics.Backgrounds;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Play;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast
{
    public class EvastScreen : ScreenWithBeatmapBackground
    {
        private const int blur = 20;

        protected virtual bool ShowCardOnBeatmapChange => true;

        protected float DimValue
        {
            set => dim.Alpha = 1 - value;
        }

        private readonly Bindable<WorkingBeatmap> beatmap = new Bindable<WorkingBeatmap>();

        private readonly Box dim;

        public EvastScreen()
        {
            AddInternal(dim = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black,
                Alpha = 0.5f,
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
            beatmap.BindValueChanged(onBeatmapChange);
        }

        private bool firstChange = true;
        private BeatmapCard lastCard;

        private void onBeatmapChange(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
            ApplyToBackground(backgroundModeBeatmap =>
            {
                backgroundModeBeatmap.Beatmap = beatmap.NewValue;
                backgroundModeBeatmap.BlurAmount.Value = blur;
                backgroundModeBeatmap.FadeTo(1, 250);
            });

            if (!ShowCardOnBeatmapChange)
                return;

            if (firstChange)
            {
                firstChange = false;
                return;
            }

            var card = new BeatmapCard(beatmap.NewValue)
            {
                Origin = Anchor.TopCentre,
                RelativePositionAxes = Axes.X,
                X = -0.1f,
                Margin = new MarginPadding { Top = 20 },
                Depth = -float.MaxValue
            };

            LoadComponentAsync(card, loaded =>
            {
                if (lastCard != null)
                    lastCard.MoveToX(1.2f, 600, Easing.OutQuint).Expire();

                lastCard = card;
                AddInternal(lastCard);
                lastCard.MoveToX(0.5f, 700, Easing.OutElastic).Delay(2000).MoveToX(1.2f, 600, Easing.InQuint).Expire();
            });
        }

        public override void OnEntering(IScreen last)
        {
            base.OnEntering(last);
            firstChange = true;
            beatmap.TriggerChange();
        }

        public override void OnResuming(IScreen last)
        {
            base.OnResuming(last);
            firstChange = true;
            beatmap.TriggerChange();
        }

        private class BeatmapCard : CompositeDrawable
        {
            public BeatmapCard(WorkingBeatmap beatmap)
            {
                AutoSizeAxes = Axes.Both;
                InternalChildren = new Drawable[]
                {
                    new CircularContainer
                    {
                        RelativeSizeAxes = Axes.Both,
                        Masking = true,
                        Children = new Drawable[]
                        {
                            new BeatmapBackground(beatmap),
                            new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Color4.Black.Opacity(0.5f)
                            }
                        }
                    },
                    new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0, 3),
                        Margin = new MarginPadding { Horizontal = 20, Vertical = 5 },
                        Children = new Drawable[]
                        {
                            new OsuSpriteText
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Font = OsuFont.GetFont(size: 22),
                                Text = beatmap.Metadata.Title
                            },
                            new OsuSpriteText
                            {
                                Anchor = Anchor.TopCentre,
                                Origin = Anchor.TopCentre,
                                Font = OsuFont.GetFont(),
                                Text = beatmap.Metadata.Artist
                            }
                        }
                    }
                };
            }
        }
    }
}
