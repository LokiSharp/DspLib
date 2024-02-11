using DspLib.DataBase;
using Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Database;

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