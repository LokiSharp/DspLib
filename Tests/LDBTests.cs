using System.Diagnostics;
using DspLib;
using Xunit;

namespace Tests;

public class LDBTests
{
    [Fact]
    public void TestReadItemTest()
    {
        var result = LDB.items;
        Debug.Write(result.dataArray.Length);
    }

    [Fact]
    public void TestReadTheme()
    {
        var result = LDB.themes;
        Debug.Write(result.dataArray.Length);
    }

    [Fact]
    public void TestReadVein()
    {
        var result = LDB.veins;
        Debug.Write(result.dataArray.Length);
    }

    [Fact]
    public void TestReadRecipe()
    {
        var result = LDB.recipes;
        Debug.Write(result.dataArray.Length);
    }
}