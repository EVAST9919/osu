using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicBarsVisualizer : MusicAmplitudesProvider
    {
        protected virtual BasicBar CreateBar() => new BasicBar();

        private int smoothness = 150;
        public int Smoothness
        {
            get => smoothness;
            set
            {
                smoothness = value;

                if (!IsLoaded)
                    return;

                Restart();
            }
        }

        private float barWidth = 4.5f;
        public float BarWidth
        {
            get => barWidth;
            set
            {
                barWidth = value;

                if (!IsLoaded)
                    return;

                foreach (var bar in EqualizerBars)
                    bar.Width = value;
            }
        }

        private int barsCount = 200;
        public int BarsCount
        {
            get => barsCount;
            set
            {
                barsCount = value;

                if (!IsLoaded)
                    return;

                Scheduler.CancelDelayedTasks();
                resetBars();
                Start();
            }
        }

        public float ValueMultiplier { get; set; } = 400;

        protected virtual void ClearBars() => Clear(true);

        private void resetBars()
        {
            ClearBars();
            rearrangeBars();
            AddBars();
        }

        private void rearrangeBars()
        {
            EqualizerBars = new BasicBar[barsCount];
            for (int i = 0; i < barsCount; i++)
            {
                EqualizerBars[i] = CreateBar();
                EqualizerBars[i].Width = barWidth;
            }
        }

        protected int RealAmplitudeFor(int barNumber) => 200 / barsCount * barNumber;

        protected BasicBar[] EqualizerBars;

        public bool IsReversed { get; set; }

        protected override void LoadComplete()
        {
            resetBars();
            base.LoadComplete();
        }

        protected virtual void AddBars() => EqualizerBars.ForEach(Add);

        protected override void OnAmplitudesUpdate(float[] amplitudes)
        {
            for (int i = 0; i < barsCount; i++)
            {
                var currentAmplitude = amplitudes[RealAmplitudeFor(i)];
                EqualizerBars[IsReversed ? barsCount - 1 - i : i].SetValue(currentAmplitude, ValueMultiplier, Smoothness);
            }
        }

        protected class BasicBar : Container
        {
            public BasicBar()
            {
                Child = CreateContent();
            }

            protected virtual Drawable CreateContent() => new Box
            {
                EdgeSmoothness = Vector2.One,
                RelativeSizeAxes = Axes.Both,
                Colour = Color4.White,
            };

            public virtual void SetValue(float amplitudeValue, float valueMultiplier, int softness)
            {
                var newHeight = ValueFormula(amplitudeValue, valueMultiplier);

                // Don't allow resize if new height less than current
                if (newHeight <= Height)
                    return;

                this.ResizeHeightTo(newHeight).Then().ResizeHeightTo(0, softness);
            }

            protected virtual float ValueFormula(float amplitudeValue, float valueMultiplier) => amplitudeValue * valueMultiplier;
        }
    }
}
