using DspLib;
using DspLib.Galaxy;
using DysonSphereProgramSeed.Dyson;

namespace Tests;

[TestClass]
public class PlanetComputeTests
{
    [TestMethod]
    public void TestPlanetCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDT = DateTime.Now;
        var mSeed = 0;
        var mStarCount = 64;
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(mSeed, mStarCount);
        StarsCompute.ComputeWithoutPlanetData(gameDesc, out var galaxyData);
        StarsCompute.PlanetCompute(galaxyData, galaxyData.stars[0], galaxyData.stars[0].planets[1]);
        var afterDT = DateTime.Now;
        var ts = afterDT.Subtract(beforeDT);
        Console.WriteLine("花费{0}ms", ts.TotalMilliseconds);
    }
    
}