using DspLib.DataBase;
using Microsoft.Extensions.Configuration;

namespace Tests;

[TestClass]
public class DataBaseTests
{
    private static IConfigurationRoot configuration { get; set; }
    private static DatabaseSecrets databaseSecrets { get; set; }

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();
        configuration = builder.Build();
        databaseSecrets = new DatabaseSecrets();
        configuration.GetSection("Database").Bind(databaseSecrets);
    }

    [TestMethod]
    public void TestReadUserSecrets()
    {
        var dspDbContext = new DspDbContext(databaseSecrets);
        Console.WriteLine(dspDbContext.Database.ProviderName);
    }
}