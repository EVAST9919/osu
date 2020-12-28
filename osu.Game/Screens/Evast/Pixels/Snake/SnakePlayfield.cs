using osuTK.Graphics;
using osuTK.Input;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osu.Framework.Bindables;

namespace osu.Game.Screens.Evast.Pixels.Snake
{
    public class SnakePlayfield : PixelField
    {
        protected override Pixel CreateNewPixel(int size) => new SnakePixel(size);

        private SnakePixel getSnakePixel(int x, int y) => Pixels[x, y] as SnakePixel;

        public SnakePlayfield()
            : base(20, 20, 25)
        {
        }

        private int snakeLength;
        private int headX;
        private int headY;
        private SnakeDirection direction;

        private bool hasFailed;

        protected override void OnStop()
        {
            base.OnStop();

            hasFailed = false;

            for (int y = 0; y < YCount; y++)
            {
                for (int x = 0; x < XCount; x++)
                {
                    var pixel = getSnakePixel(x, y);
                    pixel.Steps = 0;
                    pixel.IsFood.Value = false;
                }
            }
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            OnStop();

            snakeLength = 1;
            setHead(XCount / 2, YCount / 2);
            direction = SnakeDirection.Right;

            placeFood();
        }

        protected override void OnContinue()
        {
            if (hasFailed)
            {
                Restart();
                return;
            }

            base.OnContinue();
        }

        private void onFail()
        {
            Pause();
            hasFailed = true;
        }

        protected override void OnNewUpdate()
        {
            base.OnNewUpdate();

            for (int y = 0; y < YCount; y++)
                for (int x = 0; x < XCount; x++)
                    if (getSnakePixel(x, y).IsActive.Value)
                        getSnakePixel(x, y).Steps--;

            newMove();
        }

        private void newMove()
        {
            switch (direction)
            {
                case SnakeDirection.Left:
                    headX--;
                    if (headX < 0)
                        headX = XCount - 1;
                    break;
                case SnakeDirection.Right:
                    headX++;
                    if (headX > XCount - 1)
                        headX = 0;
                    break;
                case SnakeDirection.Up:
                    headY--;
                    if (headY < 0)
                        headY = YCount - 1;
                    break;
                case SnakeDirection.Down:
                    headY++;
                    if (headY > YCount - 1)
                        headY = 0;
                    break;
            }

            var head = getSnakePixel(headX, headY);

            if (head.Steps > 0)
            {
                head.FadeColour(Color4.Red);
                onFail();
                return;
            }

            if (head.IsFood.Value)
            {
                placeFood();

                for (int y = 0; y < YCount; y++)
                    for (int x = 0; x < XCount; x++)
                        if (getSnakePixel(x, y).IsActive.Value)
                            getSnakePixel(x, y).Steps++;

                snakeLength++;
            }

            head.SetActive(snakeLength);
        }

        private void placeFood()
        {
            int x = RNG.Next(XCount);
            int y = RNG.Next(YCount);

            if (getSnakePixel(x, y).IsActive.Value || getSnakePixel(x, y).IsFood.Value)
            {
                placeFood();
                return;
            }

            getSnakePixel(x, y).IsFood.Value = true;
        }

        private void setHead(int x, int y)
        {
            headX = x;
            headY = y;
        }

        private void changeDirectionRequest(SnakeDirection newDirection)
        {
            if (hasFailed)
                return;

            Scheduler.CancelDelayedTasks();
            direction = newDirection;
            NewUpdate();
        }

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.Up:
                        if (direction == SnakeDirection.Down || direction == SnakeDirection.Up)
                            return true;
                        changeDirectionRequest(SnakeDirection.Up);
                        return true;
                    case Key.Down:
                        if (direction == SnakeDirection.Down || direction == SnakeDirection.Up)
                            return true;
                        changeDirectionRequest(SnakeDirection.Down);
                        return true;
                    case Key.Left:
                        if (direction == SnakeDirection.Left || direction == SnakeDirection.Right)
                            return true;
                        changeDirectionRequest(SnakeDirection.Left);
                        return true;
                    case Key.Right:
                        if (direction == SnakeDirection.Left || direction == SnakeDirection.Right)
                            return true;
                        changeDirectionRequest(SnakeDirection.Right);
                        return true;
                }
            }

            return base.OnKeyDown(e);
        }

        private class SnakePixel : Pixel
        {
            public readonly BindableBool IsFood = new BindableBool();

            private int steps;
            public int Steps
            {
                get => steps;
                set
                {
                    steps = value;
                    IsActive.Value &= steps != 0;
                }
            }

            public SnakePixel(int size)
                : base(size)
            {
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                IsFood.BindValueChanged(_ => OnActiveChanged(), true);
            }

            protected override void OnActiveChanged()
            {
                if (IsFood.Value)
                {
                    Colour = Color4.Green;
                    return;
                }

                base.OnActiveChanged();
            }

            public void SetActive(int steps)
            {
                IsFood.Value = false;
                IsActive.Value = true;
                this.steps = steps;
            }
        }

        private enum SnakeDirection
        {
            Right,
            Left,
            Up,
            Down
        }
    }
}
