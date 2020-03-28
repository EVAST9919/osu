using osu.Framework.Bindables;
using osu.Game.Screens.Evast.Helpers;

namespace osu.Game.Screens.Evast.MusicVisualizers
{
    public abstract class MusicAmplitudesProvider : CurrentBeatmapProvider
    {
        public readonly BindableBool IsKiai = new BindableBool();

        protected override void Update()
        {
            base.Update();

            var track = Beatmap.Value?.Track;
            OnAmplitudesUpdate(track?.CurrentAmplitudes.FrequencyAmplitudes ?? new float[256]);
            IsKiai.Value = Beatmap.Value?.Beatmap.ControlPointInfo.EffectPointAt(track?.CurrentTime ?? 0).KiaiMode ?? false;
        }

        protected abstract void OnAmplitudesUpdate(float[] amplitudes);
    }
}
