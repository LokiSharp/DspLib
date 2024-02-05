using System.ComponentModel.DataAnnotations.Schema;
using DspLib.Enum;

namespace DspLib.DataBase;

public class SeedGalaxyInfo
{
    public int SeedGalaxyInfoId { get; set; }
    [Column(TypeName = "INT(4) UNSIGNED")] public int SeedInfoId { get; set; }
    public SeedInfo SeedInfo { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public EStarType 恒星类型 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public ESpectrType? 光谱类型 { get; set; }

    public float 恒星光度 { get; set; }
    public float 星系距离 { get; set; }
    public bool 环盖首星 { get; set; }
    public float 星系坐标x { get; set; }
    public float 星系坐标y { get; set; }
    public float 星系坐标z { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 潮汐星数 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 最多卫星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 星球数量 { get; set; }

    public EPlanetType[] 星球类型 { get; set; } = new EPlanetType[6];
    public bool 是否有水 { get; set; }
    public bool 有硫酸否 { get; set; }
}