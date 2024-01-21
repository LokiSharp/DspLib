namespace DspLib.Algorithms;

public class SimplexNoise
{
    private static readonly Grad[] grad3 = new Grad[12]
    {
        new(1.0, 1.0, 0.0),
        new(-1.0, 1.0, 0.0),
        new(1.0, -1.0, 0.0),
        new(-1.0, -1.0, 0.0),
        new(1.0, 0.0, 1.0),
        new(-1.0, 0.0, 1.0),
        new(1.0, 0.0, -1.0),
        new(-1.0, 0.0, -1.0),
        new(0.0, 1.0, 1.0),
        new(0.0, -1.0, 1.0),
        new(0.0, 1.0, -1.0),
        new(0.0, -1.0, -1.0)
    };

    private static readonly Grad[] grad4 = new Grad[32]
    {
        new(0.0, 1.0, 1.0, 1.0),
        new(0.0, 1.0, 1.0, -1.0),
        new(0.0, 1.0, -1.0, 1.0),
        new(0.0, 1.0, -1.0, -1.0),
        new(0.0, -1.0, 1.0, 1.0),
        new(0.0, -1.0, 1.0, -1.0),
        new(0.0, -1.0, -1.0, 1.0),
        new(0.0, -1.0, -1.0, -1.0),
        new(1.0, 0.0, 1.0, 1.0),
        new(1.0, 0.0, 1.0, -1.0),
        new(1.0, 0.0, -1.0, 1.0),
        new(1.0, 0.0, -1.0, -1.0),
        new(-1.0, 0.0, 1.0, 1.0),
        new(-1.0, 0.0, 1.0, -1.0),
        new(-1.0, 0.0, -1.0, 1.0),
        new(-1.0, 0.0, -1.0, -1.0),
        new(1.0, 1.0, 0.0, 1.0),
        new(1.0, 1.0, 0.0, -1.0),
        new(1.0, -1.0, 0.0, 1.0),
        new(1.0, -1.0, 0.0, -1.0),
        new(-1.0, 1.0, 0.0, 1.0),
        new(-1.0, 1.0, 0.0, -1.0),
        new(-1.0, -1.0, 0.0, 1.0),
        new(-1.0, -1.0, 0.0, -1.0),
        new(1.0, 1.0, 1.0, 0.0),
        new(1.0, 1.0, -1.0, 0.0),
        new(1.0, -1.0, 1.0, 0.0),
        new(1.0, -1.0, -1.0, 0.0),
        new(-1.0, 1.0, 1.0, 0.0),
        new(-1.0, 1.0, -1.0, 0.0),
        new(-1.0, -1.0, 1.0, 0.0),
        new(-1.0, -1.0, -1.0, 0.0)
    };

    public static short[] p = new short[256]
    {
        151,
        160,
        137,
        91,
        90,
        15,
        131,
        13,
        201,
        95,
        96,
        53,
        194,
        233,
        7,
        225,
        140,
        36,
        103,
        30,
        69,
        142,
        8,
        99,
        37,
        240,
        21,
        10,
        23,
        190,
        6,
        148,
        247,
        120,
        234,
        75,
        0,
        26,
        197,
        62,
        94,
        252,
        219,
        203,
        117,
        35,
        11,
        32,
        57,
        177,
        33,
        88,
        237,
        149,
        56,
        87,
        174,
        20,
        125,
        136,
        171,
        168,
        68,
        175,
        74,
        165,
        71,
        134,
        139,
        48,
        27,
        166,
        77,
        146,
        158,
        231,
        83,
        111,
        229,
        122,
        60,
        211,
        133,
        230,
        220,
        105,
        92,
        41,
        55,
        46,
        245,
        40,
        244,
        102,
        143,
        54,
        65,
        25,
        63,
        161,
        1,
        216,
        80,
        73,
        209,
        76,
        132,
        187,
        208,
        89,
        18,
        169,
        200,
        196,
        135,
        130,
        116,
        188,
        159,
        86,
        164,
        100,
        109,
        198,
        173,
        186,
        3,
        64,
        52,
        217,
        226,
        250,
        124,
        123,
        5,
        202,
        38,
        147,
        118,
        126,
        byte.MaxValue,
        82,
        85,
        212,
        207,
        206,
        59,
        227,
        47,
        16,
        58,
        17,
        182,
        189,
        28,
        42,
        223,
        183,
        170,
        213,
        119,
        248,
        152,
        2,
        44,
        154,
        163,
        70,
        221,
        153,
        101,
        155,
        167,
        43,
        172,
        9,
        129,
        22,
        39,
        253,
        19,
        98,
        108,
        110,
        79,
        113,
        224,
        232,
        178,
        185,
        112,
        104,
        218,
        246,
        97,
        228,
        251,
        34,
        242,
        193,
        238,
        210,
        144,
        12,
        191,
        179,
        162,
        241,
        81,
        51,
        145,
        235,
        249,
        14,
        239,
        107,
        49,
        192,
        214,
        31,
        181,
        199,
        106,
        157,
        184,
        84,
        204,
        176,
        115,
        121,
        50,
        45,
        sbyte.MaxValue,
        4,
        150,
        254,
        138,
        236,
        205,
        93,
        222,
        114,
        67,
        29,
        24,
        72,
        243,
        141,
        128,
        195,
        78,
        66,
        215,
        61,
        156,
        180
    };

