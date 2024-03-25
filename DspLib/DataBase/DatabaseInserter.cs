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

    private readonly SemaphoreSlim semaphore = new(100);


    public DatabaseInserter(string connectionString)
    {
        this.connectionString = connectionString;
        PlanetModelingManager.Start();
        RandomTable.Init();
    }

    private async Task AddAndSaveChangesInBatch(List<SeedInfo> seeds)
    {
        await semaphore.WaitAsync();
        try
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
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
                await connection.ExecuteAsync(seedPlanetsTypeCountInfoInsertQuery, seedPlanetsTypeCountInfos,
                    transaction);
                await connection.ExecuteAsync(seedStarsTypeCountInfoInsertQuery, seedStarsTypeCountInfos, transaction);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task InsertGalaxiesInfoInBatch(int startSeed, int maxSeed, int starCount)
    {
        const int highProducerWaterMark = 20000;
        const int lowProducerWaterMark = 10000;
        const int maxProducerCount = 20;
        const int highConsumerWaterMark = 10000;
        const int lowConsumerWaterMark = 5000;
        const int maxConsumerCount = 100;
        const int batchSize = 1000;

        var seeds = new HashSet<int>(Enumerable.Range(startSeed, maxSeed - startSeed + 1));
        var existingSeeds = GetSeedIdFromAllSeedInfoTables();
        seeds.ExceptWith(existingSeeds);
        var seedsQueue = new ConcurrentQueue<int>(seeds);
        var seedInfosQueue = new ConcurrentQueue<SeedInfo>();

        var producers = new List<Task>();
        var producerTokens = new List<CancellationTokenSource>();
        var producersLock = new object();
        var consumers = new List<Task>();
        var consumerTokens = new List<CancellationTokenSource>();
        var consumersLock = new object();

        var mainTask = Task.Run(() =>
        {
            while (true)
            {
                Console.WriteLine($"SeedsQueue count: {seedsQueue.Count:D8}\t" +
                                  $"SeedInfosQueue count: {seedInfosQueue.Count:D8}\t" +
                                  $"Producer tasks: {producers.Count(t => !t.IsCompleted):D2}\t" +
                                  $"Consumer tasks: {consumers.Count(t => !t.IsCompleted):D2}");

                if (seedsQueue.Count <= 0 && seedInfosQueue.Count <= 0) break;


                if (seedInfosQueue.Count < lowProducerWaterMark && producers.Count < maxProducerCount)
                {
                    var cts = new CancellationTokenSource();
                    producerTokens.Add(cts);
                    producers.Add(Task.Run(() => Producer(cts.Token), cts.Token));
                }
                else if (seedInfosQueue.Count > highProducerWaterMark && producers.Count > 1)
                {
                    lock (producersLock)
                    {
                        producerTokens[0].Cancel();
                        producerTokens.RemoveAt(0);
                        producers.RemoveAt(0);
                    }
                }

                if (seedInfosQueue.Count > highConsumerWaterMark && consumers.Count < maxConsumerCount)
                {
                    var cts = new CancellationTokenSource();
                    consumerTokens.Add(cts);
                    consumers.Add(Task.Run(() => Consume(cts.Token), cts.Token));
                }
                else if (seedInfosQueue.Count < lowConsumerWaterMark && consumers.Count > 1)
                {
                    lock (consumersLock)
                    {
                        consumerTokens[0].Cancel();
                        consumerTokens.RemoveAt(0);
                        consumers.RemoveAt(0);
                    }
                }

                Thread.Sleep(1000);
            }
        });

        mainTask.Wait();

        var remaining = seedInfosQueue.ToList();
        if (remaining.Count > 0) await AddAndSaveChangesInBatch(remaining);
        return;

        void Producer(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !seedsQueue.IsEmpty) GenerateSeed();
        }

        async Task Consume(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !seedInfosQueue.IsEmpty) await Commit();
        }

        void GenerateSeed()
        {
            while (seedsQueue.TryDequeue(out var takenSeed))
            {
                var seedInfo = SeedGenerator.GenerateSeedInfo(takenSeed, starCount);
                seedInfosQueue.Enqueue(seedInfo);
            }
        }

        async Task Commit()
        {
            var toSubmit = new List<SeedInfo>();

            while (seedInfosQueue.TryDequeue(out var takenSeed))
            {
                toSubmit.Add(takenSeed);
                if (toSubmit.Count >= batchSize) break;
            }

            await AddAndSaveChangesInBatch(toSubmit);
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