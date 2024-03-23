using System.Collections.Concurrent;
using Dapper;
using DspLib.DataBase.Model;
using DspLib.Dyson;
using Npgsql;

namespace DspLib.DataBase;

public class DatabaseInserter
{
    private readonly string connectionString;

    private readonly string seedGalaxyInfosInsertQuery = @"
INSERT INTO ""SeedGalaxyInfo""
(""SeedGalaxyInfoId"", ""SeedInfoId"", 恒星类型, 光谱类型, 恒星光度, 星系距离, 环盖首星, 星系坐标x, 星系坐标y, 星系坐标z, 潮汐星数, 最多卫星, 星球数量, 星球类型, 是否有水, 有硫酸否, 铁矿脉, 铜矿脉, 硅矿脉, 钛矿脉, 石矿脉, 煤矿脉, 原油涌泉, 可燃冰矿, 金伯利矿, 分形硅矿, 有机晶体矿, 光栅石矿, 刺笋矿脉, 单极磁矿) 
VALUES 
(@SeedGalaxyInfoId, @SeedInfoId, @恒星类型, @光谱类型, @恒星光度, @星系距离, @环盖首星, @星系坐标x, @星系坐标y, @星系坐标z, @潮汐星数, @最多卫星, @星球数量, @星球类型String, @是否有水, @有硫酸否, @铁矿脉, @铜矿脉, @硅矿脉, @钛矿脉, @石矿脉, @煤矿脉, @原油涌泉, @可燃冰矿, @金伯利矿, @分形硅矿, @有机晶体矿, @光栅石矿, @刺笋矿脉, @单极磁矿);";

    private readonly string seedInfoInsertQuery = @"
INSERT INTO ""SeedInfo""
(""SeedInfoId"", 种子号, 巨星数, 最多卫星, 最多潮汐星, 潮汐星球数, 最多潮汐永昼永夜, 潮汐永昼永夜数, 熔岩星球数, 海洋星球数, 沙漠星球数, 冰冻星球数, 气态星球数, 总星球数量, 最高亮度, 星球总亮度) 
VALUES
(@SeedInfoId, @种子号, @巨星数, @最多卫星, @最多潮汐星, @潮汐星球数, @最多潮汐永昼永夜, @潮汐永昼永夜数, @熔岩星球数, @海洋星球数, @沙漠星球数, @冰冻星球数, @气态星球数, @总星球数量, @最高亮度, @星球总亮度);";

    private readonly string seedPlanetsTypeCountInfoInsertQuery = @"
INSERT INTO ""SeedPlanetsTypeCountInfo""
(""SeedInfoId"", 地中海, 气态巨星1, 气态巨星2, 冰巨星1, 冰巨星2, 干旱荒漠, 灰烬冻土, 海洋丛林, 熔岩, 冰原冻土, 贫瘠荒漠, 戈壁, 火山灰, 红石, 草原, 水世界, 黑石盐滩, 樱林海, 飓风石林, 猩红冰湖, 气态巨星3, 热带草原, 橙晶荒漠, 极寒冻土, 潘多拉沼泽) 
VALUES 
(@SeedInfoId, @地中海, @气态巨星1, @气态巨星2, @冰巨星1, @冰巨星2, @干旱荒漠, @灰烬冻土, @海洋丛林, @熔岩, @冰原冻土, @贫瘠荒漠, @戈壁, @火山灰, @红石, @草原, @水世界, @黑石盐滩, @樱林海, @飓风石林, @猩红冰湖, @气态巨星3, @热带草原, @橙晶荒漠, @极寒冻土, @潘多拉沼泽);";

    private readonly string seedStarsTypeCountInfoInsertQuery = @"
INSERT INTO ""SeedStarsTypeCountInfo""
(""SeedInfoId"", M型恒星, K型恒星, G型恒星, F型恒星, A型恒星, B型恒星, O型恒星, X型恒星, M型巨星, K型巨星, G型巨星, F型巨星, A型巨星, B型巨星, O型巨星, X型巨星, 白矮星, 中子星, 黑洞) 
VALUES 
(@SeedInfoId, @M型恒星, @K型恒星, @G型恒星, @F型恒星, @A型恒星, @B型恒星, @O型恒星, @X型恒星, @M型巨星, @K型巨星, @G型巨星, @F型巨星, @A型巨星, @B型巨星, @O型巨星, @X型巨星, @白矮星, @中子星, @黑洞);";


    public DatabaseInserter(string connectionString)
    {
        this.connectionString = connectionString;
        PlanetModelingManager.Start();
        RandomTable.Init();
    }

