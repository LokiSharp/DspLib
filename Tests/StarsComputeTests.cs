using DspLib;
using DspLib.Galaxy;
using DysonSphereProgramSeed.Dyson;

namespace Tests;

[TestClass]
public class StarsComputeTests
{
    [TestMethod]
    public void TestStarsCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDT = DateTime.Now;
        var mSeed = 0;
        var mStarCount = 64;
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(mSeed, mStarCount);
        StarsCompute.Compute(gameDesc, out var galaxyData);
        var afterDT = DateTime.Now;
        var ts = afterDT.Subtract(beforeDT);
        Console.WriteLine("花费{0}ms", ts.TotalMilliseconds);
    }

    [TestMethod]
    public void TestMultiplyStarsCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDT = DateTime.Now;
        const int seedCount = 10;
        const int mStarCount = 64;
        var galaxyDatas = new Dictionary<string, GalaxyData>();
        for (var mSeed = 0; mSeed < seedCount; mSeed++)
        {
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(mSeed, mStarCount);
            StarsCompute.Compute(gameDesc, out var galaxyData);
            galaxyDatas[$"{mSeed}-{mStarCount}"] = galaxyData;
            Console.WriteLine("计算完成：{0}:{1}", mSeed, mStarCount);
        }

        var afterDT = DateTime.Now;
        var ts = afterDT.Subtract(beforeDT);
        Console.WriteLine("花费{0}ms", ts.TotalMilliseconds);
        Console.WriteLine("平均花费{0}ms", ts.TotalMilliseconds / seedCount);
    }

    [TestMethod]
    public void Test100StarsComputeWithoutPlanetData()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDT = DateTime.Now;
        const int seedCount = 100;
        const int mStarCount = 64;
        var galaxyDatas = new Dictionary<string, GalaxyData>();
        for (var mSeed = 0; mSeed < seedCount; mSeed++)
        {
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(mSeed, mStarCount);
            StarsCompute.ComputeWithoutPlanetData(gameDesc, out var galaxyData);
            galaxyDatas[$"{mSeed}-{mStarCount}"] = galaxyData;
            Console.WriteLine("计算完成：{0}:{1}", mSeed, mStarCount);
        }

        var afterDT = DateTime.Now;
        var ts = afterDT.Subtract(beforeDT);
        Console.WriteLine("花费{0}ms", ts.TotalMilliseconds);
        Console.WriteLine("平均花费{0}ms", ts.TotalMilliseconds / seedCount);
    }
}