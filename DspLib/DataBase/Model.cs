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
            $"Server={_databaseSecrets.Server};" +
            $"Database={_databaseSecrets.Database};" +
            $"User={_databaseSecrets.User};" +
            $"Password={_databaseSecrets.Password};" +
            $"Port={_databaseSecrets.Port}";
        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 11, 4)));
    }
}