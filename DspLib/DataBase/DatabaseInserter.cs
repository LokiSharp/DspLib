using DspLib.Dyson;
using DspLib.Enum;
using DspLib.Galaxy;

namespace DspLib.DataBase;

public class DatabaseInserter
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
                List<int> resourceCountList = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
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
                    铁矿脉 = 0,
                    铜矿脉 = 0,
                    硅矿脉 = 0,
                    钛矿脉 = 0,
                    石矿脉 = 0,
                    煤矿脉 = 0,
                    原油涌泉 = 0,
                    可燃冰矿 = 0,
                    金伯利矿 = 0,
                    分形硅矿 = 0,
                    有机晶体矿 = 0,
                    光栅石矿 = 0,
                    刺笋矿脉 = 0,
                    单极磁矿 = 0
                });
            }

            context.GalaxiesInfos.AddRange(galaxiesInfos);
            context.SaveChanges();
        }
    }
}