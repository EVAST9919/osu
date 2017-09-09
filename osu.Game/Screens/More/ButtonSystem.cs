// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using System;

namespace osu.Game.Screens.More
{
    public class ButtonSystem : FillFlowContainer
    {
        public Action OnVisualizer;

        public ButtonSystem()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            AutoSizeAxes = Axes.Both;
            Direction = FillDirection.Horizontal;
            Children = new Drawable[]
            {
                new ScreenButton(@"Visualizer", () => OnVisualizer?.Invoke())
            };
        }

        private class ScreenButton : OsuClickableContainer
        {
            private readonly Box background;
            private readonly OsuSpriteText text;

            private readonly Action clickAction;

            public ScreenButton(string title, Action clickAction = null)
            {
                this.clickAction = clickAction;

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

            protected override bool OnClick(InputState state)
            {
                clickAction?.Invoke();
                return base.OnClick(state);
            }
        }
    }
}
