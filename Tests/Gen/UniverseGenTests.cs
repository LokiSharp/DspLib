using DspLib.Gen;
using Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Gen;

public class UniverseGenTests
{
    public UniverseGenTests(ITestOutputHelper testOutputHelper)
    {
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    [Fact]
    public void CreateGalaxyTest()
    {
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(0, 64);

        var resultGalaxy = UniverseGen.CreateGalaxy(gameDesc);

        Assert.NotNull(resultGalaxy);
        Assert.Equal(0, resultGalaxy.seed);
        Assert.Equal(64, resultGalaxy.starCount);
        Assert.Equal(64, resultGalaxy.stars.Length);
    }

    [Fact]
    public void CreateMultiplyGalaxyTest()
    {
        for (var i = 0; i < 1000; i++)
        {
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(i, 64);
            var resultGalaxy = UniverseGen.CreateGalaxy(gameDesc);

            Assert.NotNull(resultGalaxy);
            Assert.Equal(i, resultGalaxy.seed);
            Assert.Equal(64, resultGalaxy.starCount);
            Assert.Equal(64, resultGalaxy.stars.Length);
        }
    }
}