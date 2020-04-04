// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Skinning;

namespace osu.Game.Rulesets.Mania.Skinning
{
    /// <summary>
    /// A mania legacy skin element.
    /// </summary>
    public class LegacyManiaElement : CompositeDrawable
    {
        /// <summary>
        /// Retrieve a per-column-count skin configuration.
        /// </summary>
        /// <param name="skin">The skin from which configuration is retrieved.</param>
        /// <param name="lookup">The value to retrieve.</param>
        /// <param name="index">If not null, denotes the index of the column to which the entry applies.</param>
        protected virtual IBindable<T> GetManiaSkinConfig<T>(ISkin skin, LegacyManiaSkinConfigurationLookups lookup, int? index = null)
            => skin.GetConfig<ManiaSkinConfigurationLookup, T>(
                new ManiaSkinConfigurationLookup(lookup, index));
    }
}
