using DspLib.Gen;
using Tests.Utils;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Gen;

public class GameDescTests
{
    public GameDescTests(ITestOutputHelper testOutputHelper)
    {
        Console.SetOut(new TestOutputHelperWriter(testOutputHelper));
    }

    [Fact]
    public void GameDescTest()
    {
        var gameDesc = new GameDesc();

        gameDesc.SetForNewGame(0, 64);

        Assert.Equal(0, gameDesc.galaxySeed);
        Assert.Equal(64, gameDesc.starCount);
        Assert.NotNull(gameDesc.combatSettings);
        Assert.NotNull(gameDesc.savedThemeIds);
        Assert.NotEmpty(gameDesc.savedThemeIds);
    }
}