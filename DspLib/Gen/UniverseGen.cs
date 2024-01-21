using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Galaxy;

public static class UniverseGen
{
    public static int algoVersion = 20200403;
    private static List<VectorLF3> tmp_poses;
    private static List<VectorLF3> tmp_drunk;
    private static int[] tmp_state = null;

    public static GalaxyData CreateGalaxy(GameDesc gameDesc)
    {
        var galaxySeed = gameDesc.galaxySeed;
        var starCount = gameDesc.starCount;
        PlanetGen.gasCoef = gameDesc.isRareResource ? 0.8f : 1f;
        var dotNet35Random = new DotNet35Random(galaxySeed);
        var tempPoses = GenerateTempPoses(dotNet35Random.Next(), starCount, 4, 2.0, 2.3, 3.5, 0.18);
        var galaxy = new GalaxyData();
        galaxy.seed = galaxySeed;
        galaxy.starCount = tempPoses;
        galaxy.stars = new StarData[tempPoses];
        if (tempPoses <= 0)
            return galaxy;
        var num1 = (float)dotNet35Random.NextDouble();
        var num2 = (float)dotNet35Random.NextDouble();
        var num3 = (float)dotNet35Random.NextDouble();
        var num4 = (float)dotNet35Random.NextDouble();
        var num5 = Mathf.CeilToInt((float)(0.0099999997764825821 * tempPoses + num1 * 0.30000001192092896));
        var num6 = Mathf.CeilToInt((float)(0.0099999997764825821 * tempPoses + num2 * 0.30000001192092896));
        var num7 = Mathf.CeilToInt((float)(0.016000000759959221 * tempPoses + num3 * 0.40000000596046448));
        var num8 = Mathf.CeilToInt((float)(0.013000000268220901 * tempPoses + num4 * 1.3999999761581421));
        var num9 = tempPoses - num5;
        var num10 = num9 - num6;
        var num11 = num10 - num7;
        var num12 = (num11 - 1) / num8;
        var num13 = num12 / 2;
        for (var index = 0; index < tempPoses; ++index)
        {
            var seed = dotNet35Random.Next();
            if (index == 0)
            {
                galaxy.stars[index] = StarGen.CreateBirthStar(galaxy, gameDesc, seed);
            }
            else
            {
                var needSpectr = ESpectrType.X;
                if (index == 3)
                    needSpectr = ESpectrType.M;
                else if (index == num11 - 1)
                    needSpectr = ESpectrType.O;
                var needtype = EStarType.MainSeqStar;
                if (index % num12 == num13)
                    needtype = EStarType.GiantStar;
                if (index >= num9)
                    needtype = EStarType.BlackHole;
                else if (index >= num10)
                    needtype = EStarType.NeutronStar;
                else if (index >= num11)
                    needtype = EStarType.WhiteDwarf;
                galaxy.stars[index] = StarGen.CreateStar(galaxy, tmp_poses[index], gameDesc, index + 1, seed, needtype,
                    needSpectr);
            }
        }

        var astrosData = galaxy.astrosData;
        var stars = galaxy.stars;
        for (var index = 0; index < galaxy.astrosData.Count; ++index)
        {
            var tmp = astrosData[index];
            tmp.uRot.w = 1f;
            tmp.uRotNext.w = 1f;
            astrosData[index] = tmp;
        }

        for (var index = 0; index < tempPoses; ++index)
        {
            StarGen.CreateStarPlanets(galaxy, stars[index], gameDesc);
            var astroId = stars[index].astroId;
            astrosData.TryGetValue(astroId, out var tmp);
            tmp.id = astroId;
            tmp.type = EAstroType.Star;
            tmp.uPos = tmp.uPosNext = stars[index].uPosition;
            tmp.uRot = tmp.uRotNext = Quaternion.identity;
            tmp.uRadius = stars[index].physicsRadius;
            astrosData[astroId] = tmp;
        }

        if (tempPoses > 0)
        {
            var starData = stars[0];
            for (var index = 0; index < starData.planetCount; ++index)
            {
                var planet = starData.planets[index];
                var themeProto = LDB.themes.Select(planet.theme);
                if (themeProto != null && themeProto.Distribute == EThemeDistribute.Birth)
                {
                    galaxy.birthPlanetId = planet.id;
                    galaxy.birthStarId = starData.id;
                    break;
                }
            }
        }

        PlanetGen.gasCoef = 1f;
        return galaxy;
    }

