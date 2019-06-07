// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace osu.Game.Screens.Evast
{
    public abstract class EvastTestScreen : EvastScreen
    {
        protected EvastTestScreen()
        {
            Container objectParent;
            FillFlowContainer settingParent;

            AddRangeInternal(new Drawable[]
            {
                objectParent = new Container
                {
                    Anchor = Anchor.TopLeft,
                    Origin = Anchor.TopLeft,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.7f
                },
                settingParent = new FillFlowContainer
                {
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 20),
                    Margin = new MarginPadding(20)
                },
            });

            AddTestObject(objectParent);
            AddSettings(settingParent);
            Connect();
        }

        protected abstract void AddTestObject(Container parent);
        protected abstract void AddSettings(FillFlowContainer parent);
        protected abstract void Connect();
    }
}
