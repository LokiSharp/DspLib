using DspLib.Algorithms;

public class PlanetAlgorithm8 : PlanetAlgorithm
{
    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.002 * modX;
        var num2 = 0.002 * modX * modX * 6.66667;
        var num3 = 0.002 * modX;
        var simplexNoise = new SimplexNoise(new DotNet35Random(planet.seed).Next());
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num4 = data.vertices[index].x * (double)planet.radius;
            var num5 = data.vertices[index].y * (double)planet.radius;
            var num6 = data.vertices[index].z * (double)planet.radius;
            var num7 = Mathf.Clamp(
                (float)(simplexNoise.Noise3DFBM(num4 * num1, num5 * num2, num6 * num3, 6, 0.45, 1.8) + 1.0 +
                        modY * 0.0099999997764825821), 0.0f, 2f);
            float num8;
            if (num7 < 1.0)
            {
                var f = Mathf.Cos(num7 * 3.14159274f) * 1.1f;
                num8 = (float)(1.0 - (Mathf.Clamp(Mathf.Sign(f) * Mathf.Pow(f, 4f), -1f, 1f) + 1.0) * 0.5);
            }
            else
            {
                var f = Mathf.Cos((float)((num7 - 1.0) * 3.1415927410125732)) * 1.1f;
                num8 = (float)(2.0 - (Mathf.Clamp(Mathf.Sign(f) * Mathf.Pow(f, 4f), -1f, 1f) + 1.0) * 0.5);
            }

            double num9 = num8;
            double num10 = num8;
            var num11 = num10 < 1.0 ? Math.Max(num10 - 0.2, 0.0) * 1.25 : num10;
            var num12 = Maths.Levelize2(num11 > 1.0 ? Math.Min(num11 * num11, 2.0) : num11);
            data.heightData[index] = (ushort)((planet.radius + num9 + 0.1) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num12 * 100.0), 0.0f, 200f);
        }
    }
}