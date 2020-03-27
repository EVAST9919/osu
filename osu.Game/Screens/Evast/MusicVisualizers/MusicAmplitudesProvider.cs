using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicAmplitudesProvider : CurrentBeatmapProvider
    {
        private bool isUpdating;

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Start();
        }

        protected override void Update()
        {
            base.Update();

            if (!isUpdating || !IsLoaded)
                return;

            OnAmplitudesUpdate(Beatmap.Value.Track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256]);
        }

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);

        protected void Start() => isUpdating = true;

        protected void Pause() => isUpdating = false;
    }
}
