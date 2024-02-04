﻿using DspLib.DataBase;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DatabaseInserterTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public DatabaseInserterTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
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
        var maxSeed = 10000;
        for (var seed = startSeed; seed < maxSeed; seed++)
        {
            using var context = new DspDbContext(databaseSecrets);
            if (context.SeedInfo.Any(seedInfo => seedInfo.种子号 == seed)) continue;
            DatabaseInserter.InsertGalaxiesInfo(databaseSecrets, seed, 64);
            testOutputHelper.WriteLine($"完成并提交：{seed}");
        }
    }
}