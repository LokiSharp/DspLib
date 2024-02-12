using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Gen;

public static class UniverseGen
{
    public static int AlgoVersion = 20200403;

    [ThreadStatic] private static List<VectorLF3> _tmpPoses;

    [ThreadStatic] private static List<VectorLF3> _tmpDrunk;

    private static int[] _tmpState = null;

    /// <summary>
    ///     创建一个新的银河系。
    /// </summary>
    /// <param name="gameDesc">包含各种游戏选项和设置的 GameDesc 对象</param>
    /// <returns>表示新创建的银河系的 GalaxyData 对象</returns>
    public static GalaxyData CreateGalaxy(GameDesc gameDesc)
    {
        #region 初始化银河系数据对象并生成用于计算的随机数

        // 从 GameDesc 对象中提取银河系种子（galaxySeed）及恒星类天体数量（starCount）
        var galaxySeed = gameDesc.galaxySeed;
        var starCount = gameDesc.starCount;
        // 根据是否设置了稀有资源来设置气体系数
        PlanetGen.gasCoef = gameDesc.isRareResource ? 0.8f : 1f;
        // 使用银河系种子初始化随机数生成器
        var dotNet35Random = new DotNet35Random(galaxySeed);
        // 生成临时位置列表（表示天体在银河系中的位置）
        var tempPoses = GenerateTempPoses(dotNet35Random.Next(), starCount, 4, 2.0, 2.3, 3.5, 0.18);

        // 创建新的银河系数据对象，设定种子、天体数量和天体数据等属性
        var galaxy = new GalaxyData
        {
            seed = galaxySeed,
            starCount = tempPoses,
            stars = new StarData[tempPoses]
        };
        // 如果没有生成天体的位置，则立即返回创建的银河系
        if (tempPoses <= 0)
            return galaxy;
        // 生成一些随机浮点数
        var num1 = (float)dotNet35Random.NextDouble();
        var num2 = (float)dotNet35Random.NextDouble();
        var num3 = (float)dotNet35Random.NextDouble();
        var num4 = (float)dotNet35Random.NextDouble();
        // 使用生成的随机数和天体位置数量运行一些数学计算，得出一些新的变量
        var num5 = Mathf.CeilToInt((float)(0.0099999997764825821 * tempPoses +
                                           num1 * 0.30000001192092896)); // 取整前取值范围 (0。32, 0.94) 取整后为 [1, 1] 用于控制黑洞的下标偏移
        var num6 = Mathf.CeilToInt((float)(0.0099999997764825821 * tempPoses +
                                           num2 * 0.30000001192092896)); // 取整前取值范围 (0.32, 0.94) 取整后为 [1, 1] 用于控制中子星下标偏移
        var num7 = Mathf.CeilToInt((float)(0.016000000759959221 * tempPoses +
                                           num3 *
                                           0.40000000596046448)); // 取整前取值范围 (0.512, 1.424) 取整后为 [1, 2] 用于控制白矮星下标偏移
        var num8 = Mathf.CeilToInt((float)(0.013000000268220901 * tempPoses +
                                           num4 * 1.3999999761581421)); // 取整前取值范围 (0.416, 2.23) 取整后为 [1, 3] 用于控制巨星的
        var num9 = tempPoses - num5; // 取值范围 [31, 63] 黑洞的位置
        var num10 = num9 - num6; // 取值范围 [30, 62] 中子星的位置
        var num11 = num10 - num7; // 取值范围 [29, 60] 白矮星的位置
        var num12 = (num11 - 1) / num8; // 取值范围 [14, 59]
        var num13 = num12 / 2; // 取值范围 [7, 29]

        #endregion

        #region 遍历所有位置，为每个位置创建天体并设置天体的属性

        for (var index = 0; index < tempPoses; ++index)
        {
            var seed = dotNet35Random.Next();
            // 若 index 等于 0，创建初始星系
            if (index == 0)
            {
                galaxy.stars[index] = StarGen.CreateBirthStar(galaxy, gameDesc, seed);
            }
            // 否则创建天体，并根据计算出的变量确定天体的类型和光谱类型
            else
            {
                var needSpectr = ESpectrType.X; // 默认光谱类型为 X
                if (index == 3)
                    needSpectr = ESpectrType.M; // 如果 index 为 3，光谱类型为 M
                else if (index == num11 - 1)
                    needSpectr = ESpectrType.O; // 如果 index 为 [32, 64] - [1, 1] - [1, 1] - [1, 2] - 1，即白矮星的前一颗，光谱类型为 O
                var needtype = EStarType.MainSeqStar; // 默认为恒星
                if (index % num12 == num13)
                    needtype = EStarType.GiantStar; // 如果 index 对 [32, 64] - [1, 1] - [1, 1] - [1, 2] - 1 求模为模量的一半，为巨星
                if (index >= num9)
                    needtype = EStarType.BlackHole; // 如果 index 为 [32, 64] - [1, 1]，即最后一颗, 为黑洞
                else if (index >= num10)
                    needtype = EStarType.NeutronStar; // 如果 index 为 [32, 64] - [1, 1] - [1, 1]，即黑洞前一颗, 为中子星
                else if (index >= num11)
                    needtype = EStarType
                        .WhiteDwarf; // 如果 index 大于 [32, 64] - [1, 1] - [1, 1] - [1, 2]，即中子星前 1 到 2 颗, 为白矮星
                galaxy.stars[index] = StarGen.CreateStar(galaxy, _tmpPoses[index], gameDesc, index + 1, seed, needtype,
                    needSpectr);
            }
        }

        #endregion

        #region 遍历所有位置，生成天体星系的行星，并更新行星的属性

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

        #endregion

        #region 查找并设置初始星球

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

        #endregion

        // 重置气体系数为1
        PlanetGen.gasCoef = 1f;

        // 返回创建的银河系
        return galaxy;
    }

