using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Utils;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Screens.Evast.NumbersGameNew
{
    public class NumbersPlayfield : CompositeDrawable
    {
        private const int spacing = 10;

        private readonly int rowCount;
        private readonly int columnCount;

        private readonly Container<DrawableNumber> numbersLayer;

        public NumbersPlayfield(int rowCount = 4, int columnCount = 4)
        {
            if (rowCount < 2 || columnCount < 2)
                throw new ArgumentException("Rows and columns count can't be less that 2");

            this.rowCount = rowCount;
            this.columnCount = columnCount;

            Size = new Vector2(columnCount * DrawableNumber.SIZE + spacing * (columnCount + 1), rowCount * DrawableNumber.SIZE + spacing * (rowCount + 1));
            InternalChildren = new Drawable[]
            {
                new PlayfieldBackground(rowCount, columnCount),
                numbersLayer = new Container<DrawableNumber>
                {
                    RelativeSizeAxes = Axes.Both
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            reset();
        }

        private void reset()
        {
            numbersLayer.Clear(true);
            addNumber();
            addNumber();
        }

        private void addNumber()
        {
            int x = RNG.Next(columnCount);
            int y = RNG.Next(rowCount);

            if (getNumberAt(x, y) != null)
            {
                addNumber();
                return;
            }

            setNumberAt(x, y);
        }

        private DrawableNumber getNumberAt(int x, int y)
        {
            if (!numbersLayer.Any())
                return null;

            DrawableNumber number = null;

            numbersLayer.Children.ForEach(c =>
            {
                if (c.Position == getPositionForAxes(x, y))
                {
                    number = c;
                    return;
                }
            });

            return number;
        }

        private void setNumberAt(int x, int y)
        {
            numbersLayer.Add(new DrawableNumber(RNG.NextBool(0.9) ? 1 : 2)
            {
                Origin = Anchor.Centre,
                Position = getPositionForAxes(x, y)
            });
        }

        private Vector2 getPositionForAxes(int x, int y) => new Vector2(getPosition(x), getPosition(y));

        private int getPosition(int axisValue) => axisValue * DrawableNumber.SIZE + spacing * (axisValue + 1) + DrawableNumber.SIZE / 2;

        private class PlayfieldBackground : CompositeDrawable
        {
            public PlayfieldBackground(int rowCount, int columnCount)
            {
                Container mainLayout;

                RelativeSizeAxes = Axes.Both;
                Masking = true;
                CornerRadius = 4;
                InternalChildren = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.Both,
                        Colour = new Color4(187, 173, 160, 255)
                    },
                    mainLayout = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Padding = new MarginPadding(spacing),
                    }
                };

                var rows = new List<FillFlowContainer>();

                for (int i = 0; i < rowCount; i++)
                {
                    var row = new FillFlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FillDirection.Horizontal,
                        Spacing = new Vector2(spacing, 0)
                    };

                    for (int y = 0; y < columnCount; y++)
                    {
                        row.Add(new Container
                        {
                            Size = new Vector2(DrawableNumber.SIZE),
                            Masking = true,
                            CornerRadius = 4,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both,
                                Colour = new Color4(205, 192, 179, 255)
                            }
                        });
                    }

                    rows.Add(row);
                }

                mainLayout.Add(new FillFlowContainer
                {
                    AutoSizeAxes = Axes.Both,
                    Direction = FillDirection.Vertical,
                    Spacing = new Vector2(0, spacing),
                    Children = rows
                });
            }
        }
    }
}
