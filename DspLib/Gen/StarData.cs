using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Gen;

public class StarData
{
    public const double kEnterDistance = 3600000.0;
    public const float kPhysicsRadiusRatio = 1200f;
    public const float kViewRadiusRatio = 800f;
    public const int kMaxDFHiveOrbit = 8;
    public float acdiskRadius;
    public float age;
    public float asterBelt1OrbitIndex;
    public float asterBelt1Radius;
    public float asterBelt2OrbitIndex;
    public float asterBelt2Radius;
    public float classFactor;
    public float color;
    public float dysonRadius = 10f;
    public GalaxyData galaxy;
    public float habitableRadius = 1f;
    public AstroOrbitData[] hiveAstroOrbits;
    public int hivePatternLevel;
    public int id;
    public int index;
    public int initialHiveCount;
    public float level;
    public float lifetime = 50f;
    public float lightBalanceRadius = 1f;
    public float luminosity = 1f;
    public float mass = 1f;
    public int maxHiveCount;
    public string name = "";
    public float orbitScaler = 1f;
    public string overrideName = "";
    public int planetCount;
    public PlanetData[] planets;
    public VectorLF3 position = VectorLF3.zero;
    public float radius = 1f;
    public float resourceCoef = 1f;
    public float safetyFactor = 1f;
    public int seed;
    public ESpectrType spectr;
    public float temperature = 8500f;
    public EStarType type;
    public VectorLF3 uPosition;

    public string displayName => !string.IsNullOrEmpty(overrideName) ? overrideName : name;

    public float expSharingFactor => (float)((1.0 - safetyFactor) * (1.0 - safetyFactor) * 3.5 + 0.5);

    public int astroId => id * 100;

    public float dysonLumino => Mathf.Round((float)Math.Pow(luminosity, 0.33000001311302185) * 1000f) / 1000f;

    public float systemRadius
    {
        get
        {
            var systemRadius = dysonRadius;
            if (planetCount > 0)
                systemRadius = planets[planetCount - 1].sunDistance;
            return systemRadius;
        }
    }

    public float physicsRadius => radius * 1200f;

    public float viewRadius => radius * 800f;

    public string typeString
    {
        get
        {
            var typeString = "";
            if (type == EStarType.GiantStar)
                typeString = spectr > ESpectrType.K
                    ? spectr > ESpectrType.F
                        ? spectr != ESpectrType.A ? typeString + "蓝巨星" : typeString + "白巨星"
                        : typeString + "黄巨星"
                    : typeString + "红巨星";
            else if (type == EStarType.WhiteDwarf)
                typeString += "白矮星";
            else if (type == EStarType.NeutronStar)
                typeString += "中子星";
            else if (type == EStarType.BlackHole)
                typeString += "黑洞";
            else if (type == EStarType.MainSeqStar)
                typeString = typeString + spectr + "型恒星";
            return typeString;
        }
    }

    public bool epicHive => type == EStarType.NeutronStar || type == EStarType.BlackHole;

    public string OrbitsDescString()
    {
        var str = "";
        for (var index1 = 1; index1 <= 12; ++index1)
        {
            var num = 0;
            for (var index2 = 0; index2 < planetCount; ++index2)
                if (planets[index2].orbitAround == 0 && planets[index2].orbitIndex == index1)
                {
                    num = planets[index2].number;
                    break;
                }

            str = asterBelt1OrbitIndex != (double)index1
                ? asterBelt2OrbitIndex != (double)index1 ? str + num : str + "b"
                : str + "a";
        }

        return str;
    }

    public override string ToString()
    {
        return "Star " + displayName;
    }
}