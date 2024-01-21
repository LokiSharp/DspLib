using DspLib.Algorithms;
using DysonSphereProgramSeed.Dyson;

public class PlanetAlgorithm10 : PlanetAlgorithm
{
    private const int kCircleCount = 10;
    private readonly double[] eccentricities = new double[10];
    private readonly Vector4[] ellipses = new Vector4[10];
    private readonly double[] heights = new double[10];

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
        var seed5 = dotNet35Random.Next();
        for (var index = 0; index < 10; ++index)
        {
            var vectorLf3 = RandomTable.SphericNormal(ref seed5, 1.0);
            var vector4 = new Vector4((float)vectorLf3.x, (float)vectorLf3.y, (float)vectorLf3.z);
            vector4.Normalize();
            vector4 *= planet.radius;
            vector4.w = (float)(dotNet35Random.NextDouble() * 10.0 + 40.0);
            ellipses[index] = vector4;
            eccentricities[index] = dotNet35Random.NextDouble() <= 0.5
                ? Remap(0.0, 1.0, 0.2, 1.0 / 3.0, dotNet35Random.NextDouble())
                : Remap(0.0, 1.0, 3.0, 5.0, dotNet35Random.NextDouble());
            heights[index] = Remap(0.0, 1.0, 1.0, 2.0, dotNet35Random.NextDouble());
        }

        var data = planet.data;
        for (var index1 = 0; index1 < data.dataLength; ++index1)
        {
            var num4 = data.vertices[index1].x * (double)planet.radius;
            var num5 = data.vertices[index1].y * (double)planet.radius;
            var num6 = data.vertices[index1].z * (double)planet.radius;
            var num7 = Maths.Levelize(num4 * 0.007);
            var num8 = Maths.Levelize(num5 * 0.007);
            var num9 = Maths.Levelize(num6 * 0.007);
            var xin = num7 + simplexNoise3.Noise(num4 * 0.05, num5 * 0.05, num6 * 0.05) * 0.04;
            var yin = num8 + simplexNoise3.Noise(num5 * 0.05, num6 * 0.05, num4 * 0.05) * 0.04;
            var zin = num9 + simplexNoise3.Noise(num6 * 0.05, num4 * 0.05, num5 * 0.05) * 0.04;
            var num10 = Math.Abs(simplexNoise4.Noise(xin, yin, zin));
            var num11 = (0.16 - num10) * 10.0;
            var num12 = num11 > 0.0 ? num11 > 1.0 ? 1.0 : num11 : 0.0;
            var num13 = num12 * num12;
            var num14 = (simplexNoise3.Noise3DFBM(num5 * 0.005, num6 * 0.005, num4 * 0.005, 4) + 0.22) * 5.0;
            var num15 = num14 > 0.0 ? num14 > 1.0 ? 1.0 : num14 : 0.0;
            var num16 = Math.Abs(simplexNoise4.Noise3DFBM(xin * 1.5, yin * 1.5, zin * 1.5, 2));
            var x = simplexNoise2.Noise3DFBM(num4 * num1 * 5.0, num5 * num2 * 5.0, num6 * num3 * 5.0, 4);
            var num17 = x * 0.2;
            var a1 = 0.0;
            for (var index2 = 0; index2 < 10; ++index2)
            {
                var num18 = ellipses[index2].x - num4;
                var num19 = ellipses[index2].y - num5;
                var num20 = ellipses[index2].z - num6;
                var num21 = eccentricities[index2] * num18 * num18 + num19 * num19 + num20 * num20;
                var num22 = Remap(-1.0, 1.0, 0.2, 5.0, x) * num21;
                if (num22 < ellipses[index2].w * (double)ellipses[index2].w)
                {
                    var num23 = 1.0 -
                                (1.0 - Mathf.Sqrt((float)(num22 / (ellipses[index2].w * (double)ellipses[index2].w))));
                    var num24 = 1.0 - num23 * num23 * num23 * num23 + num17 * 2.0;
                    if (num24 < 0.0)
                        num24 = 0.0;
                    a1 = Max(a1, heights[index2] * num24);
                }
            }

            var num25 = num4 + Math.Sin(num5 * 0.15) * 2.0;
            var num26 = num5 + Math.Sin(num6 * 0.15) * 2.0;
            var num27 = num6 + Math.Sin(num25 * 0.15) * 2.0;
            var num28 = num25 * num1;
            var num29 = num26 * num2;
            var num30 = num27 * num3;
            double f = Mathf.Pow(
                (float)((simplexNoise1.Noise3DFBM(num28 * 0.6, num29 * 0.6, num30 * 0.6, 4, deltaWLen: 1.8) + 1.0) *
                        0.5), 1.3f);
            var num31 = Remap(-1.0, 1.0, -0.1, 0.15,
                simplexNoise2.Noise3DFBM(num28 * 6.0, num29 * 6.0, num30 * 6.0, 5));
            var num32 = simplexNoise2.Noise3DFBM(num28 * 5.0 * 3.0, num29 * 5.0, num30 * 5.0, 1);
            var num33 = simplexNoise2.Noise3DFBM(num28 * 5.0 * 3.0 + num32 * 0.3, num29 * 5.0 + num32 * 0.3,
                num30 * 5.0 + num32 * 0.3, 5) * 0.1;
            var num34 = Math.Min(1.0, Maths.Levelize(Maths.Levelize4(f)));
            if (num34 <= 0.8)
            {
                if (num34 > 0.4)
                    num34 += num33;
                else
                    num34 += num31;
            }

            var num35 = Max(num34 * 2.5 - num34 * a1, num31 * 2.0);
            var num36 = (2.0 - num35) / 2.0;
            var num37 = num35 - num13 * 1.2 * num15 * num36;
            if (num37 >= 0.0)
                num37 += (num10 * 0.25 + num16 * 0.6) * num36;
            var a2 = num37 - 0.1;
            var num38 = Math.Abs(Max(a2, -1.0));
            var num39 = 100.0;
            if (num34 < 0.4)
                num38 += Remap(-1.0, 1.0, -0.2, 0.2,
                    simplexNoise1.Noise3DFBM(num28 * 2.0 + num39, num29 * 2.0 + num39, num30 * 2.0 + num39, 5));
            data.heightData[index1] = (ushort)((planet.radius + a2 + 0.1) * 100.0);
            data.biomoData[index1] = (byte)Mathf.Clamp((float)(num38 * 100.0), 0.0f, 200f);
        }
    }

    private double Remap(
        double sourceMin,
        double sourceMax,
        double targetMin,
        double targetMax,
        double x)
    {
        return (x - sourceMin) / (sourceMax - sourceMin) * (targetMax - targetMin) + targetMin;
    }

    private double Max(double a, double b)
    {
        return a <= b ? b : a;
    }
}