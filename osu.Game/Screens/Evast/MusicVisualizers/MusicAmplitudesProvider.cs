using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicAmplitudesProvider : Container
    {
        [Resolved]
        private Bindable<WorkingBeatmap> beatmap { get; set; }

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
            Scheduler.AddDelayed(updateAmplitudes, 1);
        }

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);

        protected void Restart()
        {
            Scheduler.CancelDelayedTasks();
            Start();
        }
    }
}
