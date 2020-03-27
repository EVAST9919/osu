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
    public abstract class WebHelperScreen : EvastVisualScreen
    {
        private const int search_bar_height = 30;
        private const int side_margin = 20;

        public override bool CursorVisible => true;

        private readonly OsuTextBox textBox;
        private readonly LoadingLayer loading;
        private readonly OsuButton commitButton;
        protected readonly TextFlowContainer Text;

        private GetAPIDataRequest request;

        [Resolved]
        private IAPIProvider api { get; set; }

        public WebHelperScreen()
        {
            AddInternal(new Container
            {
                RelativeSizeAxes = Axes.Both,
                Padding = new MarginPadding(side_margin),
                Children = new Drawable[]
                {
                    new FillFlowContainer
                    {
                        Anchor = Anchor.TopCentre,
                        Origin = Anchor.TopCentre,
                        AutoSizeAxes = Axes.Both,
                        Spacing = new Vector2(5, 0),
                        Children = new Drawable[]
                        {
                            new OsuSpriteText
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Text = CreateUri().ToString()
                            },
                            textBox = new OsuTextBox
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Size = new Vector2(400, 30),
                            },
                            commitButton = new OsuButton
                            {
                                Anchor = Anchor.CentreLeft,
                                Origin = Anchor.CentreLeft,
                                Size = new Vector2(100, 30),
                                Text = "Get Data",
                                Action = getData,
                            }
                        }
                    },
                    new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding
                        {
                            Top = search_bar_height + side_margin
                        },
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
                                Child = Text = new TextFlowContainer
                                {
                                    AutoSizeAxes = Axes.Both,
                                    Direction = FillDirection.Full,

                                }
                            },
                            loading = new LoadingLayer(),
                        }
                    }
                }
            });

            textBox.OnCommit += (u, v) => commitButton.Click();
        }

        protected abstract void OnRequestSuccess(string result);

        protected virtual void OnRequestFailure(Exception e) => Text.Text = e.Message;

        protected virtual Uri CreateUri() => new Uri("https://osu.ppy.sh/");

        private void getData()
        {
            request?.Cancel();
            loading.Show();

            request = new GetAPIDataRequest(textBox.Text, CreateUri());
            request.Success += result =>
            {
                Text.Clear();
                OnRequestSuccess(result);
                Console.WriteLine(result);
                loading.Hide();
            };

            request.Failure += result =>
            {
                Text.Clear();
                OnRequestFailure(result);
                Console.WriteLine(result);
                loading.Hide();
            };

            api.Queue(request);
        }
    }
}
