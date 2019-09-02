namespace osu.Game.Screens.Evast.Pixels.Tetris
{
    public class TetrisPlayfield : PixelField
    {
        private Figures nextFigure;
        private Figure currentFigure;

        private int figureXPos = 4;
        private int figureYPos = 0;

        public TetrisPlayfield()
            : base(10, 20, 25)
        {
        }

        protected override void OnRestart()
        {
            cleanField();
            generateNextFigure();
            placeNewFigure();
        }

        protected override void OnNewUpdate()
        {
            base.OnNewUpdate();

            currentFigure.BottomLeftCorner = Pixels[figureXPos, figureYPos++];
        }

        private void generateNextFigure()
        {
            //nextFigure = (Figures)RNG.Next(6);
            nextFigure = Figures.I;
        }

        private void placeNewFigure()
        {
            currentFigure = new IFigure
            {
                BottomLeftCorner = Pixels[figureXPos, figureYPos],
            };
        }

        private void cleanField()
        {
            for (int y = 0; y < YCount; y++)
                for (int x = 0; x < XCount; x++)
                    Pixels[x, y].IsActive = false;
        }

        private enum Figures
        {
            I,
            J,
            L,
            O,
            S,
            Z,
            T
        }

        private abstract class Figure
        {
            public Pixel BottomLeftCorner;

            public abstract int[,] GetMap();
        }

        private class IFigure : Figure
        {
            public override int[,] GetMap()
            {
                return new int[,]
                {
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 },
                    { 0, 1, 0, 0 },
                };
            }
        }
    }
}
