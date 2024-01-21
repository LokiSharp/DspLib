using DspLib.Algorithms;
using DspLib.Enum;
using DspLib.Galaxy;

public static class StarGen
{
    public const double GRAVITY = 1.3538551990520382E-06;
    public const float E = 2.71828175f;
    private const double PI = 3.1415926535897931;

    public static float[] orbitRadius = new float[17]
    {
        0.0f,
        0.4f,
        0.7f,
        1f,
        1.4f,
        1.9f,
        2.5f,
        3.3f,
        4.3f,
        5.5f,
        6.9f,
        8.4f,
        10f,
        11.7f,
        13.5f,
        15.4f,
        17.5f
    };

    public static float[] hiveOrbitRadius = new float[18]
    {
        0.4f,
        0.55f,
        0.7f,
        0.83f,
        1f,
        1.2f,
        1.4f,
        1.58f,
        1.72f,
        1.9f,
        2.11f,
        2.29f,
        2.5f,
        2.78f,
        3.02f,
        3.3f,
        3.6f,
        3.9f
    };

    public static int[] planet2HiveOrbitTable = new int[8]
    {
        0,
        0,
        2,
        4,
        6,
        9,
        12,
        15
    };

    public static bool[] hiveOrbitCondition = new bool[hiveOrbitRadius.Length];
    public static float specifyBirthStarMass = 0.0f;
    public static float specifyBirthStarAge = 0.0f;
    private static readonly double[] pGas = new double[10];

