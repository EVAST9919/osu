// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Screens.More
{
    public class MoreScreen : OsuScreen
    {
        private ScreenButton visualizer;

        public MoreScreen()
        {
            Children = new Drawable[]
            {
                new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Horizontal,
                    Children = new Drawable[]
                    {
                        visualizer = new ScreenButton(@"Visualizer"),
                    }
                }
            };
        }

        private class ScreenButton : OsuClickableContainer
        {
            private readonly Box background;
            private readonly OsuSpriteText text;

            public ScreenButton(string title)
            {
                Size = new Vector2(100);
                Children = new Drawable[]
                {
                    background = new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White,
                    },
                    text = new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Text = title,
                        TextSize = 20,
                        Colour = Color4.Black,
                    }
                };
            }
        }
    }
}
