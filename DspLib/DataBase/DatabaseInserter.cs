using System.Collections.Concurrent;
using System.Data.Common;
using Dapper;
using DspLib.DataBase.Model;
using DspLib.Dyson;
using DuckDB.NET.Data;

namespace DspLib.DataBase;

public class DatabaseInserter
{
    private readonly string connectionString;

    public DatabaseInserter(string connectionString)
    {
        this.connectionString = connectionString;
        PlanetModelingManager.Start();
        RandomTable.Init();
    }

    private async Task AddAndSaveChangesInBatch(List<SeedInfo> seeds)
    {
        await using var connection = new DuckDBConnection(connectionString);
        connection.Open();

        await using var transaction = await connection.BeginTransactionAsync();
        try
        {
            foreach (var seedInfo in seeds)
            {
                seedInfo.SeedInfoId = seedInfo.种子号;
                await AddSeedInfo(seedInfo, connection, transaction);

                var i = 0;
                foreach (var seedGalaxyInfo in seedInfo.SeedGalaxyInfos!)
                {
                    i++;
                    seedGalaxyInfo.SeedInfoId = seedInfo.SeedInfoId;
                    seedGalaxyInfo.SeedGalaxyInfoId = seedInfo.SeedInfoId * 64 + i;
                    await AddSeedGalaxyInfo(seedGalaxyInfo, connection, transaction);
                }

                seedInfo.SeedPlanetsTypeCountInfo!.SeedInfoId = seedInfo.SeedInfoId;
                await AddSeedPlanetsTypeCountInfo(seedInfo.SeedPlanetsTypeCountInfo, connection,
                    transaction);

                seedInfo.SeedStarsTypeCountInfo!.SeedInfoId = seedInfo.SeedInfoId;
                await AddSeedStarsTypeCountInfo(seedInfo.SeedStarsTypeCountInfo, connection, transaction);
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task AddSeedInfo(SeedInfo seedInfo, DbConnection connection,
        DbTransaction transaction)
    {
        var seedInfoInsertQuery = @"
INSERT INTO SeedInfo
(SeedInfoId, 种子号, 巨星数, 最多卫星, 最多潮汐星, 潮汐星球数, 最多潮汐永昼永夜, 潮汐永昼永夜数, 熔岩星球数, 海洋星球数, 沙漠星球数, 冰冻星球数, 气态星球数, 总星球数量, 最高亮度, 星球总亮度) 
VALUES
($SeedInfoId, $种子号, $巨星数, $最多卫星, $最多潮汐星, $潮汐星球数, $最多潮汐永昼永夜, $潮汐永昼永夜数, $熔岩星球数, $海洋星球数, $沙漠星球数, $冰冻星球数, $气态星球数, $总星球数量, $最高亮度, $星球总亮度);";

        await connection.QueryAsync(seedInfoInsertQuery, seedInfo, transaction);
    }

    private async Task AddSeedGalaxyInfo(SeedGalaxyInfo seedGalaxyInfo, DbConnection connection,
        DbTransaction transaction)
    {
        var seedGalaxyInfosInsertQuery = @"
INSERT INTO SeedGalaxyInfo
(SeedGalaxyInfoId, SeedInfoId, 恒星类型, 光谱类型, 恒星光度, 星系距离, 环盖首星, 星系坐标x, 星系坐标y, 星系坐标z, 潮汐星数, 最多卫星, 星球数量, 星球类型, 是否有水, 有硫酸否, 铁矿脉, 铜矿脉, 硅矿脉, 钛矿脉, 石矿脉, 煤矿脉, 原油涌泉, 可燃冰矿, 金伯利矿, 分形硅矿, 有机晶体矿, 光栅石矿, 刺笋矿脉, 单极磁矿) 
VALUES 
($SeedGalaxyInfoId, $SeedInfoId, $恒星类型, $光谱类型, $恒星光度, $星系距离, $环盖首星, $星系坐标x, $星系坐标y, $星系坐标z, $潮汐星数, $最多卫星, $星球数量, $星球类型String, $是否有水, $有硫酸否, $铁矿脉, $铜矿脉, $硅矿脉, $钛矿脉, $石矿脉, $煤矿脉, $原油涌泉, $可燃冰矿, $金伯利矿, $分形硅矿, $有机晶体矿, $光栅石矿, $刺笋矿脉, $单极磁矿);";

        await connection.QueryAsync(seedGalaxyInfosInsertQuery, seedGalaxyInfo, transaction);
    }

    private async Task AddSeedPlanetsTypeCountInfo(SeedPlanetsTypeCountInfo seedPlanetsTypeCountInfo,
        DbConnection connection, DbTransaction transaction)
    {
        var seedPlanetsTypeCountInfoInsertQuery = @"
INSERT INTO SeedPlanetsTypeCountInfo
(SeedInfoId, 地中海, 气态巨星1, 气态巨星2, 冰巨星1, 冰巨星2, 干旱荒漠, 灰烬冻土, 海洋丛林, 熔岩, 冰原冻土, 贫瘠荒漠, 戈壁, 火山灰, 红石, 草原, 水世界, 黑石盐滩, 樱林海, 飓风石林, 猩红冰湖, 气态巨星3, 热带草原, 橙晶荒漠, 极寒冻土, 潘多拉沼泽) 
VALUES 
($SeedInfoId, $地中海, $气态巨星1, $气态巨星2, $冰巨星1, $冰巨星2, $干旱荒漠, $灰烬冻土, $海洋丛林, $熔岩, $冰原冻土, $贫瘠荒漠, $戈壁, $火山灰, $红石, $草原, $水世界, $黑石盐滩, $樱林海, $飓风石林, $猩红冰湖, $气态巨星3, $热带草原, $橙晶荒漠, $极寒冻土, $潘多拉沼泽);";

        await connection.QueryAsync(seedPlanetsTypeCountInfoInsertQuery, seedPlanetsTypeCountInfo, transaction);
    }

    private async Task AddSeedStarsTypeCountInfo(SeedStarsTypeCountInfo seedStarsTypeCountInfo,
        DbConnection connection, DbTransaction transaction)
    {
        var seedStarsTypeCountInfoInsertQuery = @"
INSERT INTO SeedStarsTypeCountInfo
(SeedInfoId, M型恒星, K型恒星, G型恒星, F型恒星, A型恒星, B型恒星, O型恒星, X型恒星, M型巨星, K型巨星, G型巨星, F型巨星, A型巨星, B型巨星, O型巨星, X型巨星, 白矮星, 中子星, 黑洞) 
VALUES 
($SeedInfoId, $M型恒星, $K型恒星, $G型恒星, $F型恒星, $A型恒星, $B型恒星, $O型恒星, $X型恒星, $M型巨星, $K型巨星, $G型巨星, $F型巨星, $A型巨星, $B型巨星, $O型巨星, $X型巨星, $白矮星, $中子星, $黑洞);";

        await connection.QueryAsync<ulong>(seedStarsTypeCountInfoInsertQuery, seedStarsTypeCountInfo, transaction);
    }

    public async Task InsertGalaxiesInfoInBatch(int startSeed, int maxSeed, int starCount)
    {
        var existingSeeds = await GetSeedIdFromAllSeedInfoTables();
        var seedInfos = new BlockingCollection<SeedInfo>(100000);

        var addAndSaveChangesInBatchSemaphore = new SemaphoreSlim(10);
        var throttle = new SemaphoreSlim(100); // limit number of concurrent tasks
        var tasks = new List<Task>();
        var seeds = new HashSet<int>(Enumerable.Range(startSeed, maxSeed - startSeed + 1));
        seeds.ExceptWith(existingSeeds);
        foreach (var seed in seeds)
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
            var seedInfo = SeedGenerator.GenerateSeedInfo(seed, starCount);
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

    private async Task<HashSet<int>> GetSeedIdFromAllSeedInfoTables()
    {
        const string sqlQuery = @"
SELECT 种子号, COUNT(*) as Count
FROM SeedInfo
GROUP BY 种子号;";
        var result = await new DuckDBConnection(connectionString).QueryAsync<int>(sqlQuery, commandTimeout: 600);

        return result.ToHashSet();
    }
}