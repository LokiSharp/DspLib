using DspLib.Dyson;
using DspLib.Enum;
using DspLib.Galaxy;

namespace DspLib.DataBase;

public static class DatabaseInserter
{
    public static void InsertGalaxiesInfo(DatabaseSecrets databaseSecrets, int seed, int starCount)
    {
        using (var context = new DspDbContext(databaseSecrets))
        {
            PlanetModelingManager.Start();
            RandomTable.Init();
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(seed, starCount);
            StarsCompute.Compute(gameDesc, out var galaxyData);
            var galaxiesInfos = new List<GalaxiesInfo>();
            foreach (var star in galaxyData.stars)
            {
                var resourceCountList = GetMineCount(star);
                galaxiesInfos.Add(new GalaxiesInfo
                {
                    种子号码 = seed,
                    恒星类型 = star.type,
                    光谱类型 = star.spectr,
                    恒星光度 = star.dysonLumino,
                    星系距离 = (float)(star.uPosition - galaxyData.stars[0].uPosition).magnitude / 2400000.0f,
                    环盖首星 = star.dysonRadius * 2 > star.planets[0].sunDistance,
                    星系坐标x = (int)Math.Round(star.uPosition.x, 0, MidpointRounding.AwayFromZero),
                    星系坐标y = (int)Math.Round(star.uPosition.y, 0, MidpointRounding.AwayFromZero),
                    星系坐标z = (int)Math.Round(star.uPosition.z, 0, MidpointRounding.AwayFromZero),
                    潮汐星数 = star.planets.Count(planet =>
                        planet.singularity == (
                            EPlanetSingularity.TidalLocked &
                            EPlanetSingularity.TidalLocked2 &
                            EPlanetSingularity.TidalLocked4)
                    ),
                    最多卫星 = star.planets.Aggregate(
                        (a, b) =>
                            a.orbitAround > b.orbitAround ? a : b).orbitAround,
                    星球数量 = star.planetCount,
                    星球类型 = star.planets.Select(planet => planet.type).Distinct().ToArray(),
                    是否有水 = star.planets.Any(planet => planet.waterItemId == 1000),
                    有硫酸否 = star.planets.Any(planet => planet.waterItemId == 1116),
                    铁矿脉 = resourceCountList[EVeinType.Iron],
                    铜矿脉 = resourceCountList[EVeinType.Copper],
                    硅矿脉 = resourceCountList[EVeinType.Silicium],
                    钛矿脉 = resourceCountList[EVeinType.Titanium],
                    石矿脉 = resourceCountList[EVeinType.Stone],
                    煤矿脉 = resourceCountList[EVeinType.Coal],
                    原油涌泉 = resourceCountList[EVeinType.Oil],
                    可燃冰矿 = resourceCountList[EVeinType.Fireice],
                    金伯利矿 = resourceCountList[EVeinType.Diamond],
                    分形硅矿 = resourceCountList[EVeinType.Fractal],
                    有机晶体矿 = resourceCountList[EVeinType.Crysrub],
                    光栅石矿 = resourceCountList[EVeinType.Grat],
                    刺笋矿脉 = resourceCountList[EVeinType.Bamboo],
                    单极磁矿 = resourceCountList[EVeinType.Mag]
                });
            }

            context.GalaxiesInfos.AddRange(galaxiesInfos);
            context.SaveChanges();
        }
    }

    public static Dictionary<EVeinType, int> clacStarVein(StarData starData)
    {
        var veinGroupCounts = starData.planets
            .Where(planet => planet.type != EPlanetType.Gas)
            .SelectMany(planet => planet.data.veinPool)
            .GroupBy(veinData => veinData.type) // Group by EVeinType
            .ToDictionary(
                group => group.Key,
                group => group.Count(veinData => veinData.amount != 0));
        foreach (EVeinType veinType in System.Enum.GetValues(typeof(EVeinType))) veinGroupCounts.TryAdd(veinType, 0);
        return veinGroupCounts;
    }

    public static Dictionary<EVeinType, int> GetMineCount(StarData star)
    {
        var starVeinCountDictionary = System.Enum.GetValues(typeof(EVeinType))
            .Cast<EVeinType>()
            .ToDictionary(key => key, value => 0);

        foreach (var planet in star.planets)
        {
            var planetData = PlanetModelingManager.RefreshPlanetData(planet);
            if (planetData == null) continue;
            var planetVeinCountDictionary = System.Enum.GetValues(typeof(EVeinType))
                .Cast<EVeinType>()
                .Zip(planetData, (key, value) => new { key, value })
                .ToDictionary(pair => pair.key, pair => pair.value);

            foreach (var type in System.Enum.GetValues(typeof(EVeinType))
                         .Cast<EVeinType>()
                         .Where(type => type != EVeinType.Max))
                starVeinCountDictionary[type] += planetVeinCountDictionary[type];
        }

        return starVeinCountDictionary;
    }
}