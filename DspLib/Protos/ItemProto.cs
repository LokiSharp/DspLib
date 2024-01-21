using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Protos;

public class ItemProto : Proto
{
    public const int kMaxProtoId = 12000;
    public const int kMaxSubId = 256;
    public const int kMaxProtoIndex = 9999;
    public const int kWaterId = 1000;
    public const int kDroneId = 5001;
    public const int kShipId = 5002;
    public const int kCourierId = 5003;
    public const int kConstructionDroneModelIndex = 454;
    public const int kRocketId = 1503;
    public const int kWarperId = 1210;
    public const int kVeinMinerId = 2301;
    public const int kOilMinerId = 2307;
    public const int kAccumulator = 2206;
    public const int kAccumulatorFull = 2207;
    public const int kSoilPileId = 1099;
    public const int kAmmoPlasma = 1607;
    public const int kAmmoAntimatter = 1608;
    public const int kDFMemoryUnitId = 5201;
    public const int kDFSiliconNeuronId = 5202;
    public const int kDFReassemblerId = 5203;
    public const int kDFSingularityId = 5204;
    public const int kDFVirtualParticleId = 5205;
    public const int kDFEnergyFragment = 5206;
    public const int kFighterIdRangeMin = 5100;
    public const int kFighterIdRangeMax = 5199;
    public static int stationCollectorId;

    public static int[] kFuelAutoReplenishIds = new int[17]
    {
        1804,
        1803,
        1802,
        1801,
        1130,
        1129,
        1128,
        1121,
        1120,
        1109,
        1011,
        1114,
        1007,
        1006,
        1117,
        1030,
        1031
    };

    public static int[][] fuelNeeds = new int[64][];
    public static int[][] turretNeeds = new int[16][];
    public static int[] fluids;
    public static int[] turrets;
    public static int[] enemyDropRangeTable;
    public static int[] enemyDropLevelTable;
    public static float[] enemyDropCountTable;
    public static int[] enemyDropMaskTable;
    public static float[] enemyDropMaskRatioTable;
    public static ItemProto[] itemProtoById;
    public static int[] itemIds;
    public static int[] itemIndices;
    public static int[] mechaMaterials;
    public static int[] kFighterIds;
    public static int[] kFighterGroundIds;
    public static int[] kFighterSpaceIds;
    public static int[] kFighterSpaceSmallIds;
    public static int[] kFighterSpaceLargeIds;
    public int Ability;
    public EAmmoType AmmoType;
    public int BombType;
    public int BuildIndex;
    public bool BuildInGas;
    public int BuildMode;
    public bool CanBuild;
    public int CraftType;
    public int[] DescFields;
    public string Description;
    public float DropRate;
    public float EnemyDropCount;
    public int EnemyDropLevel;
    public int EnemyDropMask;
    public float EnemyDropMaskRatio;
    public Vector2 EnemyDropRange;
    public int FuelType;
    public int Grade;
    public int GridIndex;
    public long HeatValue;
    public int HpMax;
    public string IconPath;
    public bool IsEntity;
    public bool IsFluid;
    public int MechaMaterialID;
    public string MiningFrom;
    public int ModelCount;
    public int ModelIndex;
    public long Potential;
    public int PreTechOverride;
    public string ProduceFrom;
    public bool Productive;
    public float ReactorInc;
    public int StackSize;
    public int SubID;
    public EItemType Type;
    public int UnlockKey;
    public int[] Upgrades;

    public string miningFrom { get; set; }

    public string produceFrom { get; set; }

    public string description { get; set; }

    public int index { get; }

    public string propertyName { get; }
}