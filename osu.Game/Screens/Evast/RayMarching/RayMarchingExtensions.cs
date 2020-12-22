﻿using System;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu.Game.Screens.Evast.RayMarching
{
    public static class RayMarchingExtensions
    {
        public static double DistanceToCircle(Vector2 input, Circle circle) => Distance(circle.Position - input) - circle.Size.X / 2;

        public static double DistanceToSquare(Vector2 input, Box box)
        {
            var dx = Math.Max(Math.Abs(input.X - box.Position.X) - box.Size.X / 2, 0);
            var dy = Math.Max(Math.Abs(input.Y - box.Position.Y) - box.Size.X / 2, 0);
            return Distance(new Vector2(dx, dy));
        }

        public static double Distance(Vector2 v) => Math.Sqrt(v.X * v.X + v.Y * v.Y);

        public static double RayAngle(Vector2 source, Vector2 target) => Math.Atan2(target.Y - source.Y, target.X - source.X);

        public static Vector2 PositionOnASphere(Vector2 input, double distance, double angle)
        {
            var x = distance * Math.Cos(angle) + input.X;
            var y = distance * Math.Sin(angle) + input.Y;
            return new Vector2((float)x, (float)y);
        }
    }
}
