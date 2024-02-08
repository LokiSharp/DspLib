using System.Diagnostics;
using System.Text;
using DspLib.DataBase;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DatabaseDapperInserterTests
{
    private readonly Stopwatch _stopwatch;
    private readonly ITestOutputHelper _testOutputHelper;

    public DatabaseDapperInserterTests(ITestOutputHelper testOutputHelper)
    {
        _stopwatch = new Stopwatch();
        _testOutputHelper = testOutputHelper;
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    private static DatabaseSecrets databaseSecrets { get; set; }

    [Fact]
    public async void TestInsertGalaxiesInfo()
    {
        var startSeed = 0;
        var maxSeed = 99999999;
        await new DatabaseDapperInserter().InsertGalaxiesInfoInBatch(startSeed, maxSeed, 64);
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