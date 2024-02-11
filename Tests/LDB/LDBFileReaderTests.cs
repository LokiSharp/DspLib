using DspLib;
using Xunit;

namespace Tests.LDB;

public class LDBFileReaderTests
{
    [Fact]
    public void TestReadItemProtoSet()
    {
        const string filePath = "Prototypes\\ItemProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [Fact]
    public void TestReadThemeProtoSet()
    {
        const string filePath = "Prototypes\\ThemeProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [Fact]
    public void TestReadVeinProtoSet()
    {
        const string filePath = "Prototypes\\VeinProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }

    [Fact]
    public void TestReadRecipeProtoSet()
    {
        const string filePath = "Prototypes\\RecipeProtoSet.dat";
        var reader = new LDBFileReader();
        reader.ReadFile(filePath);
    }
}