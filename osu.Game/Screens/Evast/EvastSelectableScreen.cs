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
using System;

namespace osu.Game.Screens.Evast
{
    public abstract class EvastSelectableScreen : EvastScreen
    {
        private readonly ButtonSystem buttonSystem;

        protected EvastSelectableScreen()
        {
            AddInternal(buttonSystem = new ButtonSystem
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                RelativeSizeAxes = Axes.Both,
            });
        }

        protected IEnumerable<Button> Buttons
        {
            get => buttonSystem.Buttons;
            set
            {
                if (buttonSystem.Buttons == value)
                    return;

                foreach (var b in value)
                    buttonSystem.AddButton(b);
            }
        }

        protected class ButtonSystem : OsuScrollContainer
        {
            public IEnumerable<Button> Buttons
            {
                get => buttons.Children;
            }

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

            public void AddButton(Button b) => buttons.Add(b);
        }

        protected class Button : OsuHoverContainer
        {
            private const int height = 100;
            private const int width = 400;
            private readonly Box background;

            protected override IEnumerable<Drawable> EffectTargets => new[] { background };

            public Button(string name, Action action)
            {
                Action = action;

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
