using DspLib.Algorithms;
using DspLib.Enum;

public class PlanetAlgorithm7 : PlanetAlgorithm
{
    private List<Vector2> tmp_vecs = new(100);
    private int veinVectorCount;
    private Vector3[] veinVectors = new Vector3[512];
    private EVeinType[] veinVectorTypes = new EVeinType[512];

    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.008;
        var num2 = 0.01;
        var num3 = 0.01;
        var num4 = 3.0;
        var num5 = -2.4;
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
            var num13 = simplexNoise1.Noise3DFBM(num10 * num1, num11 * num2, num12 * num3, 6) * num4 + num5;
            var num14 =
                simplexNoise2.Noise3DFBM(num10 * (1.0 / 400.0), num11 * (1.0 / 400.0), num12 * (1.0 / 400.0), 3) *
                num4 * num6 + num7;
            var num15 = num14 > 0.0 ? num14 * 0.5 : num14;
            var num16 = num13 + num15;
            var f = num16 > 0.0 ? num16 * 0.5 : num16 * 1.6;
            var num17 = f > 0.0 ? Maths.Levelize3(f, 0.7) : Maths.Levelize2(f, 0.5);
            var num18 = simplexNoise2.Noise3DFBM(num10 * num1 * 2.5, num11 * num2 * 8.0, num12 * num3 * 2.5, 2) * 0.6 -
                        0.3;
            var num19 = f * num8 + num18 + num9;
            var num20 = num19 < 1.0 ? num19 : (num19 - 1.0) * 0.8 + 1.0;
            var num21 = num17;
            var num22 = num20;
            data.heightData[index] = (ushort)((planet.radius + num21) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num22 * 100.0), 0.0f, 200f);
        }
    }
}