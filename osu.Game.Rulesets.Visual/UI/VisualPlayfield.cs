// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Graphics;
using osu.Game.Rulesets.UI;
using OpenTK;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Visual.Objects;
using osu.Game.Rulesets.Visual.Judgements;

namespace osu.Game.Rulesets.Visual.UI
{
    public class VisualPlayfield : Playfield<VisualBaseHit, VisualJudgement>
    {
        protected override Container<Drawable> Content => content;
        private readonly Container<Drawable> content;

        public VisualPlayfield()
        {
            Size = new Vector2(1);

            Anchor = Anchor.TopCentre;
            Origin = Anchor.TopCentre;

            InternalChildren = new Drawable[]
            {
                content = new Container<Drawable>
                {
                    RelativeSizeAxes = Axes.Both,
                },
            };
        }
    }
}
