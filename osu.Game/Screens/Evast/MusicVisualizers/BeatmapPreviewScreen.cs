using osu.Framework.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Screens.Evast.Particles;
using osu.Game.Screens.Evast.UserInterface;
using osuTK;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class BeatmapPreviewScreen : EvastScreen
    {
        private const int circle_size = 400;

        public BeatmapPreviewScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new SpaceParticlesContainer(),
                new ParallaxContainer
                {
                    ParallaxAmount = -0.0025f,
                    Children = new Drawable[]
                    {
                        new MusicCircularVisualizer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            DegreeValue = 120,
                            BarWidth = 2,
                            BarsAmount = 40,
                            CircleSize = circle_size,
                        },
                        new MusicCircularVisualizer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            DegreeValue = 120,
                            Rotation = 120,
                            BarWidth = 2,
                            BarsAmount = 40,
                            CircleSize = circle_size,
                        },
                        new MusicCircularVisualizer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            DegreeValue = 120,
                            Rotation = 240,
                            BarWidth = 2,
                            BarsAmount = 40,
                            CircleSize = circle_size,
                        },
                        new BeatmapCircleCard
                        {
                            Size = new Vector2(circle_size),
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        }
                    }
                }
            });
        }
    }
}
