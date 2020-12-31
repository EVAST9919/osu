using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK.Graphics;
using osuTK;
using osu.Game.Graphics.Sprites;
using osu.Game.Graphics;
using osu.Game.Overlays.Settings;
using osu.Framework.Bindables;
using System;

namespace osu.Game.Screens.Evast.RayMarching
{
    public class CalculatorScreen : EvastScreen
    {
        public CalculatorScreen()
        {
            AddInternal(new Calculator
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre
            });
        }

        private class Calculator : CompositeDrawable
        {
            private const float width = 500;
            private const float padding = 5;
            private const float radius = 50;

            private readonly BindableDouble theta = new BindableDouble
            {
                MinValue = 0,
                MaxValue = 2 * Math.PI
            };

            private readonly BindableDouble phi = new BindableDouble
            {
                MinValue = 0,
                MaxValue = 2 * Math.PI
            };

            private readonly OsuSpriteText result;
            private readonly View top;
            private readonly View side;

            public Calculator()
            {
                AutoSizeAxes = Axes.Y;
                Width = width + padding * 2;
                Masking = true;
                CornerRadius = 4;
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = Color4.Black,
                        Alpha = 0.5f
                    },
                    new Container
                    {
                        Padding = new MarginPadding(5),
                        RelativeSizeAxes = Axes.X,
                        AutoSizeAxes = Axes.Y,
                        Child = new FillFlowContainer
                        {
                            RelativeSizeAxes = Axes.X,
                            AutoSizeAxes = Axes.Y,
                            Direction = FillDirection.Vertical,
                            Spacing = new Vector2(0, 5),
                            Children = new Drawable[]
                            {
                                new OsuSpriteText
                                {
                                    Font = OsuFont.GetFont(size: 20, weight: FontWeight.Bold),
                                    Text = "Position on a sphere".ToUpper()
                                },
                                new OsuSpriteText
                                {
                                    Font = OsuFont.GetFont(),
                                    Text = "Theta (around Z, vertical)"
                                },
                                new SettingsSlider<double>
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Current = theta
                                },
                                new OsuSpriteText
                                {
                                    Font = OsuFont.GetFont(),
                                    Text = "Phi (around Y, horizontal)"
                                },
                                new SettingsSlider<double>
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Current = phi
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    Height = 20,
                                    Child = result = new OsuSpriteText
                                    {
                                        Font = OsuFont.GetFont()
                                    }
                                },
                                new Container
                                {
                                    RelativeSizeAxes = Axes.X,
                                    AutoSizeAxes = Axes.Y,
                                    Children = new Drawable[]
                                    {
                                        top = new View("Top view", "x", Color4.Red, "z", Color4.Blue),
                                        side = new View("Side view", "x", Color4.Red, "y", Color4.Green)
                                        {
                                            RelativePositionAxes = Axes.X,
                                            X = 0.5f,
                                        },
                                        new Box
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            RelativeSizeAxes = Axes.Y,
                                            Width = 0.1f,
                                            EdgeSmoothness = Vector2.One
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            }

            protected override void LoadComplete()
            {
                base.LoadComplete();

                theta.BindValueChanged(_ => updateResult());
                phi.BindValueChanged(_ => updateResult(), true);
            }

            private void updateResult()
            {
                var numResult = RayMarchingExtensions.PositionOnASphere(Vector3.Zero, radius, theta.Value, phi.Value);
                result.Text = $"Position: (X: {numResult.X:0.00}, Y: {numResult.Y:0.00}, Z: {numResult.Z:0.00})";
                top.PointPosition = new Vector2(numResult.X, numResult.Z);
                side.PointPosition = new Vector2(numResult.X, numResult.Y);
            }

            private class View : CompositeDrawable
            {
                public Vector2 PointPosition
                {
                    get => point.Position;
                    set => point.Position = value;
                }

                private readonly Circle point;

                public View(string name, string xAxisName, Color4 xAxisColour, string yAxisName, Color4 yAxisColour)
                {
                    Width = width / 2;
                    Height = width / 2;
                    InternalChildren = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.X,
                            Height = 1,
                            EdgeSmoothness = Vector2.One,
                            Colour = xAxisColour,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.BottomRight,
                            Margin = new MarginPadding(5),
                            Colour = xAxisColour,
                            Text = xAxisName,
                            Font = OsuFont.GetFont(weight: FontWeight.Bold)
                        },
                        new Box
                        {
                            RelativeSizeAxes = Axes.Y,
                            Width = 1,
                            EdgeSmoothness = Vector2.One,
                            Colour = yAxisColour,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.TopCentre,
                            Origin = Anchor.TopRight,
                            Margin = new MarginPadding(5),
                            Colour = yAxisColour,
                            Text = yAxisName,
                            Font = OsuFont.GetFont(weight: FontWeight.Bold)
                        },
                        new OsuSpriteText
                        {
                            Margin = new MarginPadding(5),
                            Font = OsuFont.GetFont(),
                            Text = name
                        },
                        new Circle
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(radius * 2),
                            Colour = Color4.White,
                            Alpha = 0.5f
                        },
                        point = new Circle
                        {
                            Origin = Anchor.Centre,
                            Anchor = Anchor.Centre,
                            Size = new Vector2(6)
                        }
                    };
                }
            }
        }
    }
}
