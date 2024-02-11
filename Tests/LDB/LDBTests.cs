using Xunit;
using Xunit.Abstractions;

namespace Tests.LDB;

public class LDBTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TestReadItemTest()
    {
        var result = DspLib.LDB.items;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadTheme()
    {
        var result = DspLib.LDB.themes;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadVein()
    {
        var result = DspLib.LDB.veins;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadRecipe()
    {
        var result = DspLib.LDB.recipes;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }
}