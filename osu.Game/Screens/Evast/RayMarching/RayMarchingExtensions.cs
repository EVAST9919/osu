using System;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace osu.Game.Screens.Evast.RayMarching
{
    public static class RayMarchingExtensions
    {
        public static double DistanceToCircle(Vector2 input, Circle circle) => Vector2.Distance(circle.Position, input) - circle.Size.X / 2;

        public static double DistanceToSquare(Vector2 input, Box box)
        {
            var dx = Math.Max(Math.Abs(input.X - box.Position.X) - box.Size.X / 2, 0);
            var dy = Math.Max(Math.Abs(input.Y - box.Position.Y) - box.Size.X / 2, 0);
            return Distance(new Vector2(dx, dy));
        }

        public static double DistanceToSphere(Vector3 input, Vector3 spherePosition, double sphereRadius) => Vector3.Distance(spherePosition, input) - sphereRadius;

        public static Vector3 PositionOnASphere(Vector3 position, double radius, double theta, double phi)
        {
            var x = radius * Math.Cos(theta) * Math.Sin(phi);
            var y = radius * Math.Sin(theta) * Math.Sin(phi);
            var z = radius * Math.Cos(phi);
            return new Vector3((float)x, (float)y, (float)z) + position;
        }

        public static double Distance(Vector2 v) => Math.Sqrt(v.X * v.X + v.Y * v.Y);

        public static double RayAngle(Vector2 source, Vector2 target) => Math.Atan2(target.Y - source.Y, target.X - source.X);

        public static Vector2 PositionOnACircle(Vector2 position, double radius, double angle)
        {
            var x = radius * Math.Cos(angle);
            var y = radius * Math.Sin(angle);
            return new Vector2((float)x, (float)y) + position;
        }
    }
}
