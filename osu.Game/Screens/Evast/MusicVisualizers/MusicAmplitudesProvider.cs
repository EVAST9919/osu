using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicAmplitudesProvider : CurrentBeatmapProvider
    {
        protected override void LoadComplete()
        {
            base.LoadComplete();
            Start();
        }

        protected void Start() => updateAmplitudes();

        private void updateAmplitudes()
        {
            OnAmplitudesUpdate(Beatmap.Value.Track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256]);
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
