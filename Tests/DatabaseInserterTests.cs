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
        var builder = new ConfigurationBuilder()
            .AddEnvironmentVariables();
        configuration = builder.Build();
        databaseSecrets = new DatabaseSecrets();
        configuration.GetSection("Database").Bind(databaseSecrets);
    }

    private static IConfigurationRoot configuration { get; set; }
    private static DatabaseSecrets databaseSecrets { get; set; }

    [Fact]
    public void TestInsertGalaxiesInfo()
    {
        var startSeed = 0;
        var maxSeed = 99999999;
        _stopwatch.Start();
        for (var seed = startSeed; seed < maxSeed; seed++)
        {
            if (seed % 100 == 0) DrawProgressBar(seed, maxSeed);
            if (new DspDbContext(databaseSecrets).SeedInfo.Any(seedInfo => seedInfo.种子号 == seed)) continue;
            DatabaseInserter.InsertGalaxiesInfo(databaseSecrets, seed, 64);
        }

        _stopwatch.Stop();
    }

    private void DrawProgressBar(int progress, int total)
    {
        const int barSize = 50;
        var percent = (float)progress / total;

        var charsToDraw = (int)(percent * barSize);

        var progressBar = new StringBuilder("[");
        progressBar.Append('#', charsToDraw);
        progressBar.Append(' ', barSize - charsToDraw);
        progressBar.Append(']');

        progressBar.Append($" {percent * 100:F2}%");
        progressBar.Append($" {progress}/{total}");
        if (percent > 0)
        {
            var elapsed = _stopwatch.Elapsed;
            var estimatedTotalTime = TimeSpan.FromMilliseconds(elapsed.TotalMilliseconds / percent);
            progressBar.Append($" 单个花费时间: {elapsed.TotalMilliseconds / progress}");
            progressBar.Append($" 总需要时长: {estimatedTotalTime - elapsed}");
        }

        _testOutputHelper.WriteLine(progressBar.ToString());
    }
}