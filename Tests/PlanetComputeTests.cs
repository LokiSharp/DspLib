using DspLib;
using DspLib.Dyson;
using DspLib.Galaxy;
using Xunit;
using Xunit.Abstractions;

namespace Tests;

public class PlanetComputeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TestPlanetCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDt = DateTime.Now;
        var mSeed = 0;
        var mStarCount = 64;
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(mSeed, mStarCount);
        StarsCompute.ComputeWithoutPlanetData(gameDesc, out var galaxyData);
        StarsCompute.PlanetCompute(galaxyData, galaxyData.stars[0], galaxyData.stars[0].planets[1]);
        var afterDt = DateTime.Now;
        var ts = afterDt.Subtract(beforeDt);
        testOutputHelper.WriteLine("花费{0}ms", ts.TotalMilliseconds);
    }
}