using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicIntensityScreen : EvastVisualScreen
    {
        private readonly MusicIntensityController controller;
        private readonly OsuSpriteText kiaiText;
        private readonly OsuSpriteText intensityText;
        private readonly Box intensityBox;

        public MusicIntensityScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                controller = new MusicIntensityController(),
                new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 5),
                    Children = new Drawable[]
                    {
                        kiaiText = new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Font = OsuFont.GetFont(size: 25)
                        },
                        new FillFlowContainer
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            AutoSizeAxes = Axes.Both,
                            Direction = FillDirection.Horizontal,
                            Children = new Drawable[]
                            {
                                new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Font = OsuFont.GetFont(size: 25),
                                    Text = "Intensity: "
                                },
                                intensityText = new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Font = OsuFont.GetFont(size: 25, fixedWidth: true)
                                }
                            }
                        },
                        intensityBox = new Box
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopCentre,
                            Height = 20,
                        }
                    }
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            controller.IsKiai.BindValueChanged(kiai => kiaiText.Text = $"Kiai: {kiai.NewValue}", true);
            controller.Intensity.BindValueChanged(intensity =>
            {
                intensityText.Text = intensity.NewValue.ToString("00.00");

                var newIntensity = intensity.NewValue * 40;

                if (newIntensity < intensityBox.Width)
                    return;

                intensityBox.ResizeWidthTo(newIntensity).Then().ResizeWidthTo(0, 200);
            }, true);
        }
    }
}
