using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Game.Screens.Evast.MusicVisualizers.Bars;
using osu.Game.Screens.Evast.Particles;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.NumbersGame
{
    public class NumbersGameScreen : EvastVisualScreen
    {
        private const int music_visualizer_radius = 1000;
        private const int music_visualizer_bars_count = 70;

        private readonly NumbersPlayfield playfield;
        private readonly OsuClickableContainer resetButton;
        private readonly OsuSpriteText scoreText;

        public NumbersGameScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new ParticlesContainer(),
                new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            X = -70,
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new CircularVisualizer
                                {
                                    IsReversed = true,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    DegreeValue = 45,
                                    Rotation = 40,
                                    BarWidth = 3,
                                    BarsCount = music_visualizer_bars_count,
                                    CircleSize = music_visualizer_radius,
                                },
                                new CircularVisualizer
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    DegreeValue = 45,
                                    Rotation = 95,
                                    BarWidth = 3,
                                    BarsCount = music_visualizer_bars_count,
                                    CircleSize = music_visualizer_radius,
                                }
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            X = 70,
                            RelativeSizeAxes = Axes.Both,
                            Children = new Drawable[]
                            {
                                new CircularVisualizer
                                {
                                    IsReversed = true,
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    DegreeValue = 45,
                                    Rotation = 220,
                                    BarWidth = 3,
                                    BarsCount = music_visualizer_bars_count,
                                    CircleSize = music_visualizer_radius,
                                },
                                new CircularVisualizer
                                {
                                    Anchor = Anchor.Centre,
                                    Origin = Anchor.Centre,
                                    DegreeValue = 45,
                                    Rotation = 275,
                                    BarWidth = 3,
                                    BarsCount = music_visualizer_bars_count,
                                    CircleSize = music_visualizer_radius,
                                }
                            }
                        }
                    }
                },
                resetButton = new OsuClickableContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    AutoSizeAxes = Axes.Both,
                    CornerRadius = 6,
                    Masking = true,
                    Margin = new MarginPadding { Top = 240 },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(187, 173, 160, 255)
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = @"Restart".ToUpper(),
                            Font = OsuFont.GetFont(size: 25, weight: FontWeight.Bold),
                            Colour = new Color4(119, 110, 101, 255),
                            Shadow = false,
                            Margin = new MarginPadding { Horizontal = 10, Vertical = 10 },
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    AutoSizeAxes = Axes.Y,
                    Width = 150,
                    CornerRadius = 6,
                    Masking = true,
                    Margin = new MarginPadding { Bottom = 240 },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = new Color4(187, 173, 160, 255)
                        },
                        scoreText = new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.GetFont(size: 40, weight: FontWeight.Bold),
                            Text = "0",
                            Colour = new Color4(119, 110, 101, 255),
                            Shadow = false,
                            Margin = new MarginPadding { Horizontal = 10, Vertical = 10 },
                        }
                    }
                },
                playfield = new NumbersPlayfield
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            });

            resetButton.Action = playfield.Restart;
            playfield.Score.BindValueChanged(onScoreChanged, true);
        }

        private void onScoreChanged(ValueChangedEvent<int> newScore)
        {
            scoreText.Text = newScore.NewValue.ToString();
        }

        private class CircularVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();
        }
    }
}
