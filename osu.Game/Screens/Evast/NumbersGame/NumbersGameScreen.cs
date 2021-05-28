using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Testing;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;
using osu.Game.Screens.Evast.Helpers;
using osu.Game.Screens.Evast.MusicVisualizers;
using osu.Game.Screens.Evast.Particles;
using osuTK;
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
                new Controller(),
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

        private class Controller : MusicAmplitudesProvider
        {
            public Controller()
            {
                RelativeSizeAxes = Axes.Both;
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
                            new RoundedMusicVisualizerDrawable
                            {
                                Reversed = { Value = true },
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                BarCount = { Value = music_visualizer_bars_count },
                                Size = new Vector2(music_visualizer_radius),
                                DegreeValue = { Value = 45 },
                                Rotation = 40,
                                BarWidth = { Value = 3 }
                            },
                            new RoundedMusicVisualizerDrawable
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                BarCount = { Value = music_visualizer_bars_count },
                                Size = new Vector2(music_visualizer_radius),
                                DegreeValue = { Value = 45 },
                                Rotation = 95,
                                BarWidth = { Value = 3 }
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
                            new RoundedMusicVisualizerDrawable
                            {
                                Reversed = { Value = true },
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                DegreeValue = { Value = 45 },
                                Rotation = 220,
                                BarWidth = { Value = 3 },
                                BarCount = { Value = music_visualizer_bars_count },
                                Size = new Vector2(music_visualizer_radius),
                            },
                            new RoundedMusicVisualizerDrawable
                            {
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                DegreeValue = { Value = 45 },
                                Rotation = 275,
                                BarWidth = { Value = 3 },
                                BarCount = { Value = music_visualizer_bars_count },
                                Size = new Vector2(music_visualizer_radius),
                            }
                        }
                    }
                };
            }

            protected override void OnAmplitudesUpdate(float[] amplitudes)
            {
                foreach (var c in this.ChildrenOfType<MusicVisualizerDrawable>())
                    c.SetAmplitudes(amplitudes);
            }
        }
    }
}
