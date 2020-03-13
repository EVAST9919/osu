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
    public abstract class WebHelperScreen : EvastScreen
    {
        private readonly OsuTextBox textBox;
        private readonly LoadingLayer loading;
        private readonly OsuButton commitButton;
        protected readonly TextFlowContainer Text;

        private GetAPIDataRequest request;

        [Resolved]
        private IAPIProvider api { get; set; }

        public WebHelperScreen()
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
                },
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
