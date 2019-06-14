// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Game.Graphics.UserInterface;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.ExportArea
{
    public class ExportAreaScreen : EvastScreen
    {
        public ExportAreaScreen()
        {
            AddInternal(new SearchTextBox
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Width = 500,
                Margin = new MarginPadding { Top = 100 },
            });
        }
    }
}
