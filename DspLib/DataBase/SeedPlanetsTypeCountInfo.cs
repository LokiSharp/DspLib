using System.ComponentModel.DataAnnotations.Schema;

namespace DspLib.DataBase;

public class SeedPlanetsTypeCountInfo
{
    public int SeedPlanetsTypeCountInfoId { get; set; }

    [Column(TypeName = "INT(4)")] public int 种子号 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 地中海 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 气态巨星1 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 气态巨星2 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 冰巨星1 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 冰巨星2 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 干旱荒漠 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 灰烬冻土 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 海洋丛林 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 熔岩 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 冰原冻土 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 贫瘠荒漠 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 戈壁 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 火山灰 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 红石 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 草原 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 水世界 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 黑石盐滩 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 樱林海 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 飓风石林 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 猩红冰湖 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 气态巨星3 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 热带草原 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 橙晶荒漠 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 极寒冻土 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 潘多拉沼泽 { get; set; }
}