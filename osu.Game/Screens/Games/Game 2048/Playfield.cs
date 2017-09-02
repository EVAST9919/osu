// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics.Sprites;
using System;
using System.Collections.Generic;

namespace osu.Game.Screens.Games.Game_2048
{
    public class Playfield : Container
    {
        private const int move_duration = 200;
        private const int fade_duration = 50;
        private const int endgame_appear_duration = 300;

        private readonly FieldPoint[,] fieldPoint;
        private Container backgroundOverlay;
        private Container numbersOverlay;
        private Container gameOverOverlay;
        private Random random;

        private bool gameIsOver;
        public bool GameIsOver
        {
            set
            {
                gameIsOver = value;
                gameOverOverlay.FadeTo(gameIsOver ? 1 : 0, endgame_appear_duration);
            }
            get { return gameIsOver; }
        }

        public Playfield()
        {
            CornerRadius = 7;
            Masking = true;
            Size = new Vector2(500);
            fieldPoint = new FieldPoint[4, 4];
            Children = new Drawable[]
            {
                backgroundOverlay = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.White,
                        },
                        new BackgroundBox { Position = new Vector2(20) },
                        new BackgroundBox { Position = new Vector2(140,20) },
                        new BackgroundBox { Position = new Vector2(260,20) },
                        new BackgroundBox { Position = new Vector2(380,20) },
                        new BackgroundBox { Position = new Vector2(20,140) },
                        new BackgroundBox { Position = new Vector2(140) },
                        new BackgroundBox { Position = new Vector2(260,140) },
                        new BackgroundBox { Position = new Vector2(380,140) },
                        new BackgroundBox { Position = new Vector2(20,260) },
                        new BackgroundBox { Position = new Vector2(140,260) },
                        new BackgroundBox { Position = new Vector2(260) },
                        new BackgroundBox { Position = new Vector2(380,260) },
                        new BackgroundBox { Position = new Vector2(20,380) },
                        new BackgroundBox { Position = new Vector2(140,380) },
                        new BackgroundBox { Position = new Vector2(260,380) },
                        new BackgroundBox { Position = new Vector2(380) }
                    }
                },
                fieldPoint[0,0] = new FieldPoint { Position = new Vector2(20) },
                fieldPoint[1,0] = new FieldPoint { Position = new Vector2(140,20) },
                fieldPoint[2,0] = new FieldPoint { Position = new Vector2(260,20) },
                fieldPoint[3,0] = new FieldPoint { Position = new Vector2(380,20) },
                fieldPoint[0,1] = new FieldPoint { Position = new Vector2(20,140) },
                fieldPoint[1,1] = new FieldPoint { Position = new Vector2(140) },
                fieldPoint[2,1] = new FieldPoint { Position = new Vector2(260,140) },
                fieldPoint[3,1] = new FieldPoint { Position = new Vector2(380,140) },
                fieldPoint[0,2] = new FieldPoint { Position = new Vector2(20,260) },
                fieldPoint[1,2] = new FieldPoint { Position = new Vector2(140,260) },
                fieldPoint[2,2] = new FieldPoint { Position = new Vector2(260) },
                fieldPoint[3,2] = new FieldPoint { Position = new Vector2(380,260) },
                fieldPoint[0,3] = new FieldPoint { Position = new Vector2(20,380) },
                fieldPoint[1,3] = new FieldPoint { Position = new Vector2(140,380) },
                fieldPoint[2,3] = new FieldPoint { Position = new Vector2(260,380) },
                fieldPoint[3,3] = new FieldPoint { Position = new Vector2(380) },
                numbersOverlay = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                },
                gameOverOverlay = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Colour = Color4.Black.Opacity(200),
                        },
                        new OsuSpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            TextSize = 50,
                            Text = @"Game Over",
                            Colour = Color4.White,
                        }
                    }
                }
            };

            random = new Random();
        }

        public void Initialize()
        {
            setNewNumber();
            setNewNumber();
        }

        private bool playfieldHasEmptyBoxes()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (fieldPoint[i, j].IsEmpty)
                        return true;
                }
            }

            return false;
        }

        private void setNewNumber()
        {
            if (gameIsOver)
            {
                Reset();
            }
            else
            {
                if (playfieldHasEmptyBoxes())
                {
                    FieldPoint box = findEmptyBox();

                    numbersOverlay.Add(box.FieldNumber = new FieldNumber
                    {
                        Alpha = 0,
                        Position = box.Position
                    });

                    box.FieldNumber.FadeTo(1, fade_duration);
                }
                else
                {
                    GameIsOver = true;
                }
            }
        }

        private FieldPoint findEmptyBox()
        {
            int x = random.Next(4);
            int y = random.Next(4);

            while (true)
            {
                if (fieldPoint[x, y].IsEmpty)
                    return fieldPoint[x, y];

                x = random.Next(4);
                y = random.Next(4);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!fieldPoint[i, j].IsEmpty)
                        fieldPoint[i, j].IsEmpty = true;
                }
            }

            foreach (FieldNumber n in getAllNumbers())
                n.FadeTo(0, fade_duration).Finally(d => d.Expire());

            GameIsOver = false;

            Initialize();
        }

        private IEnumerable<FieldNumber> getAllNumbers()
        {
            foreach (Drawable d in numbersOverlay.Children)
            {
                if (d is FieldNumber)
                    yield return d as FieldNumber;
            }
        }

        public void Up()
        {

        }

        public void Down()
        {

        }

        public void Left()
        {

        }

        public void Right()
        {
            for (int i = 2; i >=0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!fieldPoint[i, j].IsEmpty)//Current box
                    {
                        for (int k = i + 1; k < 4; k++)//looking through the whole line
                        {
                            if (fieldPoint[k, j].IsEmpty)//if next box is empty
                            {
                                if (k == 3)//if it's the last one in the line
                                    setNewPosition(fieldPoint[i, j], fieldPoint[k, j]);
                                else continue;//go to the next box in the line
                            }
                            else
                            {
                                if (fieldPoint[i, j].FieldNumber.Power == fieldPoint[k, j].FieldNumber.Power)
                                    setNewPosition(fieldPoint[i, j], fieldPoint[k, j]);
                                else
                                {
                                    if (k - 1 == i)
                                        break;
                                    setNewPosition(fieldPoint[i, j], fieldPoint[k - 1, j]);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void setNewPosition(FieldPoint oldPoint, FieldPoint newPoint)
        {
            if(!newPoint.IsEmpty)
            {
                newPoint.FieldNumber.Expire();
                newPoint.FieldNumber = oldPoint.FieldNumber;
                oldPoint.FieldNumber = null;
                newPoint.FieldNumber.ClearTransforms();
                newPoint.FieldNumber.MoveTo(newPoint.Position, move_duration);
                newPoint.FieldNumber.NextPower();
            }
            else
            {
                newPoint.FieldNumber = oldPoint.FieldNumber;
                oldPoint.FieldNumber = null;
                newPoint.FieldNumber.ClearTransforms();
                newPoint.FieldNumber.MoveTo(newPoint.Position, move_duration);
            }

            setNewNumber();
        }
    }
}
