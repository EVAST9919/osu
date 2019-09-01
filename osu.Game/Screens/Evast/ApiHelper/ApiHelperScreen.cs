using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics.UserInterface;
using osu.Game.Online.API;
using osuTK;
using osuTK.Graphics;
using System;

namespace osu.Game.Screens.Evast.ApiHelper
{
    public class ApiHelperScreen : EvastScreen
    {
        private readonly OsuTextBox textBox;
        private readonly DimmedLoadingLayer loading;
        private readonly TextFlowContainer text;

        private GetAPIDataRequest request;

        [Resolved]
        private IAPIProvider api { get; set; }

        public ApiHelperScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                new FillFlowContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, 50),
                    Children = new Drawable[]
                    {
                        new FillFlowContainer
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            AutoSizeAxes = Axes.Both,
                            Spacing = new Vector2(5, 0),
                            Children = new Drawable[]
                            {
                                new OsuSpriteText
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Text = "https://osu.ppy.sh/api/v2/"
                                },
                                textBox = new OsuTextBox
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(400, 30),
                                },
                                new OsuButton
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(100, 30),
                                    Text = "Get Data",
                                    Action = () => getGata(),
                                }
                            }
                        },
                        new Container
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2 (800, 400),
                            Children = new Drawable[]
                            {
                                new Box
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Colour = Color4.Black.Opacity(100),
                                },
                                new BasicScrollContainer
                                {
                                    RelativeSizeAxes = Axes.Both,
                                    Child = text = new TextFlowContainer
                                    {
                                        AutoSizeAxes = Axes.Both,
                                        Direction = FillDirection.Full,
                                        
                                    }
                                },
                                loading = new DimmedLoadingLayer(),
                            }
                        }
                    }
                },
            });
        }

        private void getGata()
        {
            request?.Cancel();
            loading.Show();

            request = new GetAPIDataRequest(textBox.Text);
            request.Success += result =>
            {
                Console.Clear();
                Console.Write(result);
                text.Text = result;
                loading.Hide();
            };

            request.Failure += result =>
            {
                Console.Clear();
                Console.Write(result.Message);
                text.Text = result.Message;
                loading.Hide();
            };

            api.Queue(request);
        }
    }
}
