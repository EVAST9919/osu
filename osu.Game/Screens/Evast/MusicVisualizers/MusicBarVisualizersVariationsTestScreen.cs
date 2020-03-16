using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public class MusicBarVisualizersVariationsTestScreen : EvastScreen
    {
        public MusicBarVisualizersVariationsTestScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new FallBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsAmount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                    X = -400,
                },
                new SplittedBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsAmount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                },
                new CircularBarVisualizer()
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    BarsAmount = 100,
                    BarWidth = 5,
                    CircleSize = 250,
                    X = 400,
                }
            });
        }

        private class CircularBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new CircularBar();

            private class CircularBar : BasicBar
            {
                public CircularBar()
                {
                    Masking = true;
                }

                protected override void LoadComplete()
                {
                    base.LoadComplete();
                    CornerRadius = Width / 2;
                }

                protected override float ValueFormula(float amplitudeValue, float valueMultiplier) => Width + base.ValueFormula(amplitudeValue, valueMultiplier);
            }
        }

        private class SplittedBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new SplittedBar();

            private class SplittedBar : BasicBar
            {
                private const int spacing = 2;
                private const int piece_height = 2;

                private Container piecesContainer;

                private int piecesCount = -1;

                protected override Drawable CreateContent() => piecesContainer = new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X
                };

                protected override void Update()
                {
                    base.Update();

                    var newPiecesCount = (int)(Height / (piece_height + spacing));

                    if (piecesCount == newPiecesCount)
                        return;

                    piecesCount = newPiecesCount;

                    piecesContainer.Clear(true);

                    for (int i = 0; i <= newPiecesCount; i++)
                    {
                        piecesContainer.Add(new Container
                        {
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = piece_height,
                            Y = i * (piece_height + spacing),
                            Child = new Box
                            {
                                EdgeSmoothness = Vector2.One,
                                RelativeSizeAxes = Axes.Both,
                                Colour = Color4.White,
                            }
                        });
                    }
                }
            }
        }

        private class FallBarVisualizer : MusicCircularVisualizer
        {
            protected override BasicBar CreateBar() => new FallBar();

            private class FallBar : BasicBar
            {
                private Container mainBar;
                private Container fallingPiece;

                protected override Drawable CreateContent() => new Container
                {
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Children = new Drawable[]
                    {
                        mainBar = new Container
                        {
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.X,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Color4.White,
                                EdgeSmoothness = Vector2.One,
                            }
                        },
                        fallingPiece = new Container
                        {
                            Origin = Anchor.BottomCentre,
                            RelativeSizeAxes = Axes.X,
                            Height = 2,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = Color4.White,
                                EdgeSmoothness = Vector2.One,
                            }
                        }
                    },
                };

                public override void SetValue(float amplitudeValue, float valueMultiplier, int smoothness)
                {
                    var newValue = ValueFormula(amplitudeValue, valueMultiplier);

                    if (newValue > mainBar.Height)
                    {
                        mainBar.ResizeHeightTo(newValue)
                            .Then()
                            .ResizeHeightTo(0, smoothness);
                    }

                    if (mainBar.Height > -fallingPiece.Y)
                    {
                        fallingPiece.MoveToY(-newValue)
                            .Then()
                            .MoveToY(0, smoothness * 6);
                    }
                }
            }
        }
    }
}
