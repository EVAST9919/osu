// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.UserInterface;
using System;
using osu.Framework.Input;
using OpenTK.Input;

namespace osu.Game.Screens.Games.Game_2048
{
    public class Player : OsuScreen
    {
        private readonly FillFlowContainer buttonsContainer;
        private readonly Container playfieldContainer;
        private readonly Playfield playfield;

        public Player()
        {
            Children = new Drawable[]
            {
                playfieldContainer = new Container
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.7f, 1),
                    Child = playfield = new Playfield
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                    }
                },
                new Container
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.3f, 1),
                    Margin = new MarginPadding(10),
                    Child = buttonsContainer = new FillFlowContainer
                    {
                        Direction = FillDirection.Vertical,
                        Spacing = new Vector2(0, 10),
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                    }
                }
            };

            buttonsContainer.Add(new SettingButton(@"Reset field", playfield.Reset));

            playfield.Initialize();
        }

        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            switch (args.Key)
            {
                case Key.Up:
                    playfield.Up();
                    break;
                case Key.Down:
                    playfield.Down();
                    break;
                case Key.Left:
                    playfield.Left();
                    break;
                case Key.Right:
                    playfield.Right();
                    break;
            }
            return base.OnKeyDown(state, args);
        }

        private class SettingButton : OsuButton
        {
            public SettingButton(string text, Action action = null)
            {
                RelativeSizeAxes = Axes.X;
                Height = 40;
                Text = text;
                Action = action;
            }
        }
    }
}