    /// <summary>
    ///     在指定的约束和迭代次数下，生成一组暂存的随机位置。
    /// </summary>
    /// <param name="seed">随机数生成的种子</param>
    /// <param name="targetCount">要生成的目标位置数量</param>
    /// <param name="iterCount">产生足够数量位置的迭代运行次数</param>
    /// <param name="minDist">各位置之间的最小距离</param>
    /// <param name="minStepLen">随机偏移的最小步进长度</param>
    /// <param name="maxStepLen">随机偏移的最大步进长度</param>
    /// <param name="flatten">影响3D点分布的额外参数</param>
    /// <returns>成功生成的位置的数量</returns>
    private static int GenerateTempPoses(
        int seed,
        int targetCount,
        int iterCount,
        double minDist,
        double minStepLen,
        double maxStepLen,
        double flatten)
    {
        if (_tmpPoses == null)
        {
            _tmpPoses = new List<VectorLF3>();
            _tmpDrunk = new List<VectorLF3>();
        }
        else
        {
            _tmpPoses.Clear();
            _tmpDrunk.Clear();
        }

        if (iterCount < 1)
            iterCount = 1;
        else if (iterCount > 16)
            iterCount = 16;
        RandomPoses(seed, targetCount * iterCount, minDist, minStepLen, maxStepLen, flatten);
        for (var index = _tmpPoses.Count - 1; index >= 0; --index)
        {
            if (index % iterCount != 0)
                _tmpPoses.RemoveAt(index);
            if (_tmpPoses.Count <= targetCount)
                break;
        }

        return _tmpPoses.Count;
    }

    /// <summary>
    ///     在给定的最大计数和约束下，在3D空间中生成随机点。
    /// </summary>
    /// <param name="seed">点随机偏移的种子</param>
    /// <param name="maxCount">要生成的点的最大数量</param>
    /// <param name="minDist">每个点必须与其他点保持的最小距离</param>
    /// <param name="minStepLen">随机偏移的最小步长</param>
    /// <param name="maxStepLen">随机偏移的最大步长</param>
    /// <param name="flatten">控制应用于点生成的平展程度的参数</param>
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
        _tmpPoses.Add(VectorLF3.zero);
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
                    if (!CheckCollision(_tmpPoses, pt, minDist))
                    {
                        _tmpDrunk.Add(pt);
                        _tmpPoses.Add(pt);
                        if (_tmpPoses.Count >= maxCount)
                            return;
                        break;
                    }
                }
            }
        }

        var num13 = 0;
        while (num13++ < 256)
            for (var index = 0; index < _tmpDrunk.Count; ++index)
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
                            var pt = new VectorLF3(_tmpDrunk[index].x + num15 * num20,
                                _tmpDrunk[index].y + num16 * num20, _tmpDrunk[index].z + num17 * num20);
                            if (!CheckCollision(_tmpPoses, pt, minDist))
                            {
                                _tmpDrunk[index] = pt;
                                _tmpPoses.Add(pt);
                                if (_tmpPoses.Count >= maxCount)
                                    return;
                                break;
                            }
                        }
                    }
                }
    }

    /// <summary>
    ///     检查给定点与点集之间是否发生碰撞。
    ///     如果给定点和点集中任一点的平方距离小于设定的最小距离的平方，即发生碰撞。
    /// </summary>
    /// <param name="pts">用于检查碰撞的点集。</param>
    /// <param name="pt">要检查是否发生碰撞的点。</param>
    /// <param name="minDist">碰撞的最小距离。</param>
    /// <returns>如果发生碰撞（即，给定点和列表中任何点的平方距离小于设定的最小距离的平方），则返回true；否则，返回false。</returns>
    private static bool CheckCollision(List<VectorLF3> pts, VectorLF3 pt, double minDist)
    {
        var num1 = minDist * minDist;
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