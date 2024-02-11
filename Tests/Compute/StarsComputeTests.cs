using DspLib;
using DspLib.Dyson;
using DspLib.Gen;
using Xunit;
using Xunit.Abstractions;

namespace Tests.Compute;

public class StarsComputeTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void TestStarsCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDt = DateTime.Now;
        var mSeed = 0;
        var mStarCount = 64;
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(mSeed, mStarCount);
        StarsCompute.Compute(gameDesc, out _);
        var afterDt = DateTime.Now;
        var ts = afterDt.Subtract(beforeDt);
        testOutputHelper.WriteLine("花费{0}ms", ts.TotalMilliseconds);
    }

    [Fact]
    public void TestMultiplyStarsCompute()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDt = DateTime.Now;
        const int seedCount = 10;
        const int mStarCount = 64;
        var galaxyDatas = new Dictionary<string, GalaxyData>();
        for (var mSeed = 0; mSeed < seedCount; mSeed++)
        {
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(mSeed, mStarCount);
            StarsCompute.Compute(gameDesc, out var galaxyData);
            galaxyDatas[$"{mSeed}-{mStarCount}"] = galaxyData;
            testOutputHelper.WriteLine("计算完成：{0}:{1}", mSeed, mStarCount);
        }

        var afterDt = DateTime.Now;
        var ts = afterDt.Subtract(beforeDt);
        testOutputHelper.WriteLine("花费{0}ms", ts.TotalMilliseconds);
        testOutputHelper.WriteLine("平均花费{0}ms", ts.TotalMilliseconds / seedCount);
    }

    [Fact]
    public void TestMultiplyStarsComputeWithoutPlanetData()
    {
        PlanetModelingManager.Start();
        RandomTable.Init();
        var beforeDt = DateTime.Now;
        const int seedCount = 100;
        const int mStarCount = 64;
        var galaxyDatas = new Dictionary<string, GalaxyData>();
        for (var mSeed = 0; mSeed < seedCount; mSeed++)
        {
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(mSeed, mStarCount);
            StarsCompute.ComputeWithoutPlanetData(gameDesc, out var galaxyData);
            galaxyDatas[$"{mSeed}-{mStarCount}"] = galaxyData;
            testOutputHelper.WriteLine("计算完成：{0}:{1}", mSeed, mStarCount);
        }

        var afterDt = DateTime.Now;
        var ts = afterDt.Subtract(beforeDt);
        testOutputHelper.WriteLine("花费{0}ms", ts.TotalMilliseconds);
        testOutputHelper.WriteLine("平均花费{0}ms", ts.TotalMilliseconds / seedCount);
    }
}