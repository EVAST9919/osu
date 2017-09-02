// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using System;

namespace osu.Game.Screens.Games.Game_2048
{
    public class FieldNumber : Container
    {
        private readonly Box background;
        private readonly OsuSpriteText valueText;

        private int value;
        public int Value
        {
            set
            {
                this.value = value;
                valueText.Text = value.ToString();
                background.FadeColour(bg_colours[findPower(value)], 100);
            }
            get { return value; }
        }

        private static readonly Color4[] bg_colours =
        {
            OsuColour.FromHex("ffffdd"),//2
            OsuColour.FromHex("ffffbb"),//4
            OsuColour.FromHex("ffffaa"),//8
            OsuColour.FromHex("ffff99"),//16
            OsuColour.FromHex("ffff88"),//32
            OsuColour.FromHex("ffff77"),//64
            OsuColour.FromHex("ffff66"),//128
            OsuColour.FromHex("ffff55"),//256
            OsuColour.FromHex("ffff44"),//512
            OsuColour.FromHex("ffff22"),//1024
            OsuColour.FromHex("ffff00"),//2048
            OsuColour.FromHex("5d16ff"),//4096
            OsuColour.FromHex("288cff"),//8192
            OsuColour.FromHex("ff28e9"),//16384
            OsuColour.FromHex("4cff28"),//32768
            OsuColour.FromHex("ff8c28"),//65536
            OsuColour.FromHex("ff2828"),//131072
        };

        public FieldNumber()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            CornerRadius = 7;
            Masking = true;
            Size = new Vector2(100);
            Children = new Drawable[]
            {
                background = new Box { RelativeSizeAxes = Axes.Both },
                valueText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = Color4.Black,
                    TextSize = 30,
                }
            };

            Value = 2;
        }

        private int findPower(int number)
        {
            int power = 0;

            while(number != 2)
            {
                number = (int)Math.Sqrt(number);
                power++;
            }

            return power;
        }

        public void NextValue() => Value++;
    }
}
