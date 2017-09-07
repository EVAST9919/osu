// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Graphics;
using osu.Game.Rulesets.Mods;
using osu.Game.Rulesets.UI;
using System.Collections.Generic;
using osu.Framework.Graphics;
using osu.Game.Rulesets.Scoring;
using osu.Framework.Input.Bindings;
using osu.Game.Rulesets.Visual.Mods;
using osu.Game.Rulesets.Visual.Scoring;
using osu.Game.Rulesets.Visual.UI;

namespace osu.Game.Rulesets.Visual
{
    public class VisualRuleset : Ruleset
    {
        public override RulesetContainer CreateRulesetContainerWith(WorkingBeatmap beatmap, bool isForCurrentRuleset) => new VisualRulesetContainer(this, beatmap, isForCurrentRuleset);

        public override IEnumerable<KeyBinding> GetDefaultKeyBindings(int variant = 0) => null;

        public override IEnumerable<Mod> GetModsFor(ModType type)
        {
            switch (type)
            {
                case ModType.DifficultyReduction:
                    return new Mod[]
                    {
                        null,
                        null,
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VisualModHalfTime(),
                                new VisualModDaycore(),
                            },
                        },
                    };

                case ModType.DifficultyIncrease:
                    return new Mod[]
                    {
                        null,
                        null,
                        new MultiMod
                        {
                            Mods = new Mod[]
                            {
                                new VisualModDoubleTime(),
                                new VisualModNightcore(),
                            },
                        },
                        null,
                        null,
                    };

                case ModType.Special:
                    return new Mod[]
                    {
                        null,
                        null,
                        null,
                        null,
                    };

                default:
                    return new Mod[] { };
            }
        }

        public override Mod GetAutoplayMod() => new ModAutoplay();

        public override string Description => "osu!visual";

        public override Drawable CreateIcon() => new SpriteIcon { Icon = FontAwesome.fa_osu_dice };

        public override DifficultyCalculator CreateDifficultyCalculator(Beatmap beatmap) => new VisualDifficultyCalculator(beatmap);

        public override ScoreProcessor CreateScoreProcessor() => new VisualScoreProcessor();

        public override int LegacyID => 2;

        public VisualRuleset(RulesetInfo rulesetInfo)
            : base(rulesetInfo)
        {
        }
    }
}
