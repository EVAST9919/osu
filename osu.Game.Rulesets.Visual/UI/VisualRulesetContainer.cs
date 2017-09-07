// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Input;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Beatmaps;
using osu.Game.Rulesets.Visual.Judgements;
using osu.Game.Rulesets.Visual.Scoring;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Scoring;
using osu.Game.Rulesets.UI;
using osu.Game.Rulesets.Visual.Objects;
using osu.Game.Rulesets.Visual.Beatmaps;

namespace osu.Game.Rulesets.Visual.UI
{
    public class VisualRulesetContainer : RulesetContainer<VisualPlayfield, VisualBaseHit, VisualJudgement>
    {
        public VisualRulesetContainer(Ruleset ruleset, WorkingBeatmap beatmap, bool isForCurrentRuleset)
            : base(ruleset, beatmap, isForCurrentRuleset)
        {
        }

        public override ScoreProcessor CreateScoreProcessor() => new VisualScoreProcessor(this);

        protected override BeatmapConverter<VisualBaseHit> CreateBeatmapConverter() => new VisualBeatmapConverter();

        protected override Playfield<VisualBaseHit, VisualJudgement> CreatePlayfield() => new VisualPlayfield();

        public override PassThroughInputManager CreateInputManager() => new VisualInputManager(Ruleset.RulesetInfo);

        protected override DrawableHitObject<VisualBaseHit, VisualJudgement> GetVisualRepresentation(VisualBaseHit h) => null;
    }
}
