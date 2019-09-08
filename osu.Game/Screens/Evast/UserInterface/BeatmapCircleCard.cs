using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Effects;
using osu.Framework.Bindables;
using osu.Game.Beatmaps;
using osu.Framework.Allocation;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osuTK;

namespace osu.Game.Screens.Evast.UserInterface
{
    public class BeatmapCircleCard : Container
    {
        protected override Container<Drawable> Content => content;

        private readonly Bindable<WorkingBeatmap> working = new Bindable<WorkingBeatmap>();

        private readonly CircularContainer content;
        private Background background;

        public BeatmapCircleCard()
        {
            AddInternal(content = new CircularContainer
            {
                RelativeSizeAxes = Axes.Both,
                Masking = true,
                EdgeEffect = new EdgeEffectParameters
                {
                    Colour = Color4.Black.Opacity(0.15f),
                    Type = EdgeEffectType.Shadow,
                    Radius = 8,
                    Offset = new Vector2(4, 5)
                }
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
            LoadComponentAsync(new Background(beatmap.NewValue), newBackground =>
            {
                background?.FadeOut(200).Expire();
                background = newBackground;
                Add(newBackground);
            });
        }

        private class Background : BufferedContainer
        {
            private readonly Sprite sprite;
            private readonly WorkingBeatmap beatmap;

            public Background(WorkingBeatmap beatmap = null)
            {
                this.beatmap = beatmap;
                RelativeSizeAxes = Axes.Both;

                CacheDrawnFrameBuffer = true;

                Child = sprite = new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fill,
                };
            }

            [BackgroundDependencyLoader]
            private void load(TextureStore textures)
            {
                sprite.Texture = beatmap?.Background ?? textures.Get(@"Backgrounds/bg4");
            }
        }
    }
}
