using DspLib;

namespace Tests;

[TestClass]
public class LDBFileReaderTests
{
    [TestMethod]
    public void TestReadItemProtoSet()
    {
        const string filePath = "Prototypes\\ItemProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [TestMethod]
    public void TestReadThemeProtoSet()
    {
        const string filePath = "Prototypes\\ThemeProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [TestMethod]
    public void TestReadVeinProtoSet()
    {
        const string filePath = "Prototypes\\VeinProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [TestMethod]
    public void TestReadRecipeProtoSet()
    {
        const string filePath = "Prototypes\\RecipeProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }
}