    private static int GenerateTempPoses(
        int seed,
        int targetCount,
        int iterCount,
        double minDist,
        double minStepLen,
        double maxStepLen,
        double flatten)
    {
        if (tmp_poses == null)
        {
            tmp_poses = new List<VectorLF3>();
            tmp_drunk = new List<VectorLF3>();
        }
        else
        {
            tmp_poses.Clear();
            tmp_drunk.Clear();
        }

        if (iterCount < 1)
            iterCount = 1;
        else if (iterCount > 16)
            iterCount = 16;
        RandomPoses(seed, targetCount * iterCount, minDist, minStepLen, maxStepLen, flatten);
        for (var index = tmp_poses.Count - 1; index >= 0; --index)
        {
            if (index % iterCount != 0)
                tmp_poses.RemoveAt(index);
            if (tmp_poses.Count <= targetCount)
                break;
        }

        return tmp_poses.Count;
    }

    private static void RandomPoses(
        int seed,
        int maxCount,
        double minDist,
        double minStepLen,
        double maxStepLen,
        double flatten)
    {
        var dotNet35Random = new DotNet35Random(seed);
        var num1 = dotNet35Random.NextDouble();
        tmp_poses.Add(VectorLF3.zero);
        var num2 = 6;
        var num3 = 8;
        if (num2 < 1)
            num2 = 1;
        if (num3 < 1)
            num3 = 1;
        double num4 = num3 - num2;
        var num5 = (int)(num1 * num4 + num2);
        for (var index = 0; index < num5; ++index)
        {
            var num6 = 0;
            while (num6++ < 256)
            {
                var num7 = dotNet35Random.NextDouble() * 2.0 - 1.0;
                var num8 = (dotNet35Random.NextDouble() * 2.0 - 1.0) * flatten;
                var num9 = dotNet35Random.NextDouble() * 2.0 - 1.0;
                var num10 = dotNet35Random.NextDouble();
                var d = num7 * num7 + num8 * num8 + num9 * num9;
                if (d <= 1.0 && d >= 1E-08)
                {
                    var num11 = Math.Sqrt(d);
                    var num12 = (num10 * (maxStepLen - minStepLen) + minDist) / num11;
                    var pt = new VectorLF3(num7 * num12, num8 * num12, num9 * num12);
                    if (!CheckCollision(tmp_poses, pt, minDist))
                    {
                        tmp_drunk.Add(pt);
                        tmp_poses.Add(pt);
                        if (tmp_poses.Count >= maxCount)
                            return;
                        break;
                    }
                }
            }
        }

        var num13 = 0;
        while (num13++ < 256)
            for (var index = 0; index < tmp_drunk.Count; ++index)
                if (dotNet35Random.NextDouble() <= 0.7)
                {
                    var num14 = 0;
                    while (num14++ < 256)
                    {
                        var num15 = dotNet35Random.NextDouble() * 2.0 - 1.0;
                        var num16 = (dotNet35Random.NextDouble() * 2.0 - 1.0) * flatten;
                        var num17 = dotNet35Random.NextDouble() * 2.0 - 1.0;
                        var num18 = dotNet35Random.NextDouble();
                        var d = num15 * num15 + num16 * num16 + num17 * num17;
                        if (d <= 1.0 && d >= 1E-08)
                        {
                            var num19 = Math.Sqrt(d);
                            var num20 = (num18 * (maxStepLen - minStepLen) + minDist) / num19;
                            var pt = new VectorLF3(tmp_drunk[index].x + num15 * num20,
                                tmp_drunk[index].y + num16 * num20, tmp_drunk[index].z + num17 * num20);
                            if (!CheckCollision(tmp_poses, pt, minDist))
                            {
                                tmp_drunk[index] = pt;
                                tmp_poses.Add(pt);
                                if (tmp_poses.Count >= maxCount)
                                    return;
                                break;
                            }
                        }
                    }
                }
    }

    private static bool CheckCollision(List<VectorLF3> pts, VectorLF3 pt, double min_dist)
    {
        var num1 = min_dist * min_dist;
        foreach (var pt1 in pts)
        {
            var num2 = pt.x - pt1.x;
            var num3 = pt.y - pt1.y;
            var num4 = pt.z - pt1.z;
            if (num2 * num2 + num3 * num3 + num4 * num4 < num1)
                return true;
        }

        return false;
    }
}