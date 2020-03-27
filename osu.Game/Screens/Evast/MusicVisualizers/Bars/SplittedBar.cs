using osuTK;
using osuTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using System.Linq;

namespace osu.Game.Screens.Evast.MusicVisualizers.Bars
{
    public class SplittedBar : BasicBar
    {
        private const int spacing = 2;
        private const int piece_height = 2;

        private FillFlowContainer piecesContainer;

        protected override Drawable CreateContent() => piecesContainer = new FillFlowContainer
        {
            Anchor = Anchor.BottomCentre,
            Origin = Anchor.BottomCentre,
            AutoSizeAxes = Axes.Y,
            RelativeSizeAxes = Axes.X,
            Direction = FillDirection.Vertical,
            Spacing = new Vector2(0, spacing)
        };

        protected override void Update()
        {
            base.Update();

            var newPiecesCount = (int)(Height / (piece_height + spacing)) + 1;

            var diff = newPiecesCount - piecesContainer.Count;

            if (diff == 0)
                return;

            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                    piecesContainer.Add(getPiece());

                return;
            }

            for (int i = diff; i < 0; i++)
                piecesContainer.Remove(piecesContainer.AliveChildren.First());
        }

        private Container getPiece() => new Container
        {
            Origin = Anchor.BottomCentre,
            RelativeSizeAxes = Axes.X,
            Height = piece_height,
            Child = new Box
            {
                EdgeSmoothness = Vector2.One,
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White,
            }
        };
    }
}
