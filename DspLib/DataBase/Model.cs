using DspLib.Enum;
using Microsoft.EntityFrameworkCore;

namespace DspLib.DataBase;

public class DspDbContext : DbContext
{
    private readonly DatabaseSecrets _databaseSecrets;

    public DspDbContext(DatabaseSecrets databaseSecrets)
    {
        _databaseSecrets = databaseSecrets;
    }

    public DbSet<GalaxiesInfo> GalaxiesInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString =
            $"Host={_databaseSecrets.Host};" +
            $"Database={_databaseSecrets.DBname};" +
            $"Username={_databaseSecrets.User};" +
            $"Password={_databaseSecrets.Password};" +
            $"Port={_databaseSecrets.Port}";
        optionsBuilder.UseNpgsql(connectionString);
    }
}

public class GalaxiesInfo
{
    public int GalaxiesInfoId { get; set; }
    public int 种子号码 { get; set; }
    public ESpectrType 恒星类型 { get; set; }
    public float 恒星光度 { get; set; }
    public float 星系距离 { get; set; }
    public bool 环盖首星 { get; set; }
    public int 星系坐标X { get; set; }
    public int 星系坐标Y { get; set; }
    public int 星系坐标Z { get; set; }
    public int 潮汐星数 { get; set; }
    public int 最多卫星 { get; set; }
    public int 星球数量 { get; set; }
    public EPlanetType[] 星球类型 { get; set; }
    public bool 是否有水 { get; set; }
    public bool 有硫酸否 { get; set; }
    public int 铁矿脉 { get; set; }
    public int 铜矿脉 { get; set; }
    public int 硅矿脉 { get; set; }
    public int 钛矿脉 { get; set; }
    public int 石矿脉 { get; set; }
    public int 煤矿脉 { get; set; }
    public int 原油涌泉 { get; set; }
    public int 可燃冰矿 { get; set; }
    public int 金伯利矿 { get; set; }
    public int 分形硅矿 { get; set; }
    public int 有机晶体矿 { get; set; }
    public int 光栅石矿 { get; set; }
    public int 刺笋矿脉 { get; set; }
    public int 单极磁矿 { get; set; }
}