using System.Collections.Concurrent;
using DspLib.Dyson;
using DspLib.Enum;
using DspLib.Galaxy;

namespace DspLib.DataBase;

public class DatabaseInserter()
{
    private readonly DatabaseSecrets databaseSecrets;

    public DatabaseInserter(DatabaseSecrets databaseSecrets) : this()
    {
        this.databaseSecrets = databaseSecrets;
        PlanetModelingManager.Start();
        RandomTable.Init();
    }

    public string GetPlanetTypeString(PlanetData planetData)
    {
        var typeString = "未知";
        var LDBT = LDB.themes;
        var themeProto = LDBT.Select(planetData.theme);
        if (themeProto != null)
            typeString = themeProto.DisplayName;
        return typeString;
    }

    public async Task AddAndSaveChangesInBatch(List<SeedInfo> seeds)
    {
        await using var dspDbContext = new DspDbContext(databaseSecrets);

        foreach (var seed in seeds)
        {
            dspDbContext.SeedGalaxyInfo.AddRange(seed.SeedGalaxyInfos);
            dspDbContext.SeedPlanetsTypeCountInfo.AddRange(seed.SeedPlanetsTypeCountInfo);
            dspDbContext.SeedStarsTypeCountInfo.AddRange(seed.SeedStarsTypeCountInfo);
            dspDbContext.SeedInfo.Add(seed);
        }

        await dspDbContext.SaveChangesAsync();
    }

