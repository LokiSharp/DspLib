using System.Diagnostics;
using DspLib;

namespace Tests;

[TestClass]
public class LDBTests
{
    [TestMethod]
    public void TestReadItem()
    {
        var result = LDB.items;
        Debug.Write(result.dataArray.Length);
    }

    [TestMethod]
    public void TestReadTheme()
    {
        var result = LDB.themes;
        Debug.Write(result.dataArray.Length);
    }

    [TestMethod]
    public void TestReadVein()
    {
        var result = LDB.veins;
        Debug.Write(result.dataArray.Length);
    }

    [TestMethod]
    public void TestReadRecipe()
    {
        var result = LDB.recipes;
        Debug.Write(result.dataArray.Length);
    }
}