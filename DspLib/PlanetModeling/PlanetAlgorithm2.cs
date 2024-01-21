using DspLib.Algorithms;

public class PlanetAlgorithm2 : PlanetAlgorithm
{
    public override void GenerateTerrain(double modX, double modY)
    {
        modX = (3.0 - modX - modX) * modX * modX;
        var num1 = 0.0035;
        var num2 = 0.025 * modX + 0.0035 * (1.0 - modX);
        var num3 = 0.0035;
        var num4 = 3.0;
        var num5 = 1.0 + 1.3 * modY;
        var num6 = num1 * num5;
        var num7 = num2 * num5;
        var num8 = num3 * num5;
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num9 = data.vertices[index].x * (double)planet.radius;
            var num10 = data.vertices[index].y * (double)planet.radius;
            var num11 = data.vertices[index].z * (double)planet.radius;
            double y = data.vertices[index].y;
            var num12 = simplexNoise1.Noise3DFBM(num9 * num6, num10 * num7, num11 * num8, 6, 0.45, 1.8);
            var num13 = simplexNoise2.Noise3DFBM(num9 * num6 * 2.0, num10 * num7 * 2.0, num11 * num8 * 2.0, 3);
            var num14 = num4;
            var num15 = 0.6 / (Math.Abs(num12 * num14 + num4 * 0.4) + 0.6) - 0.25;
            var num16 = num15 < 0.0 ? num15 * 0.3 : num15;
            var num17 = Math.Pow(Math.Abs(y * 1.01), 3.0) * 1.0;
            var num18 = num13 < 0.0 ? 0.0 : num13;
            var num19 = num17 > 1.0 ? 1.0 : num17;
            var num20 = num16;
            var num21 = num16 * 1.5 + num18 * 1.0 + num19;
            data.heightData[index] = (ushort)((planet.radius + num20 + 0.1) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num21 * 100.0), 0.0f, 200f);
        }
    }
}