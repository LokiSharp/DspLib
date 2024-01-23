﻿using DspLib.DataBase;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DataBaseTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public DataBaseTests(ITestOutputHelper testOutputHelper)
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
    public void TestReadUserSecrets()
    {
        var dspDbContext = new DspDbContext(databaseSecrets);
        testOutputHelper.WriteLine(dspDbContext.Database.ProviderName);
    }
}