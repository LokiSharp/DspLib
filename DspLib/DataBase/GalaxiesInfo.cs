using System.ComponentModel.DataAnnotations.Schema;
using DspLib.Enum;

namespace DspLib.DataBase;

public class GalaxiesInfo
{
    public int GalaxiesInfoId { get; set; }

    [Column(TypeName = "INT(4)")] public int 种子号码 { get; set; }

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

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 铁矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 铜矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 硅矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 钛矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 石矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 煤矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 原油涌泉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 可燃冰矿 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 金伯利矿 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 分形硅矿 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 有机晶体矿 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 光栅石矿 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 刺笋矿脉 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 单极磁矿 { get; set; }
}