    private static readonly double F2 = 0.5 * (Math.Sqrt(3.0) - 1.0);
    private static readonly double G2 = (3.0 - Math.Sqrt(3.0)) / 6.0;
    private static readonly double F3 = 1.0 / 3.0;
    private static readonly double G3 = 1.0 / 6.0;
    private static readonly double F4 = (Math.Sqrt(5.0) - 1.0) / 4.0;
    private static readonly double G4 = (5.0 - Math.Sqrt(5.0)) / 20.0;
    public short[] perm = new short[512];
    public short[] permMod12 = new short[512];

    public SimplexNoise()
    {
        InitMember();
    }

    public SimplexNoise(int seed)
    {
        InitMember(seed);
    }

    private static int fastfloor(double x)
    {
        var num = (int)x;
        return x >= num ? num : num - 1;
    }

    private static double dot(Grad g, double x, double y)
    {
        return g.x * x + g.y * y;
    }

    private static double dot(Grad g, double x, double y, double z)
    {
        return g.x * x + g.y * y + g.z * z;
    }

    private static double dot(Grad g, double x, double y, double z, double w)
    {
        return g.x * x + g.y * y + g.z * z + g.w * w;
    }

    private void InitMember()
    {
        for (var index = 0; index < 512; ++index)
        {
            perm[index] = p[index & byte.MaxValue];
            permMod12[index] = (short)(perm[index] % 12);
        }
    }

    private void InitMember(int seed)
    {
        lock (p)
        {
            for (var index = 0; index < 256; ++index)
                p[index] = (short)index;
            var dotNet35Random = new DotNet35Random(seed);
            for (var index1 = 0; index1 < 256; ++index1)
            {
                var index2 = dotNet35Random.Next(0, 256);
                int num = p[index1];
                p[index1] = p[index2];
                p[index2] = (short)num;
            }

            for (var index = 0; index < 512; ++index)
            {
                perm[index] = p[index & byte.MaxValue];
                permMod12[index] = (short)(perm[index] % 12);
            }
        }
    }

    public double Noise(double xin)
    {
        var num1 = fastfloor(xin);
        var num2 = num1 + 1;
        var num3 = xin - num1;
        var num4 = num3 - 1.0;
        var num5 = 1.0 - num3 * num3;
        var num6 = num5 * num5;
        var num7 = num6 * num6 * grad3[perm[num1 & byte.MaxValue] & 7].x * num3;
        var num8 = 1.0 - num4 * num4;
        var num9 = num8 * num8;
        var num10 = num9 * num9 * grad3[perm[num2 & byte.MaxValue] & 7].x * num4;
        return (num7 + num10 - 0.0) * (256.0 / 81.0);
    }

