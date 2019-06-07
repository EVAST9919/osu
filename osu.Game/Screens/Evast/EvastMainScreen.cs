// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using osu.Framework.Graphics;
using osuTK;
using osu.Framework.Graphics.Shapes;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Evast.NumbersGame;
using System;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.Pixels.Snake;
using osu.Game.Screens.Evast.Pixels.LifeGame;

namespace osu.Game.Screens.Evast
{
    public class EvastMainScreen : EvastScreen
    {
        private readonly ButtonSystem buttons;

        public EvastMainScreen()
        {
            AddInternal(buttons = new ButtonSystem
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
            });
            addButtons();
        }

        private void addButtons()
        {
            buttons.AddButton("2048", () => this.Push(new NumbersGameScreen()));
            buttons.AddButton("Snake", () => this.Push(new SnakeScreen()));
            buttons.AddButton("The Game of Life", () => this.Push(new LifeGameScreen()));
        }

        private class ButtonSystem : OsuScrollContainer
        {
            private readonly FillFlowContainer<Button> buttons;

            private const int spacing = 10;

            public ButtonSystem()
            {
                Add(buttons = new FillFlowContainer<Button>
                {
                    Anchor = Anchor.TopCentre,
                    Origin = Anchor.TopCentre,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, spacing),
                    Margin = new MarginPadding { Top = spacing },
                });
            }

            public void AddButton(string name, Action action)
            {
                buttons.Add(new Button(name)
                {
                    Action = action,
                });
            }

            private class Button : OsuHoverContainer
            {
                private const int height = 100;
                private const int width = 400;
                private readonly Box background;

                protected override IEnumerable<Drawable> EffectTargets => new[] { background };

                public Button(string name)
                {
                    Height = height;
                    Width = width;
                    Children = new Drawable[]
                    {
                        background = new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0.5f,
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.Default,
                            Text = name,
                        }
                    };
                }

                [BackgroundDependencyLoader]
                private void load(OsuColour colors)
                {
                    IdleColour = colors.GreySeafoamDarker;
                    HoverColour = colors.GreySeafoamDark;
                }
            }
        }
    }
}
