using DspLib.Algorithms;
using DysonSphereProgramSeed.Dyson;

public class PlanetAlgorithm4 : PlanetAlgorithm
{
    private const int kCircleCount = 80;
    private readonly Vector4[] circles = new Vector4[80];
    private readonly double[] heights = new double[80];

    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.007;
        var num2 = 0.007;
        var num3 = 0.007;
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var seed3 = dotNet35Random.Next();
        for (var index = 0; index < 80; ++index)
        {
            var vectorLf3 = RandomTable.SphericNormal(ref seed3, 1.0);
            var vector4 = new Vector4((float)vectorLf3.x, (float)vectorLf3.y, (float)vectorLf3.z);
            vector4.Normalize();
            vector4 *= planet.radius;
            vector4.w = (float)(vectorLf3.magnitude * 8.0 + 8.0);
            vector4.w *= vector4.w;
            circles[index] = vector4;
            heights[index] = dotNet35Random.NextDouble() * 0.4 + 0.20000000298023224;
        }

        var data = planet.data;
        for (var index1 = 0; index1 < data.dataLength; ++index1)
        {
            var num4 = data.vertices[index1].x * (double)planet.radius;
            var num5 = data.vertices[index1].y * (double)planet.radius;
            var num6 = data.vertices[index1].z * (double)planet.radius;
            var num7 = simplexNoise1.Noise3DFBM(num4 * num1, num5 * num2, num6 * num3, 4, 0.45, 1.8);
            var num8 = simplexNoise2.Noise3DFBM(num4 * num1 * 5.0, num5 * num2 * 5.0, num6 * num3 * 5.0, 4);
            var num9 = num7 * 1.5;
            var num10 = num8 * 0.2;
            var num11 = num9 * 0.08 + num10 * 2.0;
            var num12 = 0.0;
            for (var index2 = 0; index2 < 80; ++index2)
            {
                var num13 = circles[index2].x - num4;
                var num14 = circles[index2].y - num5;
                var num15 = circles[index2].z - num6;
                var num16 = num13 * num13 + num14 * num14 + num15 * num15;
                if (num16 <= circles[index2].w)
                {
                    var num17 = num16 / circles[index2].w + num10 * 1.2;
                    if (num17 < 0.0)
                        num17 = 0.0;
                    var num18 = num17 * num17;
                    var num19 = -15.0 * (num18 * num17) + 131.0 / 6.0 * num18 - 113.0 / 15.0 * num17 + 0.7 + num10;
                    if (num19 < 0.0)
                        num19 = 0.0;
                    var num20 = num19 * num19 * heights[index2];
                    num12 = num12 > num20 ? num12 : num20;
                }
            }

            var num21 = num12 + num11 + 0.2;
            var num22 = num9 * 2.0 + 0.8;
            var num23 = num22 > 2.0 ? 2.0 : num22 < 0.0 ? 0.0 : num22;
            var num24 = num23 + (num23 > 1.5 ? -num12 : num12) * 0.5 + num8 * 0.63;
            data.heightData[index1] = (ushort)((planet.radius + num21 + 0.1) * 100.0);
            data.biomoData[index1] = (byte)Mathf.Clamp((float)(num24 * 100.0), 0.0f, 200f);
        }
    }
}