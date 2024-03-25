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

    private void AddAndSaveChangesInBatch(List<SeedInfo> seeds)
    {
        using var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        using var transaction = connection.BeginTransaction();
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

            connection.Execute(seedInfoInsertQuery, seedInfos, transaction);
            connection.Execute(seedGalaxyInfosInsertQuery, seedGalaxyInfos, transaction);
            connection.Execute(seedPlanetsTypeCountInfoInsertQuery, seedPlanetsTypeCountInfos, transaction);
            connection.Execute(seedStarsTypeCountInfoInsertQuery, seedStarsTypeCountInfos, transaction);
            transaction.Commit();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            transaction.Rollback();
            throw;
        }
        finally
        {
            connection.Close();
        }
    }

    public void InsertGalaxiesInfoInBatch(int startSeed, int maxSeed, int starCount)
    {
        const int highProducerWaterMark = 20000;
        const int lowProducerWaterMark = 4000;
        const int maxProducerCount = 20;
        const int highConsumerWaterMark = 1000;
        const int lowConsumerWaterMark = 500;
        const int maxConsumerCount = 20;
        const int batchSize = 100;

        var producerCountersTotal = new ConcurrentDictionary<int, int>();
        var consumerCountersTotal = new ConcurrentDictionary<int, int>();
        var producerCountersLastSecond = new ConcurrentDictionary<int, int>();
        var consumerCountersLastSecond = new ConcurrentDictionary<int, int>();

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
            var runTime = 0;
            while (true)
            {
                runTime += 1;
                var totalProduced = producerCountersTotal.Values.Sum();
                var totalConsumed = consumerCountersTotal.Values.Sum();
                var producedLastSecond = producerCountersLastSecond.Values.Sum();
                var consumedLastSecond = consumerCountersLastSecond.Values.Sum();

                var producedPreSecond = totalProduced / runTime;
                var consumedPreSecond = totalConsumed / runTime;

                Console.WriteLine($"[Total] P: {totalProduced}, C: {totalConsumed} " +
                                  $"[Last second] P: {producedLastSecond}, C: {consumedLastSecond} " +
                                  $"[Pre Second] P: {producedPreSecond}, C: {consumedPreSecond}");
                producerCountersLastSecond.Clear();
                consumerCountersLastSecond.Clear();

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
        if (remaining.Count > 0) AddAndSaveChangesInBatch(remaining);
        return;

        void Producer(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !seedsQueue.IsEmpty) GenerateSeed();
        }

        void Consume(CancellationToken token)
        {
            while (!token.IsCancellationRequested && !seedInfosQueue.IsEmpty) Commit();
        }

        void GenerateSeed()
        {
            var toGenerate = new List<int>();
            var seedInfos = new List<SeedInfo>();

            while (seedsQueue.TryDequeue(out var takenSeed))
            {
                toGenerate.Add(takenSeed);
                if (toGenerate.Count >= batchSize) break;
            }

            foreach (var seed in toGenerate) seedInfos.Add(SeedGenerator.GenerateSeedInfo(seed, starCount));

            foreach (var seedInfo in seedInfos) seedInfosQueue.Enqueue(seedInfo);

            producerCountersTotal.AddOrUpdate(Task.CurrentId!.Value, toGenerate.Count,
                (_, oldValue) => oldValue + toGenerate.Count);
            producerCountersLastSecond.AddOrUpdate(Task.CurrentId!.Value, toGenerate.Count,
                (_, oldValue) => oldValue + toGenerate.Count);
        }

        void Commit()
        {
            var toCommit = new List<SeedInfo>();

            while (seedInfosQueue.TryDequeue(out var takenSeed))
            {
                toCommit.Add(takenSeed);
                if (toCommit.Count >= batchSize) break;
            }

            AddAndSaveChangesInBatch(toCommit);
            consumerCountersTotal.AddOrUpdate(Task.CurrentId!.Value, toCommit.Count,
                (_, oldValue) => oldValue + toCommit.Count);
            consumerCountersLastSecond.AddOrUpdate(Task.CurrentId!.Value, toCommit.Count,
                (_, oldValue) => oldValue + toCommit.Count);
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