    public double Noise(double xin, double yin)
    {
        var num1 = (xin + yin) * F2;
        var num2 = fastfloor(xin + num1);
        var num3 = fastfloor(yin + num1);
        var num4 = (num2 + num3) * G2;
        var num5 = num2 - num4;
        var num6 = num3 - num4;
        var x1 = xin - num5;
        var y1 = yin - num6;
        int num7;
        int num8;
        if (x1 > y1)
        {
            num7 = 1;
            num8 = 0;
        }
        else
        {
            num7 = 0;
            num8 = 1;
        }

        var x2 = x1 - num7 + G2;
        var y2 = y1 - num8 + G2;
        var x3 = x1 - 1.0 + 2.0 * G2;
        var y3 = y1 - 1.0 + 2.0 * G2;
        var num9 = num2 & byte.MaxValue;
        var index1 = num3 & byte.MaxValue;
        int index2 = permMod12[num9 + perm[index1]];
        int index3 = permMod12[num9 + num7 + perm[index1 + num8]];
        int index4 = permMod12[num9 + 1 + perm[index1 + 1]];
        var num10 = 0.5 - x1 * x1 - y1 * y1;
        double num11;
        if (num10 < 0.0)
        {
            num11 = 0.0;
        }
        else
        {
            var num12 = num10 * num10;
            num11 = num12 * num12 * dot(grad3[index2], x1, y1);
        }

        var num13 = 0.5 - x2 * x2 - y2 * y2;
        double num14;
        if (num13 < 0.0)
        {
            num14 = 0.0;
        }
        else
        {
            var num15 = num13 * num13;
            num14 = num15 * num15 * dot(grad3[index3], x2, y2);
        }

        var num16 = 0.5 - x3 * x3 - y3 * y3;
        double num17;
        if (num16 < 0.0)
        {
            num17 = 0.0;
        }
        else
        {
            var num18 = num16 * num16;
            num17 = num18 * num18 * dot(grad3[index4], x3, y3);
        }

        return 70.1475 * (num11 + num14 + num17);
    }

    public double Noise(double xin, double yin, double zin)
    {
        var num1 = (xin + yin + zin) * F3;
        var num2 = fastfloor(xin + num1);
        var num3 = fastfloor(yin + num1);
        var num4 = fastfloor(zin + num1);
        var num5 = (num2 + num3 + num4) * G3;
        var num6 = num2 - num5;
        var num7 = num3 - num5;
        var num8 = num4 - num5;
        var x1 = xin - num6;
        var y1 = yin - num7;
        var z1 = zin - num8;
        int num9;
        int num10;
        int num11;
        int num12;
        int num13;
        int num14;
        if (x1 >= y1)
        {
            if (y1 >= z1)
            {
                num9 = 1;
                num10 = 0;
                num11 = 0;
                num12 = 1;
                num13 = 1;
                num14 = 0;
            }
            else if (x1 >= z1)
            {
                num9 = 1;
                num10 = 0;
                num11 = 0;
                num12 = 1;
                num13 = 0;
                num14 = 1;
            }
            else
            {
                num9 = 0;
                num10 = 0;
                num11 = 1;
                num12 = 1;
                num13 = 0;
                num14 = 1;
            }
        }
        else if (y1 < z1)
        {
            num9 = 0;
            num10 = 0;
            num11 = 1;
            num12 = 0;
            num13 = 1;
            num14 = 1;
        }
        else if (x1 < z1)
        {
            num9 = 0;
            num10 = 1;
            num11 = 0;
            num12 = 0;
            num13 = 1;
            num14 = 1;
        }
        else
        {
            num9 = 0;
            num10 = 1;
            num11 = 0;
            num12 = 1;
            num13 = 1;
            num14 = 0;
        }

        var x2 = x1 - num9 + G3;
        var y2 = y1 - num10 + G3;
        var z2 = z1 - num11 + G3;
        var x3 = x1 - num12 + 2.0 * G3;
        var y3 = y1 - num13 + 2.0 * G3;
        var z3 = z1 - num14 + 2.0 * G3;
        var x4 = x1 - 1.0 + 3.0 * G3;
        var y4 = y1 - 1.0 + 3.0 * G3;
        var z4 = z1 - 1.0 + 3.0 * G3;
        var num15 = num2 & byte.MaxValue;
        var num16 = num3 & byte.MaxValue;
        var index1 = num4 & byte.MaxValue;
        int index2 = permMod12[num15 + perm[num16 + perm[index1]]];
        int index3 = permMod12[num15 + num9 + perm[num16 + num10 + perm[index1 + num11]]];
        int index4 = permMod12[num15 + num12 + perm[num16 + num13 + perm[index1 + num14]]];
        int index5 = permMod12[num15 + 1 + perm[num16 + 1 + perm[index1 + 1]]];
        var num17 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1;
        double num18;
        if (num17 < 0.0)
        {
            num18 = 0.0;
        }
        else
        {
            var num19 = num17 * num17;
            num18 = num19 * num19 * dot(grad3[index2], x1, y1, z1);
        }

        var num20 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2;
        double num21;
        if (num20 < 0.0)
        {
            num21 = 0.0;
        }
        else
        {
            var num22 = num20 * num20;
            num21 = num22 * num22 * dot(grad3[index3], x2, y2, z2);
        }

        var num23 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3;
        double num24;
        if (num23 < 0.0)
        {
            num24 = 0.0;
        }
        else
        {
            var num25 = num23 * num23;
            num24 = num25 * num25 * dot(grad3[index4], x3, y3, z3);
        }

        var num26 = 0.6 - x4 * x4 - y4 * y4 - z4 * z4;
        double num27;
        if (num26 < 0.0)
        {
            num27 = 0.0;
        }
        else
        {
            var num28 = num26 * num26;
            num27 = num28 * num28 * dot(grad3[index5], x4, y4, z4);
        }

        return 32.696434 * (num18 + num21 + num24 + num27);
    }

