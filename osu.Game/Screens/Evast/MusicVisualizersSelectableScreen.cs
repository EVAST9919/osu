using System.Collections.Generic;
using osu.Framework.Screens;
using osu.Game.Screens.Evast.MusicVisualizers;

namespace osu.Game.Screens.Evast
{
    public class MusicVisualizersSelectableScreen : EvastSelectableScreen
    {
        protected override IReadOnlyList<Button> GetButtons() => new[]
        {
            new Button("Circular", () => this.Push(new CircularVisualizerTestScreen())),
            new Button("Linear", () => this.Push(new LinearVisualizerTestScreen())),
            new Button("Variations", () => this.Push(new MusicVisualizerVariationsTestScreen())),
            new Button("Bar variations", () => this.Push(new MusicBarVisualizersVariationsTestScreen())),
            new Button("Beatmap preview", () => this.Push(new BeatmapPreviewScreen())),
            new Button("Music intensity", () => this.Push(new MusicIntensityScreen()))
        };
    }
}