    public static StarData CreateStar(
        GalaxyData galaxy,
        VectorLF3 pos,
        GameDesc gameDesc,
        int id,
        int seed,
        EStarType needtype,
        ESpectrType needSpectr = ESpectrType.X)
    {
        var star = new StarData
        {
            galaxy = galaxy,
            index = id - 1
        };
        star.level = galaxy.starCount <= 1 ? 0.0f : star.index / (float)(galaxy.starCount - 1);
        star.id = id;
        star.seed = seed;
        var dotNet35Random1 = new DotNet35Random(seed);
        var seed1 = dotNet35Random1.Next();
        var Seed = dotNet35Random1.Next();
        star.position = pos;
        var magnitude = (float)pos.magnitude;
        var num1 = magnitude / 32f;
        if (num1 > 1.0)
            num1 = Mathf.Log(Mathf.Log(Mathf.Log(Mathf.Log(Mathf.Log(num1) + 1f) + 1f) + 1f) + 1f) + 1f;
        star.resourceCoef = Mathf.Pow(7f, num1) * 0.6f;
        var dotNet35Random2 = new DotNet35Random(Seed);
        var r1_1 = dotNet35Random2.NextDouble();
        var r2_1 = dotNet35Random2.NextDouble();
        var num2 = dotNet35Random2.NextDouble();
        var rn = dotNet35Random2.NextDouble();
        var rt = dotNet35Random2.NextDouble();
        var num3 = (dotNet35Random2.NextDouble() - 0.5) * 0.2;
        var num4 = dotNet35Random2.NextDouble() * 0.2 + 0.9;
        var y = dotNet35Random2.NextDouble() * 0.4 - 0.2;
        var num5 = Math.Pow(2.0, y);
        var dotNet35Random3 = new DotNet35Random(dotNet35Random2.Next());
        var num6 = dotNet35Random3.NextDouble();
        var num7 = Mathf.Lerp(-0.98f, 0.88f, star.level);
        var averageValue = num7 >= 0.0 ? num7 + 0.65f : num7 - 0.65f;
        var standardDeviation1 = 0.33f;
        if (needtype == EStarType.GiantStar)
        {
            averageValue = y > -0.08 ? -1.5f : 1.6f;
            standardDeviation1 = 0.3f;
        }

        var num8 = RandNormal(averageValue, standardDeviation1, r1_1, r2_1);
        switch (needSpectr)
        {
            case ESpectrType.M:
                num8 = -3f;
                break;
            case ESpectrType.O:
                num8 = 3f;
                break;
        }

        var p1 = (float)(Mathf.Clamp(num8 <= 0.0 ? num8 * 1f : num8 * 2f, -2.4f, 4.65f) + num3 + 1.0);
        switch (needtype)
        {
            case EStarType.WhiteDwarf:
                star.mass = (float)(1.0 + r2_1 * 5.0);
                break;
            case EStarType.NeutronStar:
                star.mass = (float)(7.0 + r1_1 * 11.0);
                break;
            case EStarType.BlackHole:
                star.mass = (float)(18.0 + r1_1 * r2_1 * 30.0);
                break;
            default:
                star.mass = Mathf.Pow(2f, p1);
                break;
        }

        var d = 5.0;
        if (star.mass < 2.0)
            d = 2.0 + 0.4 * (1.0 - star.mass);
        star.lifetime = (float)(10000.0 * Math.Pow(0.1, Math.Log10(star.mass * 0.5) / Math.Log10(d) + 1.0) * num4);
        switch (needtype)
        {
            case EStarType.GiantStar:
                star.lifetime = (float)(10000.0 * Math.Pow(0.1, Math.Log10(star.mass * 0.58) / Math.Log10(d) + 1.0) *
                                        num4);
                star.age = (float)(num2 * 0.039999999105930328 + 0.95999997854232788);
                break;
            case EStarType.WhiteDwarf:
            case EStarType.NeutronStar:
            case EStarType.BlackHole:
                star.age = (float)(num2 * 0.40000000596046448 + 1.0);
                if (needtype == EStarType.WhiteDwarf)
                {
                    star.lifetime += 10000f;
                    break;
                }

                if (needtype == EStarType.NeutronStar) star.lifetime += 1000f;
                break;
            default:
                star.age = star.mass >= 0.5
                    ? star.mass >= 0.8
                        ? (float)(num2 * 0.699999988079071 + 0.20000000298023224)
                        : (float)(num2 * 0.40000000596046448 + 0.10000000149011612)
                    : (float)(num2 * 0.11999999731779099 + 0.019999999552965164);
                break;
        }

        var num9 = star.lifetime * star.age;
        if (num9 > 5000.0)
            num9 = (float)((Mathf.Log(num9 / 5000f) + 1.0) * 5000.0);
        if (num9 > 8000.0)
            num9 = (float)((Mathf.Log(Mathf.Log(Mathf.Log(num9 / 8000f) + 1f) + 1f) + 1.0) * 8000.0);
        star.lifetime = num9 / star.age;
        var num10 = (float)(1.0 - Mathf.Pow(Mathf.Clamp01(star.age), 20f) * 0.5) * star.mass;
        star.temperature = (float)(Math.Pow(num10, 0.56 + 0.14 / (Math.Log10(num10 + 4.0) / Math.Log10(5.0))) * 4450.0 +
                                   1300.0);
        var num11 = Math.Log10((star.temperature - 1300.0) / 4500.0) / Math.Log10(2.6) - 0.5;
        if (num11 < 0.0)
            num11 *= 4.0;
        if (num11 > 2.0)
            num11 = 2.0;
        else if (num11 < -4.0)
            num11 = -4.0;
        star.spectr = (ESpectrType)Mathf.RoundToInt((float)num11 + 4f);
        star.color = Mathf.Clamp01((float)((num11 + 3.5) * 0.20000000298023224));
        star.classFactor = (float)num11;
        star.luminosity = Mathf.Pow(num10, 0.7f);
        star.radius = (float)(Math.Pow(star.mass, 0.4) * num5);
        star.acdiskRadius = 0.0f;
        var p2 = (float)num11 + 2f;
        star.habitableRadius = Mathf.Pow(1.7f, p2) + 0.25f * Mathf.Min(1f, star.orbitScaler);
        star.lightBalanceRadius = Mathf.Pow(1.7f, p2);
        star.orbitScaler = Mathf.Pow(1.35f, p2);
        if (star.orbitScaler < 1.0)
            star.orbitScaler = Mathf.Lerp(star.orbitScaler, 1f, 0.6f);
        SetStarAge(star, star.age, rn, rt);
        star.dysonRadius = star.orbitScaler * 0.28f;
        if (star.dysonRadius * 40000.0 < star.physicsRadius * 1.5)
            star.dysonRadius = (float)(star.physicsRadius * 1.5 / 40000.0);
        star.uPosition = star.position * 2400000.0;
        star.name = NameGen.RandomStarName(seed1, star, galaxy);
        star.overrideName = "";
        var b = Mathf.Pow(star.color, 1.3f);
        var f1 = Mathf.Clamp((float)((magnitude - 2.0) / 20.0), 0.0f, 2.5f);
        if (f1 > 1.0)
            f1 = Mathf.Log(Mathf.Log(f1) + 1f) + 1f;
        var f2 = f1 / 1.4f;
        if (star.type == EStarType.BlackHole)
            b = 5f;
        else if (star.type == EStarType.NeutronStar)
            b = 1.7f;
        else if (star.type == EStarType.WhiteDwarf)
            b = 1.2f;
        else if (star.type == EStarType.GiantStar)
            b = Mathf.Max(0.6f, b);
        else if (star.spectr == ESpectrType.O)
            b += 0.05f;
        var num12 = Mathf.Clamp01((float)(1.0 - Mathf.Pow(b * 0.9f + 0.07f, 0.73f) * (double)Mathf.Pow(f2, 0.27f) +
            num6 * 0.079999998211860657 - 0.039999999105930328));
        star.hivePatternLevel = num12 < 0.699999988079071 ? num12 < 0.30000001192092896 ? 2 : 1 : 0;
        star.safetyFactor = num12;
        var num13 = dotNet35Random3.Next(0, 1000);
        var num14 = star.epicHive ? 2 : 1;
        star.maxHiveCount = (int)(gameDesc.combatSettings.maxDensity * (double)num14 * 1000.0 + num13 + 0.5) / 1000;
        var initialColonize = gameDesc.combatSettings.initialColonize;
        if (initialColonize < 0.014999999664723873)
        {
            star.initialHiveCount = 0;
        }
        else
        {
            var a = Mathf.Clamp01((float)(1.0 - Mathf.Pow(Mathf.Clamp01(star.safetyFactor - 0.2f), 0.86f) -
                                          (star.maxHiveCount - 1) * 0.05000000074505806)) *
                    (float)(1.1000000238418579 - star.maxHiveCount * 0.10000000149011612);
            var num15 = initialColonize > 1.0
                ? Mathf.Lerp(a, (float)(1.0 + (initialColonize - 1.0) * 0.20000000298023224),
                    (float)((initialColonize - 1.0) * 0.5))
                : a * initialColonize;
            if (star.type == EStarType.GiantStar)
                num15 *= 1.2f;
            else if (star.type == EStarType.WhiteDwarf)
                num15 *= 1.4f;
            else if (star.type == EStarType.NeutronStar)
                num15 *= 1.6f;
            else if (star.type == EStarType.BlackHole)
                num15 *= 1.8f;
            else if (star.spectr == ESpectrType.O)
                num15 *= 1.1f;
            var num16 = num15 * star.maxHiveCount;
            if (num16 > star.maxHiveCount + 0.75)
                num16 = star.maxHiveCount + 0.75f;
            var standardDeviation2 = 0.5f;
            if (num16 <= 0.01)
                standardDeviation2 = 0.0f;
            else if (num16 < 1.0)
                standardDeviation2 = (float)(Mathf.Sqrt(num16) * 0.28999999165534973 + 0.20999999344348907);
            else if (num16 > 1.0)
                standardDeviation2 = (float)(0.30000001192092896 + 0.20000000298023224 * num16);
            var num17 = 64;
            do
            {
                var r1_2 = dotNet35Random3.NextDouble();
                var r2_2 = dotNet35Random3.NextDouble();
                star.initialHiveCount = (int)(RandNormal(num16, standardDeviation2, r1_2, r2_2) + 0.5);
            } while (num17-- > 0 && (star.initialHiveCount < 0 || star.initialHiveCount > star.maxHiveCount));

            if (star.initialHiveCount < 0)
                star.initialHiveCount = 0;
            else if (star.initialHiveCount > star.maxHiveCount)
                star.initialHiveCount = star.maxHiveCount;
        }

        if (star.type == EStarType.BlackHole)
        {
            var num18 = (int)(gameDesc.combatSettings.maxDensity * 1000.0 + num13 + 0.5) / 1000;
            if (star.initialHiveCount < num18)
                star.initialHiveCount = num18;
            if (star.initialHiveCount < 1)
                star.initialHiveCount = 1;
        }

        return star;
    }