    private async Task AddAndSaveChangesInBatch(List<SeedInfo> seeds)
    {
        await using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            var seedInfos = new List<SeedInfo>();
            var seedGalaxyInfos = new List<SeedGalaxyInfo>();
            var seedPlanetsTypeCountInfos = new List<SeedPlanetsTypeCountInfo>();
            var seedStarsTypeCountInfos = new List<SeedStarsTypeCountInfo>();
            foreach (var seedInfo in seeds)
            {
                seedInfo.SeedInfoId = seedInfo.种子号;
                seedInfos.Add(seedInfo);

                var i = 0;
                foreach (var seedGalaxyInfo in seedInfo.SeedGalaxyInfos!)
                {
                    i++;
                    seedGalaxyInfo.SeedInfoId = seedInfo.SeedInfoId;
                    seedGalaxyInfo.SeedGalaxyInfoId = seedInfo.SeedInfoId * 64 + i;
                    seedGalaxyInfos.Add(seedGalaxyInfo);
                }

                seedInfo.SeedPlanetsTypeCountInfo!.SeedInfoId = seedInfo.SeedInfoId;
                seedPlanetsTypeCountInfos.Add(seedInfo.SeedPlanetsTypeCountInfo);

                seedInfo.SeedStarsTypeCountInfo!.SeedInfoId = seedInfo.SeedInfoId;
                seedStarsTypeCountInfos.Add(seedInfo.SeedStarsTypeCountInfo);
            }

            await connection.ExecuteAsync(seedInfoInsertQuery, seedInfos, transaction);
            await connection.ExecuteAsync(seedGalaxyInfosInsertQuery, seedGalaxyInfos, transaction);
            await connection.ExecuteAsync(seedPlanetsTypeCountInfoInsertQuery, seedPlanetsTypeCountInfos, transaction);
            await connection.ExecuteAsync(seedStarsTypeCountInfoInsertQuery, seedStarsTypeCountInfos, transaction);
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task InsertGalaxiesInfoInBatch(int startSeed, int maxSeed, int starCount)
    {
        var existingSeeds = GetSeedIdFromAllSeedInfoTables();
        var seedInfos = new ConcurrentBag<SeedInfo>();

        var commitLock = new SemaphoreSlim(1, 1);
        var throttler = new SemaphoreSlim(20, 20);
        var seeds = new HashSet<int>(Enumerable.Range(startSeed, maxSeed - startSeed + 1));
        seeds.ExceptWith(existingSeeds);

        Task.Run(async () =>
        {
            while (true)
            {
                await commitLock.WaitAsync();
                try
                {
                    if (seedInfos.Count >= 1000)
                    {
                        Console.WriteLine($"Commit, Task: {throttler.CurrentCount}, seedInfos: {seedInfos.Count}");
                        await Commit();
                        Console.WriteLine("Commit finish");
                    }
                }
                finally
                {
                    commitLock.Release();
                    await Task.Delay(1000);
                }
            }
        });

        foreach (var seed in seeds)
        {
            await throttler.WaitAsync();
            await Task.Run(() =>
            {
                try
                {
                    Body(seed);
                }
                finally
                {
                    throttler.Release();
                }
            });
        }

        var remaining = seedInfos.ToList();
        if (remaining.Count > 0) await AddAndSaveChangesInBatch(remaining);
        return;

        void Body(int seed)
        {
            var seedInfo = SeedGenerator.GenerateSeedInfo(seed, starCount);
            seedInfos.Add(seedInfo);
        }

        async Task Commit()
        {
            const int batchSize = 100;
            var seedBatches = new List<List<SeedInfo>>();

            var toSubmit = new List<SeedInfo>();

            while (seedInfos.TryTake(out var takenSeed)) toSubmit.Add(takenSeed);

            for (var i = 0; i < toSubmit.Count; i += batchSize)
                seedBatches.Add(toSubmit.GetRange(i, Math.Min(batchSize, toSubmit.Count - i)));

            var tasks = seedBatches.Select(AddAndSaveChangesInBatch);

            await Task.WhenAll(tasks);
        }
    }

    private HashSet<int> GetSeedIdFromAllSeedInfoTables()
    {
        using var connection = new NpgsqlConnection(connectionString);
        const string sqlQuery = @"
SELECT 种子号, COUNT(*) as Count
FROM ""SeedInfo""
GROUP BY 种子号;";
        var result = connection.Query<int>(sqlQuery, commandTimeout: 600);

        return result.ToHashSet();
    }
}