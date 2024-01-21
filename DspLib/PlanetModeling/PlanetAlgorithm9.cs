using DspLib.Algorithms;

public class PlanetAlgorithm9 : PlanetAlgorithm
{
    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.01;
        var num2 = 0.012;
        var num3 = 0.01;
        var num4 = 3.0;
        var num5 = -0.2;
        var num6 = 0.9;
        var num7 = 0.5;
        var num8 = 2.5;
        var num9 = 0.3;
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num10 = data.vertices[index].x * (double)planet.radius;
            var num11 = data.vertices[index].y * (double)planet.radius;
            var num12 = data.vertices[index].z * (double)planet.radius;
            var num13 =
                simplexNoise1.Noise3DFBM(num10 * num1 * 0.75, num11 * num2 * 0.5, num12 * num3 * 0.75, 6) * num4 + num5;
            var num14 =
                simplexNoise2.Noise3DFBM(num10 * (1.0 / 400.0), num11 * (1.0 / 400.0), num12 * (1.0 / 400.0), 3) *
                num4 * num6 + num7;
            var num15 = num14 > 0.0 ? num14 * 0.5 : num14;
            var num16 = num13 + num15;
            var f = num16 > 0.0 ? num16 * 0.5 : num16 * 1.6;
            var num17 = (f > 0.0 ? Maths.Levelize3(f, 0.7) : Maths.Levelize2(f, 0.5)) + 0.618;
            var num18 = num17 > -1.0 ? num17 * 1.5 : num17 * 4.0;
            var num19 = simplexNoise2.Noise3DFBM(num10 * num1 * 2.5, num11 * num2 * 8.0, num12 * num3 * 2.5, 2) * 0.6 -
                        0.3;
            var num20 = f * num8 + num19 + num9;
            var val1 = Maths.Levelize(f + 0.7);
            var num21 = simplexNoise1.Noise3DFBM(num10 * num1 * modX, num11 * num2 * modX, num12 * num3 * modX, 6) *
                num4 + num5;
            var num22 =
                simplexNoise2.Noise3DFBM(num10 * (1.0 / 400.0), num11 * (1.0 / 400.0), num12 * (1.0 / 400.0), 3) *
                num4 * num6 + num7;
            var num23 = num22 > 0.0 ? num22 * 0.5 : num22;
            var num24 = Math.Pow((num21 + num23 + 5.0) * 0.13, 6.0) * 24.0 - 24.0;
            var num25 = num18 >= -modY ? 0.0 : Math.Pow(Math.Min(Math.Abs(num18 + modY) / 5.0, 1.0), 1.0);
            var num26 = num18 * (1.0 - num25) + num24 * num25;
            var num27 = num26 > 0.0 ? num26 * 0.5 : num26;
            var num28 = Math.Max(
                simplexNoise2.Noise3DFBM(num10 * num1 * 1.5, num11 * num2 * 2.0, num12 * num3 * 1.5, 6) * num4 + num5 +
                1.0, -0.99);
            var num29 = num28 > 0.0 ? num28 * 0.25 : num28;
            var num30 = Math.Max(val1, 0.0);
            double num31 = Mathf.Clamp01((float)(num30 - 1.0));
            var num32 = Math.Min(num30 > 1.0 ? num31 * num29 * 1.15 + 1.0 : num30, 2.0);
            data.heightData[index] = (ushort)((planet.radius + num27 + 0.2) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num32 * 100.0), 0.0f, 200f);
        }
    }
}