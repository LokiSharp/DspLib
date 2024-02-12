using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Gen;

public class StarData
{
    public const double kEnterDistance = 3600000.0;
    public const float kPhysicsRadiusRatio = 1200f;
    public const float kViewRadiusRatio = 800f;
    public const int kMaxDFHiveOrbit = 8;
    public float acdiskRadius; // 吸积盘半径
    public float age; // 年龄
    public float asterBelt1OrbitIndex;
    public float asterBelt1Radius;
    public float asterBelt2OrbitIndex;
    public float asterBelt2Radius;
    public float classFactor; // 光谱分类因子
    public float color; // 颜色
    public float dysonRadius = 10f; // 戴森球半径
    public GalaxyData galaxy; // 所在星系对象
    public float habitableRadius = 1f; // 可居住半径
    public AstroOrbitData[] hiveAstroOrbits;
    public int hivePatternLevel;
    public int id; // 标识编号
    public int index; // 索引编号
    public int initialHiveCount;
    public float level; // 天体级别
    public float lifetime = 50f; // 生命周期
    public float lightBalanceRadius = 1f; // 光照平衡半径
    public float luminosity = 1f;
    public float mass = 1f; // 质量
    public int maxHiveCount;
    public string name = "";
    public float orbitScaler = 1f; // 轨道缩放值
    public string overrideName = "";
    public int planetCount; // 所属行星数量
    public PlanetData[] planets; // 所属行星对象
    public VectorLF3 position = VectorLF3.zero; // 天体位置
    public float radius = 1f; // 半径
    public float resourceCoef = 1f; // 资源系数
    public float safetyFactor = 1f;
    public int seed; // 种子
    public ESpectrType spectr; // 光谱类型
    public float temperature = 8500f; // 温度
    public EStarType type; // 天体类型
    public VectorLF3 uPosition; // 天体实际位置

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