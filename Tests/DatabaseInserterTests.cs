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
        var maxSeed = 10000;
        for (var seed = 0; seed < maxSeed; seed++)
        {
            DatabaseInserter.InsertGalaxiesInfo(databaseSecrets, seed, 64);
            testOutputHelper.WriteLine($"完成并提交：{seed}");
        }
    }
}