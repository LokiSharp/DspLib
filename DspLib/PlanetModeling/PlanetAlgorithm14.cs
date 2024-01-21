using DspLib.Algorithms;

public class PlanetAlgorithm14 : PlanetAlgorithm
{
    private const double LAVAWIDTH = 0.12;

    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.007;
        var num2 = 0.007;
        var num3 = 0.007;
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var seed3 = dotNet35Random.Next();
        var seed4 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var simplexNoise3 = new SimplexNoise(seed3);
        var simplexNoise4 = new SimplexNoise(seed4);
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num4 = data.vertices[index].x * (double)planet.radius;
            var num5 = data.vertices[index].y * (double)planet.radius;
            var num6 = data.vertices[index].z * (double)planet.radius;
            var num7 = Maths.Levelize(num4 * 0.007 / 2.0);
            var num8 = Maths.Levelize(num5 * 0.007 / 2.0);
            var num9 = Maths.Levelize(num6 * 0.007 / 2.0);
            var xin = num7 + simplexNoise3.Noise(num4 * 0.05, num5 * 0.05, num6 * 0.05) * 0.04;
            var yin = num8 + simplexNoise3.Noise(num5 * 0.05, num6 * 0.05, num4 * 0.05) * 0.04;
            var zin = num9 + simplexNoise3.Noise(num6 * 0.05, num4 * 0.05, num5 * 0.05) * 0.04;
            var num10 = (0.12 - Math.Abs(simplexNoise4.Noise(xin, yin, zin))) * 10.0;
            var num11 = num10 > 0.0 ? num10 > 1.0 ? 1.0 : num10 : 0.0;
            var num12 = num11 * num11;
            var num13 = (simplexNoise3.Noise3DFBM(num5 * 0.005, num6 * 0.005, num4 * 0.005, 4) + 0.22) * 5.0;
            var num14 = num13 > 0.0 ? num13 > 1.0 ? 1.0 : num13 : 0.0;
            Math.Abs(simplexNoise4.Noise3DFBM(xin * 1.5, yin * 1.5, zin * 1.5, 2));
            var num15 = num4 + Math.Sin(num5 * 0.15) * 3.0;
            var num16 = num5 + Math.Sin(num6 * 0.15) * 3.0;
            var num17 = num6 + Math.Sin(num15 * 0.15) * 3.0;
            var num18 = 0.0;
            var num19 = simplexNoise1.Noise3DFBM(num15 * num1 * 1.0, num16 * num2 * 1.1, num17 * num3 * 1.0, 6,
                deltaWLen: 1.8);
            var num20 = simplexNoise2.Noise3DFBM(num15 * num1 * 1.3 + 0.5, num16 * num2 * 2.8 + 0.2,
                num17 * num3 * 1.3 + 0.7, 3) * 2.0;
            var num21 = simplexNoise2.Noise3DFBM(num15 * num1 * 6.0, num16 * num2 * 12.0, num17 * num3 * 6.0, 2) * 2.0;
            var num22 = simplexNoise2.Noise3DFBM(num15 * num1 * 0.8, num16 * num2 * 0.8, num17 * num3 * 0.8, 2) * 2.0;
            var f = num19 * 2.0 + 0.92 + Mathf.Clamp01((float)(num20 * Mathf.Abs((float)num22 + 0.5f) - 0.35) * 1f);
            if (f < 0.0)
                f = 0.0;
            var t = Maths.Levelize2(f);
            if (t > 0.0)
                t = Maths.Levelize4(Maths.Levelize2(f));
            var num23 = t > 0.0
                ? t > 1.0
                    ? t > 2.0
                        ? Mathf.Lerp(1.4f, 2.7f, (float)t - 2f) + num21 * 0.12
                        : Mathf.Lerp(0.3f, 1.4f, (float)t - 1f) + num21 * 0.12
                    : Mathf.Lerp(0.0f, 0.3f, (float)t) + num21 * 0.1
                : Mathf.Lerp(-4f, 0.0f, (float)t + 1f);
            if (f < 0.0)
                f *= 2.0;
            if (f < 1.0)
                f = Maths.Levelize(f);
            var num24 = num18 - num12 * 1.2 * num14;
            if (num24 >= 0.0)
                num24 = num23;
            var num25 = num24 - 0.1;
            double num26 = Mathf.Abs((float)f);
            var num27 = Math.Pow(Mathf.Clamp01((float)((-num25 + 2.0) / 2.5)), 10.0);
            var num28 = (1.0 - num27) * num26 + num27 * 2.0;
            var num29 = num28 > 0.0 ? num28 > 2.0 ? 2.0 : num28 : 0.0;
            var num30 = num29 + (num29 > 1.8 ? -num21 * 0.8 : num21 * 0.2) * (1.0 - num27);
            var num31 = -0.3 - num25;
            if (num31 > 0.0)
            {
                var num32 = simplexNoise2.Noise(num15 * 0.16, num16 * 0.16, num17 * 0.16) - 1.0;
                var num33 = num31 > 1.0 ? 1.0 : num31;
                var num34 = (3.0 - num33 - num33) * num33 * num33;
                num25 = -0.3 - num34 * 10.0 + num34 * num34 * num34 * num34 * num32 * 0.5;
            }

            data.heightData[index] = (ushort)((planet.radius + num25 + 0.2) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num30 * 100.0), 0.0f, 200f);
        }
    }
}