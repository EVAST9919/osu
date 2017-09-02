// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Screens.Games.Game_2048
{
    public class FieldBox : Container
    {
        private bool isEmpty = true;
        public bool IsEmpty
        {
            set { isEmpty = value; }
            get { return isEmpty; }
        }

        private FieldNumber fieldNumber;
        public FieldNumber FieldNumber
        {
            set
            {
                fieldNumber = value;
                isEmpty = value == null;
            }
            get
            {
                return fieldNumber;
            }
        }

        public FieldBox()
        {
            Anchor = Anchor.TopLeft;
            Origin = Anchor.TopLeft;
            CornerRadius = 7;
            Masking = true;
            Size = new Vector2(100);
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.Black.Opacity(125),
            };
        }
    }
}
