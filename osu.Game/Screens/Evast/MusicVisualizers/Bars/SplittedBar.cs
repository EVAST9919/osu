using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;

namespace osu.Game.Screens.Evast.MusicVisualizers.Bars
{
    public class SplittedBar : BasicBar
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
