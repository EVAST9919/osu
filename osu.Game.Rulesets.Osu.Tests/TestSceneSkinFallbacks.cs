// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Sample;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.OpenGL.Textures;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Timing;
using osu.Game.Audio;
using osu.Game.Beatmaps;
using osu.Game.Configuration;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Skinning;
using osu.Game.Storyboards;
using osu.Game.Tests.Visual;

namespace osu.Game.Rulesets.Osu.Tests
{
    [TestFixture]
    public class TestSceneSkinFallbacks : TestSceneOsuPlayer
    {
        private readonly TestSource testUserSkin;
        private readonly BeatmapTestSource testBeatmapSkin;

        public TestSceneSkinFallbacks()
        {
            testUserSkin = new TestSource("user");
            testBeatmapSkin = new BeatmapTestSource();
        }

        [Test]
        public void TestBeatmapSkinDefault()
        {
            AddStep("enable user provider", () => testUserSkin.Enabled = true);

            AddStep("enable beatmap skin", () => LocalConfig.Set<bool>(OsuSetting.BeatmapSkins, true));
            checkNextHitObject("beatmap");

            AddStep("disable beatmap skin", () => LocalConfig.Set<bool>(OsuSetting.BeatmapSkins, false));
            checkNextHitObject("user");

            AddStep("disable user provider", () => testUserSkin.Enabled = false);
            checkNextHitObject(null);
        }

        private void checkNextHitObject(string skin) =>
            AddUntilStep($"check skin from {skin}", () =>
            {
                var firstObject = Player.DrawableRuleset.Playfield.HitObjectContainer.AliveObjects.OfType<DrawableHitCircle>().FirstOrDefault();

                if (firstObject == null)
                    return false;

                var skinnable = firstObject.ApproachCircle.Child as SkinnableDrawable;

                if (skin == null && skinnable?.Drawable is Sprite)
                    // check for default skin provider
                    return true;

                var text = skinnable?.Drawable as SpriteText;

                return text?.Text == skin;
            });

        [Resolved]
        private AudioManager audio { get; set; }

        protected override TestPlayer CreatePlayer(Ruleset ruleset) => new SkinProvidingPlayer(testUserSkin);

        protected override WorkingBeatmap CreateWorkingBeatmap(IBeatmap beatmap, Storyboard storyboard = null) => new CustomSkinWorkingBeatmap(beatmap, storyboard, Clock, audio, testBeatmapSkin);

        public class CustomSkinWorkingBeatmap : ClockBackedTestWorkingBeatmap
        {
            private readonly IBeatmapSkin skin;

            public CustomSkinWorkingBeatmap(IBeatmap beatmap, Storyboard storyboard, IFrameBasedClock frameBasedClock, AudioManager audio, IBeatmapSkin skin)
                : base(beatmap, storyboard, frameBasedClock, audio)
            {
                this.skin = skin;
            }

            protected override IBeatmapSkin GetSkin() => skin;
        }

        public class SkinProvidingPlayer : TestPlayer
        {
            private readonly TestSource userSkin;

            public SkinProvidingPlayer(TestSource userSkin)
            {
                this.userSkin = userSkin;
            }

            private DependencyContainer dependencies;

            protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent)
            {
                dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

                dependencies.CacheAs<ISkinSource>(userSkin);

                return dependencies;
            }
        }

        private class BeatmapTestSource : TestSource, IBeatmapSkin
        {
            public BeatmapTestSource()
                : base("beatmap")
            {
            }
        }

        public class TestSource : ISkinSource
        {
            private readonly string identifier;

            public TestSource(string identifier)
            {
                this.identifier = identifier;
            }

            public Drawable GetDrawableComponent(ISkinComponent component)
            {
                if (!enabled) return null;

                return new OsuSpriteText
                {
                    Text = identifier,
                    Font = OsuFont.Default.With(size: 30),
                };
            }

            public Texture GetTexture(string componentName, WrapMode wrapModeS, WrapMode wrapModeT) => null;

            public SampleChannel GetSample(ISampleInfo sampleInfo) => null;

            public TValue GetValue<TConfiguration, TValue>(Func<TConfiguration, TValue> query) where TConfiguration : SkinConfiguration => default;
            public IBindable<TValue> GetConfig<TLookup, TValue>(TLookup lookup) => null;

            public event Action SourceChanged;

            private bool enabled = true;

            public bool Enabled
            {
                get => enabled;
                set
                {
                    if (value == enabled)
                        return;

                    enabled = value;
                    SourceChanged?.Invoke();
                }
            }
        }
    }
}