    public static StarData CreateBirthStar(GalaxyData galaxy, GameDesc gameDesc, int seed)
    {
        var birthStar = new StarData();
        birthStar.galaxy = galaxy;
        birthStar.index = 0;
        birthStar.level = 0.0f;
        birthStar.id = 1;
        birthStar.seed = seed;
        birthStar.resourceCoef = 0.6f;
        var dotNet35Random1 = new DotNet35Random(seed);
        var seed1 = dotNet35Random1.Next();
        var Seed = dotNet35Random1.Next();
        birthStar.name = NameGen.RandomName(seed1);
        birthStar.overrideName = "";
        birthStar.position = VectorLF3.zero;
        var dotNet35Random2 = new DotNet35Random(Seed);
        var r1_1 = dotNet35Random2.NextDouble();
        var r2_1 = dotNet35Random2.NextDouble();
        var num1 = dotNet35Random2.NextDouble();
        var rn = dotNet35Random2.NextDouble();
        var rt = dotNet35Random2.NextDouble();
        var num2 = dotNet35Random2.NextDouble() * 0.2 + 0.9;
        var num3 = Math.Pow(2.0, dotNet35Random2.NextDouble() * 0.4 - 0.2);
        var dotNet35Random3 = new DotNet35Random(dotNet35Random2.Next());
        var num4 = dotNet35Random3.NextDouble();
        var p1 = Mathf.Clamp(RandNormal(0.0f, 0.08f, r1_1, r2_1), -0.2f, 0.2f);
        birthStar.mass = Mathf.Pow(2f, p1);
        if (specifyBirthStarMass > 0.10000000149011612)
            birthStar.mass = specifyBirthStarMass;
        if (specifyBirthStarAge > 9.9999997473787516E-06)
            birthStar.age = specifyBirthStarAge;
        var d = 2.0 + 0.4 * (1.0 - birthStar.mass);
        birthStar.lifetime =
            (float)(10000.0 * Math.Pow(0.1, Math.Log10(birthStar.mass * 0.5) / Math.Log10(d) + 1.0) * num2);
        birthStar.age = (float)(num1 * 0.4 + 0.3);
        if (specifyBirthStarAge > 9.9999997473787516E-06)
            birthStar.age = specifyBirthStarAge;
        var num5 = (float)(1.0 - Mathf.Pow(Mathf.Clamp01(birthStar.age), 20f) * 0.5) * birthStar.mass;
        birthStar.temperature =
            (float)(Math.Pow(num5, 0.56 + 0.14 / (Math.Log10(num5 + 4.0) / Math.Log10(5.0))) * 4450.0 + 1300.0);
        var num6 = Math.Log10((birthStar.temperature - 1300.0) / 4500.0) / Math.Log10(2.6) - 0.5;
        if (num6 < 0.0)
            num6 *= 4.0;
        if (num6 > 2.0)
            num6 = 2.0;
        else if (num6 < -4.0)
            num6 = -4.0;
        birthStar.spectr = (ESpectrType)Mathf.RoundToInt((float)num6 + 4f);
        birthStar.color = Mathf.Clamp01((float)((num6 + 3.5) * 0.20000000298023224));
        birthStar.classFactor = (float)num6;
        birthStar.luminosity = Mathf.Pow(num5, 0.7f);
        birthStar.radius = (float)(Math.Pow(birthStar.mass, 0.4) * num3);
        birthStar.acdiskRadius = 0.0f;
        var p2 = (float)num6 + 2f;
        birthStar.habitableRadius = Mathf.Pow(1.7f, p2) + 0.2f * Mathf.Min(1f, birthStar.orbitScaler);
        birthStar.lightBalanceRadius = Mathf.Pow(1.7f, p2);
        birthStar.orbitScaler = Mathf.Pow(1.35f, p2);
        if (birthStar.orbitScaler < 1.0)
            birthStar.orbitScaler = Mathf.Lerp(birthStar.orbitScaler, 1f, 0.6f);
        SetStarAge(birthStar, birthStar.age, rn, rt);
        birthStar.dysonRadius = birthStar.orbitScaler * 0.28f;
        if (birthStar.dysonRadius * 40000.0 < birthStar.physicsRadius * 1.5)
            birthStar.dysonRadius = (float)(birthStar.physicsRadius * 1.5 / 40000.0);
        birthStar.uPosition = VectorLF3.zero;
        birthStar.name = NameGen.RandomStarName(seed1, birthStar, galaxy);
        birthStar.overrideName = "";
        birthStar.hivePatternLevel = 0;
        birthStar.safetyFactor = (float)(0.847000002861023 + num4 * 0.026000000536441803);
        var num7 = dotNet35Random3.Next(0, 1000);
        birthStar.maxHiveCount = (int)(gameDesc.combatSettings.maxDensity * 1000.0 + num7 + 0.5) / 1000;
        var initialColonize = gameDesc.combatSettings.initialColonize;
        var num8 = initialColonize * (double)birthStar.maxHiveCount < 0.699999988079071 ? 0 : 1;
        if (initialColonize < 0.014999999664723873)
        {
            birthStar.initialHiveCount = 0;
        }
        else
        {
            var num9 = 0.6f * initialColonize * birthStar.maxHiveCount;
            var standardDeviation = 0.5f;
            if (num9 < 1.0)
                standardDeviation = (float)(Mathf.Sqrt(num9) * 0.28999999165534973 + 0.20999999344348907);
            else if (num9 > (double)birthStar.maxHiveCount)
                num9 = birthStar.maxHiveCount;
            var num10 = 16;
            do
            {
                var r1_2 = dotNet35Random3.NextDouble();
                var r2_2 = dotNet35Random3.NextDouble();
                birthStar.initialHiveCount = (int)(RandNormal(num9, standardDeviation, r1_2, r2_2) + 0.5);
            } while (num10-- > 0 &&
                     (birthStar.initialHiveCount < 0 || birthStar.initialHiveCount > birthStar.maxHiveCount));

            if (birthStar.initialHiveCount < num8)
                birthStar.initialHiveCount = num8;
            else if (birthStar.initialHiveCount > birthStar.maxHiveCount)
                birthStar.initialHiveCount = birthStar.maxHiveCount;
        }

        return birthStar;
    }

