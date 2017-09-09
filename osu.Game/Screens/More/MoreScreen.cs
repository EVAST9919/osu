// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Screens.More.Visualizer;

namespace osu.Game.Screens.More
{
    public class MoreScreen : OsuScreen
    {
        public MoreScreen()
        {
            Children = new Drawable[]
            {
                new ButtonSystem
                {
                    OnVisualizer = delegate { Push(new VisualizerSettings()); },
                },
            };
        }
    }
}
