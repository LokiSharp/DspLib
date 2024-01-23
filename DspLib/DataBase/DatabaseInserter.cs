using DspLib.Dyson;
using DspLib.Enum;
using DspLib.Galaxy;

namespace DspLib.DataBase;

public class DatabaseInserter
{
    public static void InsertGalaxiesInfo(DatabaseSecrets databaseSecrets)
    {
        using (var context = new DspDbContext(databaseSecrets))
        {
            PlanetModelingManager.Start();
            RandomTable.Init();
            var mSeed = 0;
            var mStarCount = 64;
            var gameDesc = new GameDesc();
            gameDesc.SetForNewGame(mSeed, mStarCount);
            StarsCompute.Compute(gameDesc, out var galaxyData);
            var galaxiesInfo = new GalaxiesInfo
            {
                GalaxiesInfoId = 0,
                种子号码 = 0,
                恒星类型 = EStarType.MainSeqStar,
                光谱类型 = ESpectrType.M,
                恒星光度 = 0,
                星系距离 = 0,
                环盖首星 = false,
                星系坐标x = 0,
                星系坐标y = 0,
                星系坐标z = 0,
                潮汐星数 = 0,
                最多卫星 = 0,
                星球数量 = 0,
                星球类型 = new EPlanetType[]
                {
                },
                是否有水 = false,
                有硫酸否 = false,
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
            };
        }
    }
}