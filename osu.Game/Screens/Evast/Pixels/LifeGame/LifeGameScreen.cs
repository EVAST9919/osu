using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;

namespace osu.Game.Screens.Evast.Pixels.LifeGame
{
    public class LifeGameScreen : EvastTestScreen
    {
        private readonly GameOfLifePlayfield playfield;

        public LifeGameScreen()
        {
            Add(new ContentAdjustmentContainer
            {
                Child = playfield = new GameOfLifePlayfield(300, 300)
                {
                    RelativeSizeAxes = Axes.Both
                }
            });
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            AddButton(@"Pause Simulation", playfield.Pause);
            AddButton(@"Start Simulation", playfield.Continue);
            AddButton(@"Create Random Map", playfield.CreateRandomMap);
            AddButton(@"Clear Map", playfield.ClearMap);
            AddSlider(@"Update Delay", 5.0, 200, 100, d => playfield.UpdateDelay = d);
        }

        private class ContentAdjustmentContainer : Container
        {
            protected override Container<Drawable> Content => content;

            private readonly Container content;

            public ContentAdjustmentContainer()
            {
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;
                RelativeSizeAxes = Axes.Both;
                InternalChild = content = new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    FillMode = FillMode.Fit,
                };
            }
        }
    }
}