    public double Noise(double x, double y, double z, double w)
    {
        var num1 = (x + y + z + w) * F4;
        var num2 = fastfloor(x + num1);
        var num3 = fastfloor(y + num1);
        var num4 = fastfloor(z + num1);
        var num5 = fastfloor(w + num1);
        var num6 = (num2 + num3 + num4 + num5) * G4;
        var num7 = num2 - num6;
        var num8 = num3 - num6;
        var num9 = num4 - num6;
        var num10 = num5 - num6;
        var x1 = x - num7;
        var y1 = y - num8;
        var z1 = z - num9;
        var w1 = w - num10;
        var num11 = 0;
        var num12 = 0;
        var num13 = 0;
        var num14 = 0;
        if (x1 > y1)
            ++num11;
        else
            ++num12;
        if (x1 > z1)
            ++num11;
        else
            ++num13;
        if (x1 > w1)
            ++num11;
        else
            ++num14;
        if (y1 > z1)
            ++num12;
        else
            ++num13;
        if (y1 > w1)
            ++num12;
        else
            ++num14;
        if (z1 > w1)
            ++num13;
        else
            ++num14;
        var num15 = num11 >= 3 ? 1 : 0;
        var num16 = num12 >= 3 ? 1 : 0;
        var num17 = num13 >= 3 ? 1 : 0;
        var num18 = num14 >= 3 ? 1 : 0;
        var num19 = num11 >= 2 ? 1 : 0;
        var num20 = num12 >= 2 ? 1 : 0;
        var num21 = num13 >= 2 ? 1 : 0;
        var num22 = num14 >= 2 ? 1 : 0;
        var num23 = num11 >= 1 ? 1 : 0;
        var num24 = num12 >= 1 ? 1 : 0;
        var num25 = num13 >= 1 ? 1 : 0;
        var num26 = num14 >= 1 ? 1 : 0;
        var x2 = x1 - num15 + G4;
        var y2 = y1 - num16 + G4;
        var z2 = z1 - num17 + G4;
        var w2 = w1 - num18 + G4;
        var x3 = x1 - num19 + 2.0 * G4;
        var y3 = y1 - num20 + 2.0 * G4;
        var z3 = z1 - num21 + 2.0 * G4;
        var w3 = w1 - num22 + 2.0 * G4;
        var x4 = x1 - num23 + 3.0 * G4;
        var y4 = y1 - num24 + 3.0 * G4;
        var z4 = z1 - num25 + 3.0 * G4;
        var w4 = w1 - num26 + 3.0 * G4;
        var x5 = x1 - 1.0 + 4.0 * G4;
        var y5 = y1 - 1.0 + 4.0 * G4;
        var z5 = z1 - 1.0 + 4.0 * G4;
        var w5 = w1 - 1.0 + 4.0 * G4;
        var num27 = num2 & byte.MaxValue;
        var num28 = num3 & byte.MaxValue;
        var num29 = num4 & byte.MaxValue;
        var index1 = num5 & byte.MaxValue;
        var index2 = perm[num27 + perm[num28 + perm[num29 + perm[index1]]]] % 32;
        var index3 = perm[num27 + num15 + perm[num28 + num16 + perm[num29 + num17 + perm[index1 + num18]]]] % 32;
        var index4 = perm[num27 + num19 + perm[num28 + num20 + perm[num29 + num21 + perm[index1 + num22]]]] % 32;
        var index5 = perm[num27 + num23 + perm[num28 + num24 + perm[num29 + num25 + perm[index1 + num26]]]] % 32;
        var index6 = perm[num27 + 1 + perm[num28 + 1 + perm[num29 + 1 + perm[index1 + 1]]]] % 32;
        var num30 = 0.6 - x1 * x1 - y1 * y1 - z1 * z1 - w1 * w1;
        double num31;
        if (num30 < 0.0)
        {
            num31 = 0.0;
        }
        else
        {
            var num32 = num30 * num30;
            num31 = num32 * num32 * dot(grad4[index2], x1, y1, z1, w1);
        }

        var num33 = 0.6 - x2 * x2 - y2 * y2 - z2 * z2 - w2 * w2;
        double num34;
        if (num33 < 0.0)
        {
            num34 = 0.0;
        }
        else
        {
            var num35 = num33 * num33;
            num34 = num35 * num35 * dot(grad4[index3], x2, y2, z2, w2);
        }

        var num36 = 0.6 - x3 * x3 - y3 * y3 - z3 * z3 - w3 * w3;
        double num37;
        if (num36 < 0.0)
        {
            num37 = 0.0;
        }
        else
        {
            var num38 = num36 * num36;
            num37 = num38 * num38 * dot(grad4[index4], x3, y3, z3, w3);
        }

        var num39 = 0.6 - x4 * x4 - y4 * y4 - z4 * z4 - w4 * w4;
        double num40;
        if (num39 < 0.0)
        {
            num40 = 0.0;
        }
        else
        {
            var num41 = num39 * num39;
            num40 = num41 * num41 * dot(grad4[index5], x4, y4, z4, w4);
        }

        var num42 = 0.6 - x5 * x5 - y5 * y5 - z5 * z5 - w5 * w5;
        double num43;
        if (num42 < 0.0)
        {
            num43 = 0.0;
        }
        else
        {
            var num44 = num42 * num42;
            num43 = num44 * num44 * dot(grad4[index6], x5, y5, z5, w5);
        }

        return 27.29 * (num31 + num34 + num37 + num40 + num43);
    }

