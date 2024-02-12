﻿using DspLib.DataBase;
using Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Database;

public class DatabaseInserterTests
{
    public DatabaseInserterTests(ITestOutputHelper testOutputHelper)
    {
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    [Fact]
    public async void TestInsertGalaxiesInfoInBatch()
    {
        var startSeed = 0;
        var maxSeed = 99999999;
        await new DatabaseInserter(new ConnectionString().GetString())
            .InsertGalaxiesInfoInBatch(startSeed, maxSeed, 64);
    }
}