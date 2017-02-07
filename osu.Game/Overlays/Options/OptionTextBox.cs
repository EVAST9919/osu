﻿// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Configuration;
using osu.Framework.Graphics.UserInterface;
using osu.Game.Graphics.UserInterface;

namespace osu.Game.Overlays.Options
{
    public class OptionTextBox : OsuTextBox
    {
        private Bindable<string> bindable;

        public Bindable<string> Bindable
        {
            set
            {
                if (bindable != null)
                    bindable.ValueChanged -= bindableValueChanged;
                bindable = value;
                if (bindable != null)
                {
                    base.Text = bindable.Value;
                    bindable.ValueChanged += bindableValueChanged;
                }
            }
        }
        
        protected override string InternalText
        {
            get { return base.InternalText; }
            set
            {
                base.InternalText = value;
                if (bindable != null)
                    bindable.Value = value;
            }
        }

        private void bindableValueChanged(object sender, EventArgs e)
        {
            Text = bindable.Value;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (bindable != null)
                bindable.ValueChanged -= bindableValueChanged;
            base.Dispose(isDisposing);
        }
    }
}