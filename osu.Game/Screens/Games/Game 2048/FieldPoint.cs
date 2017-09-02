// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Games.Game_2048
{
    public class FieldPoint : Container
    {
        public bool IsEmpty
        {
            set
            {
                if (value == true)
                    FieldNumber = null;
            }
            get
            {
                return FieldNumber == null;
            }
        }

        private FieldNumber fieldNumber;
        public FieldNumber FieldNumber
        {
            set { fieldNumber = value; }
            get { return fieldNumber; }
        }
    }
}
