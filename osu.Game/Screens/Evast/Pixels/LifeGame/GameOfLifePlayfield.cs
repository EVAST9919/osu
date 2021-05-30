using System;
using System.Linq;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.OpenGL.Vertices;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Input.Events;
using osu.Framework.Layout;
using osu.Framework.Utils;
using osu.Game.Screens.Evast.Helpers;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;

namespace osu.Game.Screens.Evast.Pixels.LifeGame
{
    public class GameOfLifePlayfield : Container
    {
        private readonly Cell[] cells;
        private readonly Cell[] lastGenCells;

        public double UpdateDelay { set; get; } = 200;

        private readonly int sizeX;
        private readonly int sizeY;
        private readonly LayoutValue layout = new LayoutValue(Invalidation.DrawSize);
        private readonly BufferedContainer bufferedContainer;
        private readonly GamePlayfield playfield;

        public GameOfLifePlayfield(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            cells = new Cell[sizeX * sizeY];
            lastGenCells = new Cell[sizeX * sizeY];

            for (int i = 0; i < cells.Length; i++)
            {
                cells[i] = new Cell();
                lastGenCells[i] = new Cell();
            }

            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Black
                },
                bufferedContainer = new BufferedContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CacheDrawnFrameBuffer = true,
                    RedrawOnScale = false,
                    Child = playfield = new GamePlayfield(sizeX, sizeY)
                }
            };

            AddLayout(layout);
        }

        protected override void Update()
        {
            base.Update();

            if (!layout.IsValid)
            {
                playfield.UpdateState(cells);
                bufferedContainer.ForceRedraw();

                layout.Validate();
            }
        }

        public void Pause()
        {
            Scheduler.CancelDelayedTasks();
        }

        public void Continue()
        {
            Scheduler.AddDelayed(onNewUpdate, UpdateDelay);
        }

        public void CreateRandomMap()
        {
            ClearMap();

            for (int i = 0; i < cells.Length; i++)
                cells[i].IsAlive = RNG.NextBool();

            layout.Invalidate();
        }

        public void ClearMap()
        {
            for (int i = 0; i < cells.Length; i++)
            {
                lastGenCells[i].IsAlive = false;
                cells[i].IsAlive = false;
            }

            layout.Invalidate();
        }

        private void onNewUpdate()
        {
            saveLastGeneration();

            for (int i = 0; i < cells.Length; i++)
            {
                int x = i < sizeX ? i : i % sizeX;
                int y = i / sizeX;

                int nearbyCellsAmount = countNeighbours(x, y);

                if (!lastGenCells[i].IsAlive && nearbyCellsAmount == 3)
                {
                    cells[i].IsAlive = true;
                    continue;
                }

                if (lastGenCells[i].IsAlive && !(nearbyCellsAmount == 2 || nearbyCellsAmount == 3))
                    cells[i].IsAlive = false;
            }

            layout.Invalidate();
            Scheduler.AddDelayed(onNewUpdate, UpdateDelay);
        }

        private void saveLastGeneration()
        {
            for (int i = 0; i < cells.Length; i++)
                lastGenCells[i].IsAlive = cells[i].IsAlive;
        }

        private int countNeighbours(int x, int y)
        {
            int amount = 0;

            ////
            int checkableX = x - 1;
            int checkableY = y - 1;

            if (checkableX < 0)
                checkableX = sizeX - 1;
            if (checkableY < 0)
                checkableY = sizeY - 1;

            if (lastGenCells[getArrayIndex(checkableX, checkableY)].IsAlive)
                amount++;

            ////

            if (lastGenCells[getArrayIndex(x, checkableY)].IsAlive)
                amount++;

            ////

            checkableX = x + 1;

            if (checkableX > sizeX - 1)
                checkableX = 0;

            if (lastGenCells[getArrayIndex(checkableX, checkableY)].IsAlive)
                amount++;

            ////

            checkableX = x - 1;

            if (checkableX < 0)
                checkableX = sizeX - 1;

            if (lastGenCells[getArrayIndex(checkableX, y)].IsAlive)
                amount++;

            ////

            checkableX = x + 1;

            if (checkableX > sizeX - 1)
                checkableX = 0;

            if (lastGenCells[getArrayIndex(checkableX, y)].IsAlive)
                amount++;

            ////

            checkableX = x - 1;
            checkableY = y + 1;

            if (checkableX < 0)
                checkableX = sizeX - 1;
            if (checkableY > sizeY - 1)
                checkableY = 0;

            if (lastGenCells[getArrayIndex(checkableX, checkableY)].IsAlive)
                amount++;

            ////

            if (lastGenCells[getArrayIndex(x, checkableY)].IsAlive)
                amount++;

            ////

            checkableX = x + 1;

            if (checkableX > sizeX - 1)
                checkableX = 0;

            if (lastGenCells[getArrayIndex(checkableX, checkableY)].IsAlive)
                amount++;
            ////

            return amount;
        }

        private int getArrayIndex(int x, int y) => y * sizeY + x;

        private int getArrayIndexAtAbsolutePosition(Vector2 position)
        {
            var cellCoords = new Vector2(MathExtensions.Map(position.X, 0, DrawWidth, 0, sizeX), MathExtensions.Map(position.Y, 0, DrawHeight, 0, sizeY));
            return getArrayIndex((int)cellCoords.X, (int)cellCoords.Y);
        }

        protected override bool OnMouseMove(MouseMoveEvent e)
        {
            var state = e.CurrentState.Mouse;

            if (state.Buttons.Contains(MouseButton.Left))
            {
                var index = getArrayIndexAtAbsolutePosition(e.MousePosition);

                if (!cells[index].IsAlive)
                {
                    cells[index].IsAlive = true;
                    layout.Invalidate();
                    return true;
                }
            }

            if (state.Buttons.Contains(MouseButton.Right))
            {
                var index = getArrayIndexAtAbsolutePosition(e.MousePosition);

                if (cells[index].IsAlive)
                {
                    cells[index].IsAlive = false;
                    layout.Invalidate();
                    return true;
                }
            }

            return true;
        }

        private class GamePlayfield : Sprite
        {
            protected override DrawNode CreateDrawNode() => new GamePlayfieldDrawNode(this);

            private Cell[] cells;
            private readonly int sizeX;
            private readonly int sizeY;

            public GamePlayfield(int sizeX, int sizeY)
            {
                this.sizeX = sizeX;
                this.sizeY = sizeY;
                Texture = Texture.WhitePixel;
                RelativeSizeAxes = Axes.Both;
            }

            public void UpdateState(Cell[] cells)
            {
                this.cells = cells;
            }

            private class GamePlayfieldDrawNode : SpriteDrawNode
            {
                public new GamePlayfield Source => (GamePlayfield)base.Source;

                public GamePlayfieldDrawNode(GamePlayfield source)
                    : base(source)
                {
                }

                private Cell[] cells;
                private int sizeX;
                private int sizeY;
                private Vector2 drawSize;

                public override void ApplyState()
                {
                    base.ApplyState();

                    cells = Source.cells;
                    sizeX = Source.sizeX;
                    sizeY = Source.sizeY;
                    drawSize = Source.DrawSize;
                }

                protected override void Blit(Action<TexturedVertex2D> vertexAction)
                {
                    if (cells == null)
                        return;

                    var size = new Vector2(drawSize.X / sizeX, drawSize.Y / sizeY);

                    for (int i = 0; i < cells.Length; i++)
                    {
                        if (!cells[i].IsAlive)
                            continue;

                        int x = i < sizeX ? i : i % sizeX;
                        int y = i / sizeX;

                        var position = new Vector2(x * size.X, y * size.Y);

                        var rectangle = new RectangleF(position, size);

                        var quad = new Quad(
                            Vector2Extensions.Transform(rectangle.TopLeft, DrawInfo.Matrix),
                            Vector2Extensions.Transform(rectangle.TopRight, DrawInfo.Matrix),
                            Vector2Extensions.Transform(rectangle.BottomLeft, DrawInfo.Matrix),
                            Vector2Extensions.Transform(rectangle.BottomRight, DrawInfo.Matrix)
                        );

                        DrawQuad(Texture, quad, Color4.White, null, vertexAction);
                    }
                }
            }
        }

        private struct Cell
        {
            public bool IsAlive;
        }
    }
}