    private static double _signpow(double x, double pow)
    {
        var num = x > 0.0 ? 1.0 : -1.0;
        return Math.Abs(Math.Pow(x, pow)) * num;
    }

    public static void CreateStarPlanets(GalaxyData galaxy, StarData star, GameDesc gameDesc)
    {
        var dotNet35Random1 = new DotNet35Random(star.seed);
        dotNet35Random1.Next();
        dotNet35Random1.Next();
        dotNet35Random1.Next();
        var dotNet35Random2 = new DotNet35Random(dotNet35Random1.Next());
        var num1 = dotNet35Random2.NextDouble();
        var num2 = dotNet35Random2.NextDouble();
        var num3 = dotNet35Random2.NextDouble();
        var num4 = dotNet35Random2.NextDouble();
        var num5 = dotNet35Random2.NextDouble();
        var num6 = dotNet35Random2.NextDouble() * 0.2 + 0.9;
        var num7 = dotNet35Random2.NextDouble() * 0.2 + 0.9;
        var dotNet35Random3 = new DotNet35Random(dotNet35Random1.Next());
        SetHiveOrbitsConditionsTrue();
        if (star.type == EStarType.BlackHole)
        {
            star.planetCount = 1;
            star.planets = new PlanetData[star.planetCount];
            var info_seed = dotNet35Random2.Next();
            var gen_seed = dotNet35Random2.Next();
            star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 3, 1, false, info_seed,
                gen_seed);
        }
        else if (star.type == EStarType.NeutronStar)
        {
            star.planetCount = 1;
            star.planets = new PlanetData[star.planetCount];
            var info_seed = dotNet35Random2.Next();
            var gen_seed = dotNet35Random2.Next();
            star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 3, 1, false, info_seed,
                gen_seed);
        }
        else if (star.type == EStarType.WhiteDwarf)
        {
            if (num1 < 0.699999988079071)
            {
                star.planetCount = 1;
                star.planets = new PlanetData[star.planetCount];
                var info_seed = dotNet35Random2.Next();
                var gen_seed = dotNet35Random2.Next();
                star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 3, 1, false,
                    info_seed, gen_seed);
            }
            else
            {
                star.planetCount = 2;
                star.planets = new PlanetData[star.planetCount];
                if (num2 < 0.30000001192092896)
                {
                    var info_seed1 = dotNet35Random2.Next();
                    var gen_seed1 = dotNet35Random2.Next();
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 3, 1, false,
                        info_seed1, gen_seed1);
                    var info_seed2 = dotNet35Random2.Next();
                    var gen_seed2 = dotNet35Random2.Next();
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 0, 4, 2, false,
                        info_seed2, gen_seed2);
                }
                else
                {
                    var info_seed3 = dotNet35Random2.Next();
                    var gen_seed3 = dotNet35Random2.Next();
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 4, 1, true,
                        info_seed3, gen_seed3);
                    var info_seed4 = dotNet35Random2.Next();
                    var gen_seed4 = dotNet35Random2.Next();
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 1, 1, 1, false,
                        info_seed4, gen_seed4);
                }
            }
        }
        else if (star.type == EStarType.GiantStar)
        {
            if (num1 < 0.30000001192092896)
            {
                star.planetCount = 1;
                star.planets = new PlanetData[star.planetCount];
                var info_seed = dotNet35Random2.Next();
                var gen_seed = dotNet35Random2.Next();
                var orbitIndex = num3 > 0.5 ? 3 : 2;
                star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, orbitIndex, 1,
                    false, info_seed, gen_seed);
            }
            else if (num1 < 0.800000011920929)
            {
                star.planetCount = 2;
                star.planets = new PlanetData[star.planetCount];
                if (num2 < 0.25)
                {
                    var info_seed5 = dotNet35Random2.Next();
                    var gen_seed5 = dotNet35Random2.Next();
                    var orbitIndex1 = num3 > 0.5 ? 3 : 2;
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, orbitIndex1, 1,
                        false, info_seed5, gen_seed5);
                    var info_seed6 = dotNet35Random2.Next();
                    var gen_seed6 = dotNet35Random2.Next();
                    var orbitIndex2 = num3 > 0.5 ? 4 : 3;
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 0, orbitIndex2, 2,
                        false, info_seed6, gen_seed6);
                }
                else
                {
                    var info_seed7 = dotNet35Random2.Next();
                    var gen_seed7 = dotNet35Random2.Next();
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, 3, 1, true,
                        info_seed7, gen_seed7);
                    var info_seed8 = dotNet35Random2.Next();
                    var gen_seed8 = dotNet35Random2.Next();
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 1, 1, 1, false,
                        info_seed8, gen_seed8);
                }
            }
            else
            {
                star.planetCount = 3;
                star.planets = new PlanetData[star.planetCount];
                if (num2 < 0.15000000596046448)
                {
                    var info_seed9 = dotNet35Random2.Next();
                    var gen_seed9 = dotNet35Random2.Next();
                    var orbitIndex3 = num3 > 0.5 ? 3 : 2;
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, orbitIndex3, 1,
                        false, info_seed9, gen_seed9);
                    var info_seed10 = dotNet35Random2.Next();
                    var gen_seed10 = dotNet35Random2.Next();
                    var orbitIndex4 = num3 > 0.5 ? 4 : 3;
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 0, orbitIndex4, 2,
                        false, info_seed10, gen_seed10);
                    var info_seed11 = dotNet35Random2.Next();
                    var gen_seed11 = dotNet35Random2.Next();
                    var orbitIndex5 = num3 > 0.5 ? 5 : 4;
                    star.planets[2] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 2, 0, orbitIndex5, 3,
                        false, info_seed11, gen_seed11);
                }
                else if (num2 < 0.75)
                {
                    var info_seed12 = dotNet35Random2.Next();
                    var gen_seed12 = dotNet35Random2.Next();
                    var orbitIndex = num3 > 0.5 ? 3 : 2;
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, orbitIndex, 1,
                        false, info_seed12, gen_seed12);
                    var info_seed13 = dotNet35Random2.Next();
                    var gen_seed13 = dotNet35Random2.Next();
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 0, 4, 2, true,
                        info_seed13, gen_seed13);
                    var info_seed14 = dotNet35Random2.Next();
                    var gen_seed14 = dotNet35Random2.Next();
                    star.planets[2] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 2, 2, 1, 1, false,
                        info_seed14, gen_seed14);
                }
                else
                {
                    var info_seed15 = dotNet35Random2.Next();
                    var gen_seed15 = dotNet35Random2.Next();
                    var orbitIndex = num3 > 0.5 ? 4 : 3;
                    star.planets[0] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 0, 0, orbitIndex, 1,
                        true, info_seed15, gen_seed15);
                    var info_seed16 = dotNet35Random2.Next();
                    var gen_seed16 = dotNet35Random2.Next();
                    star.planets[1] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 1, 1, 1, 1, false,
                        info_seed16, gen_seed16);
                    var info_seed17 = dotNet35Random2.Next();
                    var gen_seed17 = dotNet35Random2.Next();
                    star.planets[2] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, 2, 1, 2, 2, false,
                        info_seed17, gen_seed17);
                }
            }
        }
        else
        {
            Array.Clear(pGas, 0, pGas.Length);
            if (star.index == 0)
            {
                star.planetCount = 4;
                pGas[0] = 0.0;
                pGas[1] = 0.0;
                pGas[2] = 0.0;
            }
            else if (star.spectr == ESpectrType.M)
            {
                star.planetCount = num1 >= 0.1 ? num1 >= 0.3 ? num1 >= 0.8 ? 4 : 3 : 2 : 1;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.2;
                    pGas[1] = 0.2;
                }
                else
                {
                    pGas[0] = 0.0;
                    pGas[1] = 0.2;
                    pGas[2] = 0.3;
                }
            }
            else if (star.spectr == ESpectrType.K)
            {
                star.planetCount = num1 >= 0.1 ? num1 >= 0.2 ? num1 >= 0.7 ? num1 >= 0.95 ? 5 : 4 : 3 : 2 : 1;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.18;
                    pGas[1] = 0.18;
                }
                else
                {
                    pGas[0] = 0.0;
                    pGas[1] = 0.18;
                    pGas[2] = 0.28;
                    pGas[3] = 0.28;
                }
            }
            else if (star.spectr == ESpectrType.G)
            {
                star.planetCount = num1 >= 0.4 ? num1 >= 0.9 ? 5 : 4 : 3;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.18;
                    pGas[1] = 0.18;
                }
                else
                {
                    pGas[0] = 0.0;
                    pGas[1] = 0.2;
                    pGas[2] = 0.3;
                    pGas[3] = 0.3;
                }
            }
            else if (star.spectr == ESpectrType.F)
            {
                star.planetCount = num1 >= 0.35 ? num1 >= 0.8 ? 5 : 4 : 3;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.2;
                    pGas[1] = 0.2;
                }
                else
                {
                    pGas[0] = 0.0;
                    pGas[1] = 0.22;
                    pGas[2] = 0.31;
                    pGas[3] = 0.31;
                }
            }
            else if (star.spectr == ESpectrType.A)
            {
                star.planetCount = num1 >= 0.3 ? num1 >= 0.75 ? 5 : 4 : 3;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.2;
                    pGas[1] = 0.2;
                }
                else
                {
                    pGas[0] = 0.1;
                    pGas[1] = 0.28;
                    pGas[2] = 0.3;
                    pGas[3] = 0.35;
                }
            }
            else if (star.spectr == ESpectrType.B)
            {
                star.planetCount = num1 >= 0.3 ? num1 >= 0.75 ? 6 : 5 : 4;
                if (star.planetCount <= 3)
                {
                    pGas[0] = 0.2;
                    pGas[1] = 0.2;
                }
                else
                {
                    pGas[0] = 0.1;
                    pGas[1] = 0.22;
                    pGas[2] = 0.28;
                    pGas[3] = 0.35;
                    pGas[4] = 0.35;
                }
            }
            else if (star.spectr == ESpectrType.O)
            {
                star.planetCount = num1 >= 0.5 ? 6 : 5;
                pGas[0] = 0.1;
                pGas[1] = 0.2;
                pGas[2] = 0.25;
                pGas[3] = 0.3;
                pGas[4] = 0.32;
                pGas[5] = 0.35;
            }
            else
            {
                star.planetCount = 1;
            }

            star.planets = new PlanetData[star.planetCount];
            var num8 = 0;
            var num9 = 0;
            var orbitAround = 0;
            var num10 = 1;
            for (var index = 0; index < star.planetCount; ++index)
            {
                var info_seed = dotNet35Random2.Next();
                var gen_seed = dotNet35Random2.Next();
                var num11 = dotNet35Random2.NextDouble();
                var num12 = dotNet35Random2.NextDouble();
                var gasGiant = false;
                if (orbitAround == 0)
                {
                    ++num8;
                    if (index < star.planetCount - 1 && num11 < pGas[index])
                    {
                        gasGiant = true;
                        if (num10 < 3)
                            num10 = 3;
                    }

                    for (; star.index != 0 || num10 != 3; ++num10)
                    {
                        var num13 = star.planetCount - index;
                        var num14 = 9 - num10;
                        if (num14 > num13)
                        {
                            var a = num13 / (float)num14;
                            var num15 = num10 <= 3
                                ? Mathf.Lerp(a, 1f, 0.15f) + 0.01f
                                : Mathf.Lerp(a, 1f, 0.45f) + 0.01f;
                            if (dotNet35Random2.NextDouble() < num15)
                                goto label_62;
                        }
                        else
                        {
                            goto label_62;
                        }
                    }

                    gasGiant = true;
                }
                else
                {
                    ++num9;
                    gasGiant = false;
                }

                label_62:
                star.planets[index] = PlanetGen.CreatePlanet(galaxy, star, gameDesc.savedThemeIds, index, orbitAround,
                    orbitAround == 0 ? num10 : num9, orbitAround == 0 ? num8 : num9, gasGiant, info_seed, gen_seed);
                ++num10;
                if (gasGiant)
                {
                    orbitAround = num8;
                    num9 = 0;
                }

                if (num9 >= 1 && num12 < 0.8)
                {
                    orbitAround = 0;
                    num9 = 0;
                }
            }
        }

        var num16 = 0;
        var num17 = 0;
        var index1 = 0;
        for (var index2 = 0; index2 < star.planetCount; ++index2)
            if (star.planets[index2].type == EPlanetType.Gas)
            {
                num16 = star.planets[index2].orbitIndex;
                break;
            }

        for (var index3 = 0; index3 < star.planetCount; ++index3)
            if (star.planets[index3].orbitAround == 0)
                num17 = star.planets[index3].orbitIndex;
        if (num16 > 0)
        {
            var num18 = num16 - 1;
            var flag = true;
            for (var index4 = 0; index4 < star.planetCount; ++index4)
                if (star.planets[index4].orbitAround == 0 && star.planets[index4].orbitIndex == num16 - 1)
                {
                    flag = false;
                    break;
                }

            if (flag && num4 < 0.2 + num18 * 0.2)
                index1 = num18;
        }

        var index5 = num5 >= 0.2 ? num5 >= 0.4 ? num5 >= 0.8 ? 0 : num17 + 1 : num17 + 2 : num17 + 3;
        if (index5 != 0 && index5 < 5)
            index5 = 5;
        star.asterBelt1OrbitIndex = index1;
        star.asterBelt2OrbitIndex = index5;
        if (index1 > 0)
            star.asterBelt1Radius = orbitRadius[index1] * (float)num6 * star.orbitScaler;
        if (index5 > 0)
            star.asterBelt2Radius = orbitRadius[index5] * (float)num7 * star.orbitScaler;
        for (var index6 = 0; index6 < star.planetCount; ++index6)
        {
            var planet = star.planets[index6];
            SetHiveOrbitConditionFalse(planet.orbitIndex,
                planet.orbitAroundPlanet != null ? planet.orbitAroundPlanet.orbitIndex : 0,
                planet.sunDistance / star.orbitScaler, star.index);
        }

        star.hiveAstroOrbits = new AstroOrbitData[8];
        var hiveAstroOrbits = star.hiveAstroOrbits;
        var number = 0;
        for (var index7 = 0; index7 < hiveOrbitCondition.Length; ++index7)
            if (hiveOrbitCondition[index7])
                ++number;
        for (var index8 = 0; index8 < 8; ++index8)
        {
            var num19 = dotNet35Random3.NextDouble() * 2.0 - 1.0;
            var num20 = dotNet35Random3.NextDouble();
            var num21 = dotNet35Random3.NextDouble();
            var num22 = Math.Sign(num19) * Math.Pow(Math.Abs(num19), 0.7) * 90.0;
            var num23 = num20 * 360.0;
            var num24 = num21 * 360.0;
            var num25 = 0.3f;
            if (number > 0)
            {
                var num26 = star.index != 0 ? 5 : 2;
                var maxValue = (number > num26 ? num26 : number) * 100;
                var num27 = maxValue * 100;
                var num28 = dotNet35Random3.Next(maxValue);
                var num29 = num28 * num28 / num27;
                for (var index9 = 0; index9 < hiveOrbitCondition.Length; ++index9)
                    if (hiveOrbitCondition[index9])
                    {
                        if (num29 == 0)
                        {
                            num25 = hiveOrbitRadius[index9];
                            hiveOrbitCondition[index9] = false;
                            --number;
                            break;
                        }

                        --num29;
                    }
            }

            var num30 = num25 * star.orbitScaler;
            hiveAstroOrbits[index8] = new AstroOrbitData();
            hiveAstroOrbits[index8].orbitRadius = num30;
            hiveAstroOrbits[index8].orbitInclination = (float)num22;
            hiveAstroOrbits[index8].orbitLongitude = (float)num23;
            hiveAstroOrbits[index8].orbitPhase = (float)num24;
            hiveAstroOrbits[index8].orbitalPeriod = gameDesc.creationVersion.Build >= 20700
                ? Math.Sqrt(39.478417604357432 * num30 * num30 * num30 / (5.4154207962081527E-06 * star.mass))
                : Math.Sqrt(39.478417604357432 * num25 * num25 * num25 / (1.3538551990520382E-06 * star.mass));
            hiveAstroOrbits[index8].orbitRotation =
                Quaternion.AngleAxis(hiveAstroOrbits[index8].orbitLongitude, Vector3.up) *
                Quaternion.AngleAxis(hiveAstroOrbits[index8].orbitInclination, Vector3.forward);
            hiveAstroOrbits[index8].orbitNormal = Maths
                .QRotateLF(hiveAstroOrbits[index8].orbitRotation, new VectorLF3(0.0f, 1f, 0.0f)).normalized;
        }
    }

    public static void SetStarAge(StarData star, float age, double rn, double rt)
    {
        var num1 = (float)(rn * 0.1 + 0.95);
        var num2 = (float)(rt * 0.4 + 0.8);
        var num3 = (float)(rt * 9.0 + 1.0);
        star.age = age;
        if (age >= 1.0)
        {
            if (star.mass >= 18.0)
            {
                star.type = EStarType.BlackHole;
                star.spectr = ESpectrType.X;
                star.mass *= 2.5f * num2;
                star.radius *= 1f;
                star.acdiskRadius = star.radius * 5f;
                star.temperature = 0.0f;
                star.luminosity *= 1f / 1000f * num1;
                star.habitableRadius = 0.0f;
                star.lightBalanceRadius *= 0.4f * num1;
                star.color = 1f;
            }
            else if (star.mass >= 7.0)
            {
                star.type = EStarType.NeutronStar;
                star.spectr = ESpectrType.X;
                star.mass *= 0.2f * num1;
                star.radius *= 0.15f;
                star.acdiskRadius = star.radius * 9f;
                star.temperature = num3 * 1E+07f;
                star.luminosity *= 0.1f * num1;
                star.habitableRadius = 0.0f;
                star.lightBalanceRadius *= 3f * num1;
                star.orbitScaler *= 1.5f * num1;
                star.color = 1f;
            }
            else
            {
                star.type = EStarType.WhiteDwarf;
                star.spectr = ESpectrType.X;
                star.mass *= 0.2f * num1;
                star.radius *= 0.2f;
                star.acdiskRadius = 0.0f;
                star.temperature = num2 * 150000f;
                star.luminosity *= 0.04f * num2;
                star.habitableRadius *= 0.15f * num2;
                star.lightBalanceRadius *= 0.2f * num1;
                star.color = 0.7f;
            }
        }
        else
        {
            if (age < 0.95999997854232788)
                return;
            var num4 = (float)(Math.Pow(5.0, Math.Abs(Math.Log10(star.mass) - 0.7)) * 5.0);
            if (num4 > 10.0)
                num4 = (float)((Mathf.Log(num4 * 0.1f) + 1.0) * 10.0);
            var num5 = (float)(1.0 - Mathf.Pow(star.age, 30f) * 0.5);
            star.type = EStarType.GiantStar;
            star.mass = num5 * star.mass;
            star.radius = num4 * num2;
            star.acdiskRadius = 0.0f;
            star.temperature = num5 * star.temperature;
            star.luminosity = 1.6f * star.luminosity;
            star.habitableRadius = 9f * star.habitableRadius;
            star.lightBalanceRadius = 3f * star.habitableRadius;
            star.orbitScaler = 3.3f * star.orbitScaler;
        }
    }

    private static float RandNormal(
        float averageValue,
        float standardDeviation,
        double r1,
        double r2)
    {
        return averageValue + standardDeviation *
            (float)(Math.Sqrt(-2.0 * Math.Log(1.0 - r1)) * Math.Sin(2.0 * Math.PI * r2));
    }

    private static void SetHiveOrbitsConditionsTrue()
    {
        for (var index = 0; index < hiveOrbitCondition.Length; ++index)
            hiveOrbitCondition[index] = true;
    }

    private static void SetHiveOrbitConditionFalse(
        int planetOrbitIndex,
        int orbitAroundOrbitIndex,
        float sunDistance,
        int starIndex)
    {
        var index1 = orbitAroundOrbitIndex > 0 ? orbitAroundOrbitIndex : planetOrbitIndex;
        var num1 = orbitAroundOrbitIndex > 0 ? planetOrbitIndex : 0;
        if (index1 <= 0 || index1 >= planet2HiveOrbitTable.Length)
            return;
        var index2 = planet2HiveOrbitTable[index1];
        hiveOrbitCondition[index2] = false;
        if (num1 <= 0)
            return;
        var num2 = starIndex != 0
            ? (float)(0.048999998718500137 * num1 + 0.026000000536441803 + 0.12999999523162842)
            : (float)(0.041000001132488251 * num1 + 0.026000000536441803 + 0.11999999731779099);
        var index3 = index2 - 1;
        var index4 = index2 + 1;
        if (index3 >= 0 && sunDistance - (double)hiveOrbitRadius[index3] < num2)
            hiveOrbitCondition[index3] = false;
        if (index4 >= hiveOrbitCondition.Length || hiveOrbitRadius[index4] - (double)sunDistance >= num2)
            return;
        hiveOrbitCondition[index4] = false;
    }
}