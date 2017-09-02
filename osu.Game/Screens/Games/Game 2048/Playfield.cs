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
using System.Collections;
using System.Collections.Generic;

namespace osu.Game.Screens.Games.Game_2048
{
    public class Playfield : Container
    {
        private readonly FieldBox[,] fieldBox;
        private Container gameOverOverlay;
        private Random random;

        public Playfield()
        {
            CornerRadius = 7;
            Masking = true;
            Size = new Vector2(500);
            fieldBox = new FieldBox[4, 4];
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.White,
                },
                fieldBox[0,0] = new FieldBox { Position = new Vector2(20) },
                fieldBox[1,0] = new FieldBox { Position = new Vector2(140,20) },
                fieldBox[2,0] = new FieldBox { Position = new Vector2(260,20) },
                fieldBox[3,0] = new FieldBox { Position = new Vector2(380,20) },
                fieldBox[0,1] = new FieldBox { Position = new Vector2(20,140) },
                fieldBox[1,1] = new FieldBox { Position = new Vector2(140) },
                fieldBox[2,1] = new FieldBox { Position = new Vector2(260,140) },
                fieldBox[3,1] = new FieldBox { Position = new Vector2(380,140) },
                fieldBox[0,2] = new FieldBox { Position = new Vector2(20,260) },
                fieldBox[1,2] = new FieldBox { Position = new Vector2(140,260) },
                fieldBox[2,2] = new FieldBox { Position = new Vector2(260) },
                fieldBox[3,2] = new FieldBox { Position = new Vector2(380,260) },
                fieldBox[0,3] = new FieldBox { Position = new Vector2(20,380) },
                fieldBox[1,3] = new FieldBox { Position = new Vector2(140,380) },
                fieldBox[2,3] = new FieldBox { Position = new Vector2(260,380) },
                fieldBox[3,3] = new FieldBox { Position = new Vector2(380) },
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
                    if (fieldBox[i, j].IsEmpty)
                        return true;
                }
            }

            return false;
        }

        private void setNewNumber()
        {
            if (playfieldHasEmptyBoxes())
            {
                FieldBox box = findEmptyBox();

                Add(box.FieldNumber = new FieldNumber { Position = box.Position });
            }
            else
            {
                Add(gameOverOverlay = new Container
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
                });
                gameOverOverlay.FadeTo(1, 300);
            }
        }

        private FieldBox findEmptyBox()
        {
            int x = random.Next(4);
            int y = random.Next(4);

            while (true)
            {
                if (fieldBox[x, y].IsEmpty)
                    return fieldBox[x, y];

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
                    fieldBox[i, j].IsEmpty = true;
                }
            }

            foreach (FieldNumber n in GetAllNumbers())
                n.Expire();

            Initialize();
        }

        public IEnumerable<FieldNumber> GetAllNumbers()
        {
            foreach (Drawable d in Children)
            {
                if (d is FieldNumber)
                    yield return d as FieldNumber;
            }
        }

        public void Up()
        {
            setNewNumber();
        }

        public void Down()
        {
            setNewNumber();
        }

        public void Left()
        {
            setNewNumber();
        }

        public void Right()
        {
            setNewNumber();
        }
    }
}
