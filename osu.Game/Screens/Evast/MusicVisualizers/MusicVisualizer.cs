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
            set
            {
                if (updateDelay == value)
                    return;
                updateDelay = value;

                if (!IsLoaded)
                    return;

                Restart();
            }
            get { return updateDelay; }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Start();
        }

        private void updateAmplitudes()
        {
            var frequencyAmplitudes = beatmap.Value.Track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256];
            OnAmplitudesUpdate(frequencyAmplitudes);
            Scheduler.AddDelayed(updateAmplitudes, UpdateDelay);
        }

        protected void Start() => updateAmplitudes();

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);

        protected void Restart()
        {
            Scheduler.CancelDelayedTasks();
            Start();
        }
    }
}
