// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.MathUtils;
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
                    Colour = OsuColour.FromHex(@"776E65"),
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
            OsuColour.FromHex("EDE0C8"),//4
            OsuColour.FromHex("F3B179"),//8
            OsuColour.FromHex("F59663"),//16
            OsuColour.FromHex("F77C5F"),//32
            OsuColour.FromHex("F65E3C"),//64
            OsuColour.FromHex("EDCF72"),//128
            OsuColour.FromHex("ECCC61"),//256
            OsuColour.FromHex("EDC750"),//512
            OsuColour.FromHex("EEC53F"),//1024
            OsuColour.FromHex("ECC22D"),//2048
            OsuColour.FromHex("ECC22D"),//4096
            OsuColour.FromHex("0011FF"),//8182
        };
    }
}
