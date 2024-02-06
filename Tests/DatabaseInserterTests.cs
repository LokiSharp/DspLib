using System.Diagnostics;
using System.Text;
using DspLib.DataBase;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DatabaseInserterTests
{
    private readonly Stopwatch _stopwatch;
    private readonly ITestOutputHelper _testOutputHelper;

    public DatabaseInserterTests(ITestOutputHelper testOutputHelper)
    {
        _stopwatch = new Stopwatch();
        _testOutputHelper = testOutputHelper;
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();
        configuration = builder.Build();
        databaseSecrets = new DatabaseSecrets();
        configuration.GetSection("Database").Bind(databaseSecrets);
    }

    private static IConfigurationRoot configuration { get; set; }
    private static DatabaseSecrets databaseSecrets { get; set; }

    [Fact]
    public async void TestInsertGalaxiesInfo()
    {
        var startSeed = 0;
        var maxSeed = 99999999;
        await new DatabaseInserter(databaseSecrets).InsertGalaxiesInfoInBatch(startSeed, maxSeed, 64);
    }

    private class TestOutputHelperWriter(ITestOutputHelper output) : TextWriter
    {
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string message)
        {
            output.WriteLine(message);
        }

        public override void Write(string message)
        {
            output.WriteLine(message);
        }
    }
}