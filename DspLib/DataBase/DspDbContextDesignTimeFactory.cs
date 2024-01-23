using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DspLib.DataBase;

public class DspDbContextDesignTimeFactory : IDesignTimeDbContextFactory<DspDbContext>
{
    public DspDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();
        var databaseSecrets = new DatabaseSecrets();
        configuration.GetSection("Database").Bind(databaseSecrets);

        var connectionString =
            $"Server={databaseSecrets.Server};" +
            $"Database={databaseSecrets.Database};" +
            $"User={databaseSecrets.User};" +
            $"Password={databaseSecrets.Password};" +
            $"Port={databaseSecrets.Port}";

        var builder = new DbContextOptionsBuilder<DspDbContext>();
        builder.UseMySql(connectionString, new MariaDbServerVersion(new Version(10, 11, 4)));
        ;

        return new DspDbContext(databaseSecrets);
    }
}