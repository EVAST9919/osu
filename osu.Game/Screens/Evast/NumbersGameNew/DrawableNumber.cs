using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Bindables;
using osuTK.Graphics;
using osuTK;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics;
using System;

namespace osu.Game.Screens.Evast.NumbersGameNew
{
    public class DrawableNumber : CompositeDrawable
    {
        public const int SIZE = 100;

        public Vector2 IndexedPosition;

        private readonly BindableInt power = new BindableInt();
        private readonly Box background;
        private readonly Container content;
        private readonly OsuSpriteText text;

        public DrawableNumber(int xIndex, int yIndex, int startPower = 1)
        {
            IndexedPosition = new Vector2(xIndex, yIndex);
            power.Value = startPower;

            Size = new Vector2(SIZE);
            InternalChild = content = new Container
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both,
                Scale = new Vector2(0),
                Masking = true,
                CornerRadius = 4,
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    },
                    text = new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Font = OsuFont.GetFont(size: 40, weight: FontWeight.Bold),
                        Text = getPoweredString(),
                        Colour = new Color4(119, 110, 101, 255),
                        Shadow = false,
                    }
                }
            };

            if (startPower != 1)
                background.Colour = getPowerColour(power.Value);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            power.BindValueChanged(onPowerChanged);

            content.ScaleTo(1, 200, Easing.OutQuint);
        }

        public void IncreaseValue() => power.Value++;

        private void onPowerChanged(ValueChangedEvent<int> powerChangedEvent)
        {
            this.ScaleTo(1.2f, 40, Easing.OutQuint).Then().ScaleTo(1, 160, Easing.OutQuint);
            background.FadeColour(getPowerColour(powerChangedEvent.NewValue), 200, Easing.OutQuint);
            text.Text = getPoweredString();
        }

        private string getPoweredString() => Math.Round(Math.Pow(2, power.Value)).ToString();

        private Color4 getPowerColour(int newPower)
        {
            switch(newPower)
            {
                case 1:
                    return Color4.White;

                case 2:
                    return new Color4(237, 224, 200, 255);

                case 3:
                    return new Color4(243, 177, 121, 255);

                case 4:
                    return new Color4(245, 150, 99, 255);

                case 5:
                    return new Color4(247, 124, 95, 255);

                case 6:
                    return new Color4(246, 94, 60, 255);

                case 7:
                    return new Color4(237, 207, 114, 255);

                case 8:
                    return new Color4(236, 204, 97, 255);

                case 9:
                    return new Color4(237, 199, 80, 255);

                case 10:
                    return new Color4(238, 197, 63, 255);

                default:
                case 11:
                    return new Color4(236, 194, 45, 255);
            }
        }
    }
}
