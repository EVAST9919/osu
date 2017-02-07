﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics.Sprites;
using osu.Game.Overlays;
using OpenTK.Graphics;

namespace osu.Game.Graphics.UserInterface
{
    public class OsuTextBox : TextBox
    {
        protected override Color4 BackgroundUnfocused => Color4.Black.Opacity(0.5f);
        protected override Color4 BackgroundFocused => OsuColour.Gray(0.3f).Opacity(0.8f);
        protected override Color4 BackgroundCommit => BorderColour;

        public OsuTextBox()
        {
            Height = 40;
            TextContainer.Height = OsuSpriteText.FONT_SIZE / Height;
            CornerRadius = 5;
        }

        [BackgroundDependencyLoader]
        private void load(OsuColour colour)
        {
            BorderColour = colour.Yellow;
        }

        protected override bool OnFocus(InputState state)
        {
            BorderThickness = 3;

            return base.OnFocus(state);
        }

        protected override void OnFocusLost(InputState state)
        {
            BorderThickness = 0;

            base.OnFocusLost(state);
        }
    }

    public class OsuPasswordTextBox : OsuTextBox
    {
        protected virtual char MaskCharacter => '*';

        protected override Drawable AddCharacterToFlow(char c) => base.AddCharacterToFlow(MaskCharacter);
    }
}
