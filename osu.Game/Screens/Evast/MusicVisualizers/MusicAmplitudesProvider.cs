using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicAmplitudesProvider : CurrentBeatmapProvider
    {
        protected override void Update()
        {
            base.Update();
            OnAmplitudesUpdate(Beatmap.Value.Track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256]);
        }

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);
    }
}
