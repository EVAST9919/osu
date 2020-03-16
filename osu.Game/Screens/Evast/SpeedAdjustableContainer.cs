using osu.Framework.Graphics.Containers;
using osu.Framework.Timing;

namespace osu.Game.Screens.Evast
{
    public class SpeedAdjustableContainer : Container
    {
        public double Rate
        {
            get => clock.Rate;
            set => clock.Rate = value;
        }

        private readonly StopwatchClock clock;

        public SpeedAdjustableContainer()
        {
            ProcessCustomClock = true;
            Clock = new FramedClock(clock = new StopwatchClock());
            clock.Start();
        }
    }
}
