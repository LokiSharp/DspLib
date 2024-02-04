using System.ComponentModel.DataAnnotations.Schema;

namespace DspLib.DataBase;

public class SeedInfo
{
    [Column(TypeName = "INT(4) UNSIGNED")] public int SeedInfoId { get; set; }
    public List<SeedGalaxiesInfo> SeedGalaxiesInfos { get; set; }
    public SeedPlanetsTypeCountInfo SeedPlanetsTypeCountInfo { get; set; }
    public SeedStarsTypeCountInfo SeedStarsTypeCountInfo { get; set; }
    [Column(TypeName = "INT(4)")] public int 种子号 { get; set; }

    public int 巨星数 { get; set; }
    public int 最多卫星 { get; set; }
    public int 最多潮汐星 { get; set; }
    public int 潮汐星球数 { get; set; }
    public int 最多潮汐永昼永夜 { get; set; }
    public int 潮汐永昼永夜数 { get; set; }
    public int 熔岩星球数 { get; set; }
    public int 海洋星球数 { get; set; }
    public int 沙漠星球数 { get; set; }
    public int 冰冻星球数 { get; set; }
    public int 气态星球数 { get; set; }
    public int 总星球数量 { get; set; }
    public float 最高亮度 { get; set; }
    public float 星球总亮度 { get; set; }
}