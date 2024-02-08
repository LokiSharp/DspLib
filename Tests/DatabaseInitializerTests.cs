using DspLib.DataBase;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class DatabaseInitializerTests
{
    public DatabaseInitializerTests(ITestOutputHelper testOutputHelper)
    {
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    [Fact]
    public void TestDatabaseInitializer()
    {
        new DatabaseInitializer().CreateTable(10);
    }
}