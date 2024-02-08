using DspLib.DataBase;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DatabaseInserterTests
{
    public DatabaseInserterTests(ITestOutputHelper testOutputHelper)
    {
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    [Fact]
    public async void TestInsertGalaxiesInfo()
    {
        var startSeed = 0;
        var maxSeed = 99999999;
        await new DatabaseInserter().InsertGalaxiesInfoInBatch(startSeed, maxSeed, 64);
    }
}