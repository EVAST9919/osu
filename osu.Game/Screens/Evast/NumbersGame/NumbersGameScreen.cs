// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Graphics.Containers;
using osu.Game.Graphics.Sprites;

namespace osu.Game.Screens.Evast.NumbersGame
{
    public class NumbersGameScreen : EvastScreen
    {
        private readonly NumbersPlayfield playfield;
        private readonly OsuClickableContainer resetButton;
        private readonly OsuSpriteText scoreText;

        public NumbersGameScreen()
        {
            AddRangeInternal(new Drawable[]
            {
                resetButton = new OsuClickableContainer
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.TopCentre,
                    AutoSizeAxes = Axes.Both,
                    CornerRadius = 6,
                    Masking = true,
                    Margin = new MarginPadding { Top = 240 },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = OsuColour.FromHex(@"BBADA0"),
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = @"Restart".ToUpper(),
                            Font = OsuFont.GetFont(size: 25, weight: FontWeight.Bold),
                            Colour = OsuColour.FromHex(@"776E65"),
                            Shadow = false,
                            Margin = new MarginPadding { Horizontal = 10, Top = 20, Bottom = 10 },
                        }
                    }
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.BottomCentre,
                    AutoSizeAxes = Axes.Y,
                    Width = 150,
                    CornerRadius = 6,
                    Masking = true,
                    Margin = new MarginPadding { Bottom = 240 },
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = OsuColour.FromHex(@"BBADA0"),
                        },
                        scoreText = new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Font = OsuFont.GetFont(size: 40, weight: FontWeight.Bold),
                            Text = "0",
                            Colour = OsuColour.FromHex(@"776E65"),
                            Shadow = false,
                            Margin = new MarginPadding { Horizontal = 10, Bottom = 20, Top = 10 },
                        }
                    }
                },
                playfield = new NumbersPlayfield
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                },
            });

            resetButton.Action = playfield.Reset;
            playfield.Score.BindValueChanged(onScoreChanged, true);
        }

        private void onScoreChanged(ValueChangedEvent<int> newScore)
        {
            scoreText.Text = newScore.NewValue.ToString();
        }
    }
}
