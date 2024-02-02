using System.ComponentModel.DataAnnotations.Schema;

namespace DspLib.DataBase;

public class SeedStarsTypeCountInfo
{
    public int SeedStarsTypeCountInfoId { get; set; }

    [Column(TypeName = "INT(4)")] public int 种子号 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int M型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int K型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int G型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int F型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int A型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int B型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int O型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int X型恒星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int M型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int K型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int G型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int F型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int A型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int B型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int O型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int X型巨星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 白矮星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 中子星 { get; set; }

    [Column(TypeName = "TINYINT(1) UNSIGNED")]
    public int 黑洞 { get; set; }
}