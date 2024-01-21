using DspLib.Algorithms;

public class PlanetAlgorithm5 : PlanetAlgorithm
{
    public override void GenerateTerrain(double modX, double modY)
    {
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num1 = data.vertices[index].x * (double)planet.radius;
            var num2 = data.vertices[index].y * (double)planet.radius;
            var num3 = data.vertices[index].z * (double)planet.radius;
            var num4 = 0.0;
            var num5 = Maths.Levelize(num1 * 0.007);
            var num6 = Maths.Levelize(num2 * 0.007);
            var num7 = Maths.Levelize(num3 * 0.007);
            var xin = num5 + simplexNoise1.Noise(num1 * 0.05, num2 * 0.05, num3 * 0.05) * 0.04;
            var yin = num6 + simplexNoise1.Noise(num2 * 0.05, num3 * 0.05, num1 * 0.05) * 0.04;
            var zin = num7 + simplexNoise1.Noise(num3 * 0.05, num1 * 0.05, num2 * 0.05) * 0.04;
            var num8 = Math.Abs(simplexNoise2.Noise(xin, yin, zin));
            var num9 = (0.16 - num8) * 10.0;
            var num10 = num9 > 0.0 ? num9 > 1.0 ? 1.0 : num9 : 0.0;
            var num11 = num10 * num10;
            var num12 = (simplexNoise1.Noise3DFBM(num2 * 0.005, num3 * 0.005, num1 * 0.005, 4) + 0.22) * 5.0;
            var num13 = num12 > 0.0 ? num12 > 1.0 ? 1.0 : num12 : 0.0;
            var num14 = Math.Abs(simplexNoise2.Noise3DFBM(xin * 1.5, yin * 1.5, zin * 1.5, 2));
            var num15 = simplexNoise1.Noise3DFBM(num3 * 0.06, num2 * 0.06, num1 * 0.06, 2);
            var num16 = num4 - num11 * 1.2 * num13;
            if (num16 >= 0.0)
                num16 += num8 * 0.25 + num14 * 0.6;
            var num17 = num16 - 0.1;
            var num18 = num8 * 2.1;
            if (num18 < 0.0)
                num18 *= 5.0;
            var num19 = num18 > -1.0 ? num18 > 2.0 ? 2.0 : num18 : -1.0;
            var num20 = num19 + num15 * 0.6 * num19;
            var num21 = -0.3 - num17;
            if (num21 > 0.0)
            {
                var num22 = simplexNoise2.Noise(num1 * 0.16, num2 * 0.16, num3 * 0.16) - 1.0;
                var num23 = num21 > 1.0 ? 1.0 : num21;
                var num24 = (3.0 - num23 - num23) * num23 * num23;
                num17 = -0.3 - num24 * 3.7000000476837158 + num24 * num24 * num24 * num24 * num22 * 0.5;
            }

            data.heightData[index] = (ushort)((planet.radius + num17 + 0.2) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num20 * 100.0), 0.0f, 200f);
        }
    }
}