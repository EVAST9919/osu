using osuTK;
using osuTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Bindables;

namespace osu.Game.Screens.Evast.Pixels
{
    public class PixelField : Container
    {
        protected virtual Pixel CreateNewPixel(int size) => new Pixel(size);

        protected readonly Pixel[,] Pixels;

        public double UpdateDelay { set; get; }

        protected readonly int XCount;
        protected readonly int YCount;

        public PixelField(int xCount, int yCount, int pixelSize = 15, int spacing = 2)
        {
            XCount = xCount;
            YCount = yCount;
            UpdateDelay = 200;

            Pixels = new Pixel[xCount, yCount];

            Size = new Vector2(xCount * pixelSize + spacing * (xCount - 1), yCount * pixelSize + spacing * (yCount - 1));

            for (int y = 0; y < yCount; y++)
            {
                for (int x = 0; x < xCount; x++)
                {
                    Pixels[x, y] = CreateNewPixel(pixelSize);
                    Pixels[x, y].Position = new Vector2(x * pixelSize + spacing * x, y * pixelSize + spacing * y);

                    Add(Pixels[x, y]);
                }
            }
        }

        protected virtual void OnRestart()
        {
        }

        protected virtual void OnPause()
        {
        }

        protected virtual void OnContinue()
        {
            if (!isGoing)
            {
                isGoing = true;
                NewUpdate();
            }
        }

        protected virtual void OnStop()
        {
        }

        protected virtual void OnNewUpdate()
        {
        }

        private bool isGoing;

        public void Restart()
        {
            Scheduler.CancelDelayedTasks();
            OnRestart();

            isGoing = true;

            NewUpdate();
        }

        public void Stop()
        {
            Scheduler.CancelDelayedTasks();
            OnStop();

            isGoing = false;
        }

        public void Pause()
        {
            Scheduler.CancelDelayedTasks();
            OnPause();

            isGoing = false;
        }

        public void Continue() => OnContinue();

        public void NewUpdate()
        {
            OnNewUpdate();

            if (isGoing)
                Scheduler.AddDelayed(NewUpdate, UpdateDelay);
        }

        protected class Pixel : Box
        {
            public readonly BindableBool IsActive = new BindableBool();

            public Pixel(int size)
            {
                Size = new Vector2(size);
                Colour = Color4.Black.Opacity(170);
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();
                IsActive.BindValueChanged(_ => OnActiveChanged(), true);
            }

            protected virtual void OnActiveChanged()
            {
                Colour = IsActive.Value ? Color4.White : Color4.Black.Opacity(170);
            }
        }
    }
}
