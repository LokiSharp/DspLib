using DspLib.DataBase;
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
        var numOfTables = 10;
        var startSeed = 0;
        var maxSeed = 99999999;
        await new DatabaseInserter().InsertGalaxiesInfoInBatch(numOfTables, startSeed, maxSeed, 64);
    }
}