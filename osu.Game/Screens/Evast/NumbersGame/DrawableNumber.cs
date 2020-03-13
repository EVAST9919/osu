using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.NumbersGame
{
    public class DrawableNumber : Container
    {
        private int colourPointer;
        public int Value { get; private set; }

        public bool IsLocked;
        public void Lock() => IsLocked = true;
        public Vector2 Coordinates { set; get; }

        private readonly OsuSpriteText numberText;
        private readonly Box background;

        public DrawableNumber()
        {
            Value = RNG.NextBool(0.9) ? 2 : 4;
            if (Value == 4)
                colourPointer++;

            Anchor = Anchor.TopLeft;
            Origin = Anchor.Centre;
            Size = new Vector2(100);
            Scale = new Vector2(0);
            Alpha = 0;
            CornerRadius = 6;
            Masking = true;
            Children = new Drawable[]
            {
                background = new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = colours[colourPointer],
                },
                numberText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = OsuFont.GetFont(size: 50, weight: FontWeight.Bold),
                    Text = Value.ToString(),
                    Colour = new Color4(119, 110, 101, 255),
                    Shadow = false,
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            this.ScaleTo(1, 120);
            this.FadeTo(1, 120);
        }

        public void IncreaseValue() => Value *= 2;

        public void IncreaseValueAnimation()
        {
            numberText.Text = Value.ToString();

            if (Value == 8)
                numberText.Colour = Color4.White;

            colourPointer++;
            background.Colour = colours[colourPointer];

            this.ScaleTo(1.2f, 40, Easing.OutQuint).Then().ScaleTo(1, 160, Easing.OutQuint);
        }

        private static readonly Color4[] colours =
        {
            Color4.White,
            new Color4(237, 224, 200, 255),//4
            new Color4(243, 177, 121, 255),//8
            new Color4(245, 150, 99, 255),//16
            new Color4(247, 124, 95, 255),//32
            new Color4(246, 94, 60, 255),//64
            new Color4(237, 207, 114, 255),//128
            new Color4(236, 204, 97, 255),//256
            new Color4(237, 199, 80, 255),//512
            new Color4(238, 197, 63, 255),//1024
            new Color4(236, 194, 45, 255),//2048
            new Color4(236, 194, 45, 255),//4096
            new Color4(0, 17, 255, 255),//8192
        };
    }
}
