using DspLib.Enum;

namespace DspLib.DataBase.Model;

public class SeedGalaxyInfo
{
    public long SeedGalaxyInfoId { get; set; }
    public long SeedInfoId { get; set; }
    public SeedInfo? SeedInfo { get; set; }
    public EStarType 恒星类型 { get; set; }
    public ESpectrType? 光谱类型 { get; set; }
    public float 恒星光度 { get; set; }
    public float 星系距离 { get; set; }
    public bool 环盖首星 { get; set; }
    public float 星系坐标x { get; set; }
    public float 星系坐标y { get; set; }
    public float 星系坐标z { get; set; }
    public int 潮汐星数 { get; set; }
    public int 最多卫星 { get; set; }
    public int 星球数量 { get; set; }
    public string? 星球类型String { get; set; }

    public EPlanetType[] 星球类型
    {
        get
        {
            if (星球类型String != null)
                return 星球类型String.Trim('[', ']')
                    .Split(',')
                    .Select(x => (EPlanetType)int.Parse(x))
                    .ToArray();
            return [];
        }
        set { 星球类型String = "[" + string.Join(",", value.Select(x => (int)x)) + "]"; }
    }

    public bool 是否有水 { get; set; }
    public bool 有硫酸否 { get; set; }
}