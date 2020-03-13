using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Input.Events;
using osu.Framework.Utils;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;
using osuTK.Input;

namespace osu.Game.Screens.Evast.NumbersGameNew
{
    public class NumbersPlayfield : CompositeDrawable
    {
        private const int spacing = 10;
        private const int move_duration = 200;

        private readonly int rowCount;
        private readonly int columnCount;

        private readonly BindableBool hasFailed = new BindableBool();

        private readonly Container<DrawableNumber> numbersLayer;
        private readonly Container failOverlay;

        public NumbersPlayfield(int rowCount = 4, int columnCount = 4)
        {
            if (rowCount < 2 || columnCount < 2)
                throw new ArgumentException("Rows and columns count can't be less that 2");

            this.rowCount = rowCount;
            this.columnCount = columnCount;

            Size = new Vector2(columnCount * DrawableNumber.SIZE + spacing * (columnCount + 1), rowCount * DrawableNumber.SIZE + spacing * (rowCount + 1));
            Masking = true;
            CornerRadius = 4;
            InternalChildren = new Drawable[]
            {
                new PlayfieldBackground(rowCount, columnCount),
                numbersLayer = new Container<DrawableNumber>
                {
                    RelativeSizeAxes = Axes.Both
                },
                failOverlay = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.White.Opacity(0.5f),
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Text = "Game Over",
                            Font = OsuFont.GetFont(size: 50, weight: FontWeight.Bold),
                            Colour = new Color4(119, 110, 101, 255),
                            Shadow = false,
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            hasFailed.BindValueChanged(onFailChanged);
            reset();
        }

        private void onFailChanged(ValueChangedEvent<bool> failed)
        {
            failOverlay.FadeTo(failed.NewValue ? 1 : 0, 500, Easing.OutQuint);
            inputIsBlocked = failed.NewValue;
        }

        private void reset()
        {
            hasFailed.Value = false;
            numbersLayer.Clear(true);
            tryAddNumber();
            tryAddNumber();
        }

        private void tryAddNumber()
        {
            if (hasFailed.Value)
                return;

            int x = RNG.Next(columnCount);
            int y = RNG.Next(rowCount);

            if (getNumberAt(x, y) != null)
            {
                tryAddNumber();
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
                if (c.XIndex == x && c.YIndex == y)
                {
                    number = c;
                    return;
                }
            });

            return number;
        }

        private void setNumberAt(int x, int y)
        {
            numbersLayer.Add(new DrawableNumber(x, y, RNG.NextBool(0.9) ? 1 : 2)
            {
                Origin = Anchor.Centre,
                Position = getPositionForAxes(x, y)
            });
        }

        private Vector2 getPositionForAxes(int x, int y) => new Vector2(getPosition(x), getPosition(y));

        private int getPosition(int axisValue) => axisValue * DrawableNumber.SIZE + spacing * (axisValue + 1) + DrawableNumber.SIZE / 2;

        private void checkFailCondition()
        {
            if (numbersLayer.Count == rowCount * columnCount)
                hasFailed.Value = true;
        }

        #region Move logic

        protected override bool OnKeyDown(KeyDownEvent e)
        {
            if (!e.Repeat)
            {
                switch (e.Key)
                {
                    case Key.Right:
                        tryMove(MoveDirection.Right);
                        return true;
                    case Key.Left:
                        tryMove(MoveDirection.Left);
                        return true;
                    case Key.Up:
                        tryMove(MoveDirection.Up);
                        return true;
                    case Key.Down:
                        tryMove(MoveDirection.Down);
                        return true;
                }
            }

            return base.OnKeyDown(e);
        }

        private bool inputIsBlocked;

        private void tryMove(MoveDirection direction)
        {
            if (inputIsBlocked)
                return;

            inputIsBlocked = true;

            bool moveHasBeenMade = false;

            switch (direction)
            {
                case MoveDirection.Up:
                    moveHasBeenMade = moveUp();
                    break;

                case MoveDirection.Down:
                    moveHasBeenMade = moveDown();
                    break;

                case MoveDirection.Left:
                    moveHasBeenMade = moveLeft();
                    break;

                case MoveDirection.Right:
                    moveHasBeenMade = moveRight();
                    break;
            }

            finishMove(moveHasBeenMade);
        }

        private void finishMove(bool add)
        {
            Scheduler.AddDelayed(() =>
            {
                numbersLayer.ForEach(n => n.IsBlocked = false);
                inputIsBlocked = false;

                if (add)
                    tryAddNumber();

                checkFailCondition();
            }, move_duration + 10);
        }

        private bool moveUp()
        {
            return false;
        }

        private bool moveDown()
        {
            return false;
        }

        private bool moveLeft()
        {
            return false;
        }

        private bool moveRight()
        {
            bool moveHasBeenMade = false;

            for (int j = 0; j < rowCount; j++)
            {
                for (int i = columnCount - 1; i >= 0; i--)
                {
                    var currentNumber = getNumberAt(i, j);
                    if (currentNumber == null)
                        continue;

                    DrawableNumber closest = null;

                    for (int k = i + 1; k < columnCount; k++)
                    {
                        var possibleClosest = getNumberAt(k, j);
                        if (possibleClosest == null)
                            continue;

                        closest = possibleClosest;
                        break;
                    }

                    int newXIndex;

                    if (closest == null)
                    {
                        newXIndex = columnCount - 1;

                        if (newXIndex == currentNumber.XIndex)
                            continue;

                        currentNumber.XIndex = newXIndex;
                        currentNumber.MoveToX(getPosition(newXIndex), move_duration, Easing.OutQuint);
                    }
                    else
                    {
                        if (closest.IsBlocked || closest.Power != currentNumber.Power)
                        {
                            newXIndex = closest.XIndex - 1;

                            if (newXIndex == currentNumber.XIndex)
                                continue;

                            currentNumber.XIndex = newXIndex;
                            currentNumber.MoveToX(getPosition(newXIndex), move_duration, Easing.OutQuint);
                        }
                        else
                        {
                            newXIndex = closest.XIndex;

                            currentNumber.XIndex = newXIndex;
                            currentNumber.MoveToX(getPosition(newXIndex), move_duration, Easing.OutQuint).Expire();

                            closest.IsBlocked = true;
                            closest.IncreaseValue(move_duration);
                        }
                    }

                    moveHasBeenMade = true;
                }
            }

            return moveHasBeenMade;
        }

        #endregion

        private enum MoveDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        private class PlayfieldBackground : CompositeDrawable
        {
            public PlayfieldBackground(int rowCount, int columnCount)
            {
                Container mainLayout;

                RelativeSizeAxes = Axes.Both;
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