    public SeedInfo GenerateSeedInfo(int seed, int starCount)
    {
        var gameDesc = new GameDesc();
        gameDesc.SetForNewGame(seed, starCount);
        StarsCompute.ComputeWithoutPlanetData(gameDesc, out var galaxyData);

        var starsTypeCountDictionary = GetStarsTypeCount(galaxyData);
        var planetsTypeCountDictionary = GetPlanetsTypeCount(galaxyData);

        var seedInfo = new SeedInfo
        {
            种子号 = seed,
            巨星数 = galaxyData.stars.Count(star => star.type == EStarType.GiantStar),
            最多卫星 = galaxyData.stars.Max(star => star.planets.Count(planet => planet.orbitAround > 0)),
            最多潮汐星 = galaxyData.stars.Max(star => star.planets.Count(
                planet => planet.singularity is
                    EPlanetSingularity.TidalLocked or
                    EPlanetSingularity.TidalLocked2 or
                    EPlanetSingularity.TidalLocked4
            )),
            潮汐星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.singularity is
                    EPlanetSingularity.TidalLocked or
                    EPlanetSingularity.TidalLocked2 or
                    EPlanetSingularity.TidalLocked4
            )),
            最多潮汐永昼永夜 = galaxyData.stars.Max(star => star.planets.Count(
                planet => planet.singularity is EPlanetSingularity.TidalLocked
            )),
            潮汐永昼永夜数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.singularity is EPlanetSingularity.TidalLocked
            )),
            熔岩星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.type is EPlanetType.Vocano
            )),
            海洋星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.type is EPlanetType.Ocean
            )),
            沙漠星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.type is EPlanetType.Desert
            )),
            冰冻星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.type is EPlanetType.Ice
            )),
            气态星球数 = galaxyData.stars.Sum(star => star.planets.Count(
                planet => planet.type is EPlanetType.Gas
            )),
            总星球数量 = galaxyData.stars.Sum(star => star.planetCount),
            最高亮度 = galaxyData.stars.Max(star => star.dysonLumino),
            星球总亮度 = galaxyData.stars.Sum(star => star.dysonLumino)
        };

        var seedGalaxyInfos = (from star in galaxyData.stars
            select new SeedGalaxyInfo
            {
                SeedInfo = seedInfo,
                恒星类型 = star.type,
                光谱类型 = star.spectr,
                恒星光度 = star.dysonLumino,
                星系距离 = (float)(star.uPosition - galaxyData.stars[0].uPosition).magnitude / 2400000.0f,
                环盖首星 = star.dysonRadius * 2 > star.planets[0].sunDistance,
                星系坐标x = (int)Math.Round(star.uPosition.x, 0, MidpointRounding.AwayFromZero),
                星系坐标y = (int)Math.Round(star.uPosition.y, 0, MidpointRounding.AwayFromZero),
                星系坐标z = (int)Math.Round(star.uPosition.z, 0, MidpointRounding.AwayFromZero),
                潮汐星数 = star.planets.Count(planet => planet.singularity == (EPlanetSingularity.TidalLocked &
                                                                           EPlanetSingularity.TidalLocked4)),
                最多卫星 = star.planets.Aggregate((a, b) => a.orbitAround > b.orbitAround ? a : b).orbitAround,
                星球数量 = star.planetCount,
                星球类型 = star.planets.Select(planet => planet.type).Distinct().ToArray(),
                是否有水 = star.planets.Any(planet => planet.waterItemId == 1000),
                有硫酸否 = star.planets.Any(planet => planet.waterItemId == 1116)
            }).ToList();

        var seedStarsTypeCountInfo = new SeedStarsTypeCountInfo
        {
            SeedInfo = seedInfo,
            M型恒星 = starsTypeCountDictionary[1],
            K型恒星 = starsTypeCountDictionary[2],
            G型恒星 = starsTypeCountDictionary[3],
            F型恒星 = starsTypeCountDictionary[4],
            A型恒星 = starsTypeCountDictionary[5],
            B型恒星 = starsTypeCountDictionary[6],
            O型恒星 = starsTypeCountDictionary[7],
            X型恒星 = starsTypeCountDictionary[8],
            M型巨星 = starsTypeCountDictionary[9],
            K型巨星 = starsTypeCountDictionary[10],
            G型巨星 = starsTypeCountDictionary[11],
            F型巨星 = starsTypeCountDictionary[12],
            A型巨星 = starsTypeCountDictionary[13],
            B型巨星 = starsTypeCountDictionary[14],
            O型巨星 = starsTypeCountDictionary[15],
            X型巨星 = starsTypeCountDictionary[16],
            白矮星 = starsTypeCountDictionary[17],
            中子星 = starsTypeCountDictionary[18],
            黑洞 = starsTypeCountDictionary[19]
        };

        var seedPlanetsTypeCountInfo = new SeedPlanetsTypeCountInfo
        {
            SeedInfo = seedInfo,
            地中海 = planetsTypeCountDictionary[1],
            气态巨星1 = planetsTypeCountDictionary[2],
            气态巨星2 = planetsTypeCountDictionary[3],
            冰巨星1 = planetsTypeCountDictionary[4],
            冰巨星2 = planetsTypeCountDictionary[5],
            干旱荒漠 = planetsTypeCountDictionary[6],
            灰烬冻土 = planetsTypeCountDictionary[7],
            海洋丛林 = planetsTypeCountDictionary[8],
            熔岩 = planetsTypeCountDictionary[9],
            冰原冻土 = planetsTypeCountDictionary[10],
            贫瘠荒漠 = planetsTypeCountDictionary[11],
            戈壁 = planetsTypeCountDictionary[12],
            火山灰 = planetsTypeCountDictionary[13],
            红石 = planetsTypeCountDictionary[14],
            草原 = planetsTypeCountDictionary[15],
            水世界 = planetsTypeCountDictionary[16],
            黑石盐滩 = planetsTypeCountDictionary[17],
            樱林海 = planetsTypeCountDictionary[18],
            飓风石林 = planetsTypeCountDictionary[19],
            猩红冰湖 = planetsTypeCountDictionary[20],
            气态巨星3 = planetsTypeCountDictionary[21],
            热带草原 = planetsTypeCountDictionary[22],
            橙晶荒漠 = planetsTypeCountDictionary[23],
            极寒冻土 = planetsTypeCountDictionary[24],
            潘多拉沼泽 = planetsTypeCountDictionary[25]
        };

        seedInfo.SeedGalaxyInfos = seedGalaxyInfos;
        seedInfo.SeedStarsTypeCountInfo = seedStarsTypeCountInfo;
        seedInfo.SeedPlanetsTypeCountInfo = seedPlanetsTypeCountInfo;
        return seedInfo;
    }

    public async Task InsertGalaxiesInfoInBatch(int startSeed, int maxSeed, int starCount)
    {
        var seedInfos = new BlockingCollection<SeedInfo>(10000);
        var existingSeeds = new HashSet<int>(new DspDbContext(databaseSecrets).SeedInfo.Select(si => si.种子号));
        var addAndSaveChangesInBatchSemaphore = new SemaphoreSlim(10);
        var throttle = new SemaphoreSlim(100); // limit number of concurrent tasks
        var tasks = new List<Task>();

        for (var seed = startSeed; seed < maxSeed; seed++)
        {
            await throttle.WaitAsync();
            var takenSeed = seed;
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    await Body(takenSeed);
                }
                finally
                {
                    throttle.Release();
                }
            }));
        }

        await Task.WhenAll(tasks);

        var remaining = seedInfos.ToList();
        if (remaining.Count > 0) await AddAndSaveChangesInBatch(remaining);
        return;

        async Task Body(int seed)
        {
            if (existingSeeds.Contains(seed)) return;
            var seedInfo = GenerateSeedInfo(seed, starCount);
            seedInfos.Add(seedInfo);

            if (seedInfos.Count < 1000) return;
            var toSubmit = new List<SeedInfo>();
            while (seedInfos.TryTake(out var takenSeed))
            {
                toSubmit.Add(takenSeed);
                if (toSubmit.Count < 1000) continue;
                await addAndSaveChangesInBatchSemaphore.WaitAsync();
                try
                {
                    await AddAndSaveChangesInBatch(toSubmit);
                    toSubmit.Clear();
                }
                finally
                {
                    addAndSaveChangesInBatchSemaphore.Release();
                }

                break;
            }
        }
    }

    public Dictionary<EVeinType, int> clacStarVein(StarData starData)
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

    public Dictionary<EVeinType, int> GetMineCount(StarData star)
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

    public Dictionary<int, int> GetPlanetsTypeCount(GalaxyData galaxy)
    {
        var starTypeCountDictionary = Enumerable.Range(0, 26).ToDictionary(key => key, value => 0);

        foreach (var star in galaxy.stars)
        foreach (var planet in star.planets)
            if (!starTypeCountDictionary.TryAdd(planet.theme, 1))
                starTypeCountDictionary[planet.theme] += 1;

        return starTypeCountDictionary;
    }

    public Dictionary<int, int> GetStarsTypeCount(GalaxyData galaxy)
    {
        var starTypeCountDictionary = Enumerable.Range(0, 20).ToDictionary(key => key, value => 0);
        foreach (var star in galaxy.stars)
        {
            var starTypeNum = star.type switch
            {
                EStarType.MainSeqStar =>
                    star.spectr switch
                    {
                        ESpectrType.M => 1,
                        ESpectrType.K => 2,
                        ESpectrType.G => 3,
                        ESpectrType.F => 4,
                        ESpectrType.A => 5,
                        ESpectrType.B => 6,
                        ESpectrType.O => 7,
                        ESpectrType.X => 8,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                EStarType.GiantStar =>
                    star.spectr switch
                    {
                        ESpectrType.M => 9,
                        ESpectrType.K => 10,
                        ESpectrType.G => 11,
                        ESpectrType.F => 12,
                        ESpectrType.A => 13,
                        ESpectrType.B => 14,
                        ESpectrType.O => 15,
                        ESpectrType.X => 16,
                        _ => throw new ArgumentOutOfRangeException()
                    },
                EStarType.WhiteDwarf => 17,
                EStarType.NeutronStar => 18,
                EStarType.BlackHole => 19,
                _ => throw new ArgumentOutOfRangeException()
            };
            if (!starTypeCountDictionary.TryAdd(starTypeNum, 1))
                starTypeCountDictionary[starTypeNum] += 1;
        }

        return starTypeCountDictionary;
    }
}