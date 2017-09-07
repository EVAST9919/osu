// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Game.Beatmaps;
using osu.Game.Rulesets.Visual.Objects;
using System.Collections.Generic;
using System;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Beatmaps;
using osu.Game.Rulesets.Objects;

namespace osu.Game.Rulesets.Visual.Beatmaps
{
    internal class VisualBeatmapConverter : BeatmapConverter<VisualBaseHit>
    {
        protected override IEnumerable<Type> ValidConversionTypes { get; } = new[] { typeof(IHasXPosition) };

        protected override IEnumerable<VisualBaseHit> ConvertHitObject(HitObject obj, Beatmap beatmap) => null;
    }
}
