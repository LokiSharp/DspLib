using DspLib.Algorithms;

public class PlanetAlgorithm6 : PlanetAlgorithm
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
            var num15 = num4 - num11 * 1.2 * num13;
            if (num15 >= 0.0)
                num15 += num8 * 0.25 + num14 * 0.6;
            var num16 = num15 - 0.1;
            var num17 = -0.3 - num16;
            if (num17 > 0.0)
            {
                var num18 = num17 > 1.0 ? 1.0 : num17;
                num16 = -0.3 - (3.0 - num18 - num18) * num18 * num18 * 3.7000000476837158;
            }

            var num19 = Maths.Levelize(num11 > 0.30000001192092896 ? num11 : 0.30000001192092896, 0.7);
            var num20 = num16 > -0.800000011920929 ? num16 : (-num19 - num8) * 0.89999997615814209;
            var num21 = num20 > -1.2000000476837158 ? num20 : -1.2000000476837158;
            var num22 = num21 * num11 + (num8 * 2.1 + 0.800000011920929);
            if (num22 > 1.7000000476837158 && num22 < 2.0)
                num22 = 2.0;
            data.heightData[index] = (ushort)((planet.radius + num21 + 0.2) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num22 * 100.0), 0.0f, 200f);
        }
    }
}