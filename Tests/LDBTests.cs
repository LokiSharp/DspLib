using DspLib;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class LDBTests(ITestOutputHelper testOutputHelper)
{

    [Fact]
    public void TestReadItemTest()
    {
        var result = LDB.items;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadTheme()
    {
        var result = LDB.themes;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadVein()
    {
        var result = LDB.veins;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }

    [Fact]
    public void TestReadRecipe()
    {
        var result = LDB.recipes;
        testOutputHelper.WriteLine(result.dataArray.Length.ToString());
    }
}