using osu.Game.Graphics.UserInterface;
using osu.Framework.Graphics;

namespace osu.Game.Screens.Evast.ExportArea
{
    public class ExportAreaScreen : EvastScreen
    {
        public ExportAreaScreen()
        {
            AddInternal(new SearchTextBox
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Width = 500,
                Margin = new MarginPadding { Top = 100 },
            });
        }
    }
}
