using DspLib.Algorithms;

namespace DysonSphereProgramSeed.Dyson;

public static class RandomTable
{
    private const int size = 65536;
    private static VectorLF3[] sphericNormal;
    private static int[] integers;

    private static double Normal(URandom1 rand)
    {
        var num = rand.NextDouble();
        var a = rand.NextDouble() * Math.PI * 2.0;
        return Math.Sqrt(-2.0 * Math.Log(1.0 - num)) * Math.Sin(a);
    }

    public static void Init()
    {
        GenerateSphericNormal();
        GenerateIntegers();
    }

    public static VectorLF3 SphericNormal(ref int seed, double scale)
    {
        ++seed;
        seed &= ushort.MaxValue;
        return new VectorLF3(sphericNormal[seed].x * scale, sphericNormal[seed].y * scale,
            sphericNormal[seed].z * scale);
    }

    public static int Integer(ref int seed, int maxValue)
    {
        ++seed;
        seed &= ushort.MaxValue;
        return maxValue <= 0 ? 0 : integers[seed] % maxValue;
    }

    public static void GenerateSphericNormal()
    {
        var rand = new URandom1(1001);
        sphericNormal = new VectorLF3[65536];
        for (var index = 0; index < 65536; ++index)
        {
            double num1;
            double num2;
            double num3;
            double num4;
            double d;
            do
            {
                do
                {
                    num1 = rand.NextDouble() * 2.0 - 1.0;
                    num2 = rand.NextDouble() * 2.0 - 1.0;
                    num3 = rand.NextDouble() * 2.0 - 1.0;
                    num4 = Normal(rand);
                } while (num4 > 5.0 || num4 < -5.0);

                d = num1 * num1 + num2 * num2 + num3 * num3;
            } while (d > 1.0 || d < 1E-06);

            var num5 = num4 / Math.Sqrt(d);
            var x_ = num1 * num5;
            var y_ = num2 * num5;
            var z_ = num3 * num5;
            sphericNormal[index] = new VectorLF3(x_, y_, z_);
        }
    }

    public static void GenerateIntegers()
    {
        var urandom1 = new URandom1(1002);
        integers = new int[65536];
        for (var index = 0; index < 65536; ++index)
            integers[index] = urandom1.Next();
    }
}