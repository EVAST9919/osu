// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Games.Game_2048;
using System;

namespace osu.Game.Screens.Games
{
    public class GamesScreen : OsuScreen
    {
        public GamesScreen()
        {
            Add(new ClickableGame(@"2048", () => Push(new Player())));
        }

        private class ClickableGame : OsuClickableContainer
        {
            private readonly Action clickAction;

            public ClickableGame(string gameName, Action clickAction = null)
            {
                this.clickAction = clickAction;

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                Size = new Vector2(200, 200);
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.White,
                    },
                    new OsuSpriteText
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Colour = Color4.Black,
                        Text = gameName,
                    },
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
