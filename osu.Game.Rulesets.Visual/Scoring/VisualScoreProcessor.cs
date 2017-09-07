// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Rulesets.Visual.Judgements;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Visual.Objects;

namespace osu.Game.Rulesets.Visual.Scoring
{
    internal class VisualScoreProcessor : ScoreProcessor<VisualBaseHit, VisualJudgement>
    {
        public VisualScoreProcessor()
        {
        }

        public VisualScoreProcessor(RulesetContainer<VisualBaseHit, VisualJudgement> rulesetContainer)
            : base(rulesetContainer)
        {
        }

        protected override void Reset()
        {
            base.Reset();

            Health.Value = 1;
            Accuracy.Value = 1;
        }

        protected override void OnNewJudgement(VisualJudgement judgement)
        {
        }
    }
}