    public double Noise2DFBM(double x, double y, int nOctaves, double deltaAmp = 0.5, double deltaWLen = 2.0)
    {
        var num1 = 0.0;
        var num2 = 0.5;
        for (var index = 0; index < nOctaves; ++index)
        {
            num1 += Noise(x, y) * num2;
            num2 *= deltaAmp;
            x *= deltaWLen;
            y *= deltaWLen;
        }

        return num1;
    }

    public double Noise3DFBM(
        double x,
        double y,
        double z,
        int nOctaves,
        double deltaAmp = 0.5,
        double deltaWLen = 2.0)
    {
        var num1 = 0.0;
        var num2 = 0.5;
        for (var index = 0; index < nOctaves; ++index)
        {
            num1 += Noise(x, y, z) * num2;
            num2 *= deltaAmp;
            x *= deltaWLen;
            y *= deltaWLen;
            z *= deltaWLen;
        }

        return num1;
    }

    public double Noise3DFBMInitialAmp(
        double x,
        double y,
        double z,
        int nOctaves,
        double deltaAmp = 0.5,
        double deltaWLen = 2.0,
        double initialAmp = 0.5)
    {
        var num1 = 0.0;
        var num2 = initialAmp;
        for (var index = 0; index < nOctaves; ++index)
        {
            num1 += Noise(x, y, z) * num2;
            num2 *= deltaAmp;
            x *= deltaWLen;
            y *= deltaWLen;
            z *= deltaWLen;
        }

        return num1;
    }

    public double RidgedNoise(
        double x,
        double y,
        double z,
        int nOctaves,
        double deltaAmp = 0.5,
        double deltaWLen = 2.0,
        double initialAmp = 0.5)
    {
        var num1 = 0.0;
        var num2 = initialAmp;
        for (var index = 0; index < nOctaves; ++index)
        {
            num1 += Math.Abs(Noise(x, y, z) * num2);
            num2 *= deltaAmp;
            x *= deltaWLen;
            y *= deltaWLen;
            z *= deltaWLen;
        }

        return num1;
    }

    public class Grad
    {
        public double w;
        public double x;
        public double y;
        public double z;

        public Grad(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Grad(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }
}