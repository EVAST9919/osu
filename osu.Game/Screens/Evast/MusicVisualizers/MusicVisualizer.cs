using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicVisualizer : Container
    {
        [Resolved]
        private Bindable<WorkingBeatmap> beatmap { get; set; }

        private int updateDelay = 1;

        public int UpdateDelay
        {
            get => updateDelay;
            set
            {
                updateDelay = value;

                if (!IsLoaded)
                    return;

                Restart();
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Start();
        }

        protected void Start() => updateAmplitudes();

        private void updateAmplitudes()
        {
            var frequencyAmplitudes = beatmap.Value.Track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256];
            OnAmplitudesUpdate(frequencyAmplitudes);
            Scheduler.AddDelayed(updateAmplitudes, UpdateDelay);
        }

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);

        protected void Restart()
        {
            Scheduler.CancelDelayedTasks();
            Start();
        }
    }
}
