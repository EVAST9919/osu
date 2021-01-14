using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;
using System;
using osu.Framework.Utils;

namespace osu.Game.Screens.Evast.RayMarching.SimpleRender
{
    public class SimpleRenderContainer : CompositeDrawable
    {
        private const int resolution = 100;

        private const float theta_spacing = 0.01f;
        private const float phi_spacing = 0.01f;

        private const float r1 = 1;
        private const float r2 = 2;
        private const float k2 = 5;
        private const float k1 = (float)(resolution * k2 * 3) / (8 * (r1 + r2));

        private float alpha;
        private float beta;

        private readonly PixelField pixelField;

        public SimpleRenderContainer()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChild = pixelField = new PixelField(resolution);
        }

        protected override void Update()
        {
            base.Update();

            renderFrame(alpha, beta);

            var elapsedTime = (float)Time.Elapsed / 1000;
            alpha += elapsedTime * 1.5f;
            beta += elapsedTime;
        }

        private void renderFrame(float a, float b)
        {
            pixelField.ClearAll();

            var cosA = Math.Cos(a);
            var sinA = Math.Sin(a);
            var cosB = Math.Cos(b);
            var sinB = Math.Sin(b);

            double[,] zBuffer = new double[resolution, resolution];

            for (float theta = 0; theta < 2 * Math.PI; theta += theta_spacing)
            {
                var cosTheta = Math.Cos(theta);
                var sinTheta = Math.Sin(theta);

                for (float phi = 0; phi < 2 * Math.PI; phi += phi_spacing)
                {
                    var cosPhi = Math.Cos(phi);
                    var sinPhi = Math.Sin(phi);

                    var circleX = r2 + r1 * cosTheta;
                    var circleY = r1 * sinTheta;

                    var x = circleX * (cosB * cosPhi + sinA * sinB * sinPhi) - circleY * cosA * sinB;
                    var y = circleX * (sinB * cosPhi - sinA * cosB * sinPhi) + circleY * cosA * cosB;
                    var z = k2 + cosA * circleX * sinPhi + circleY * sinA;
                    var ooz = 1f / z;

                    var xp = (int)(resolution / 2 + k1 * ooz * x);
                    var yp = (int)(resolution / 2 - k1 * ooz * y);

                    var l = cosPhi * cosTheta * sinB - cosA * cosTheta * sinPhi - sinA * sinTheta + cosB * (cosA * sinTheta - cosTheta * sinA * sinPhi);

                    if (l > 0)
                    {
                        if (ooz > zBuffer[xp, yp])
                        {
                            zBuffer[xp, yp] = ooz;
                            var luminance = l * 8;
                            pixelField.Pixels[xp, yp].Alpha = Interpolation.ValueAt(luminance, 0f, 1f, 0f, 11f);
                        }
                    }
                }
            }
        }

        private class PixelField : CompositeDrawable
        {
            public readonly Box[,] Pixels;

            public PixelField(int resolution)
            {
                Pixels = new Box[resolution, resolution];

                RelativeSizeAxes = Axes.Both;
                AddInternal(new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Colour4.Black
                });

                for (int i = 0; i < resolution; i++)
                {
                    for (int j = 0; j < resolution; j++)
                    {
                        AddInternal(Pixels[i, j] = new Box
                        {
                            RelativePositionAxes = Axes.Both,
                            RelativeSizeAxes = Axes.Both,
                            Position = new Vector2((float)i / resolution, (float)j / resolution),
                            Size = new Vector2(1f / resolution)
                        });
                    }
                }
            }

            public void ClearAll()
            {
                for (int i = 0; i < resolution; i++)
                {
                    for (int j = 0; j < resolution; j++)
                    {
                        Pixels[i, j].Alpha = 0;
                    }
                }
            }
        }
    }
}
