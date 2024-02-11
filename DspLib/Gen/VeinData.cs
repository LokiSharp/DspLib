using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Gen;

public struct VeinData
{
    public int id;
    public EVeinType type;
    public short modelIndex;
    public short groupIndex;
    public int amount;
    public int productId;
    public Vector3 pos;
    public int combatStatId;
    public int minerCount;
    public int minerId0;
    public int minerId1;
    public int minerId2;
    public int minerId3;
    public int hashAddress;
    public int modelId;
    public int colliderId;
    public int minerBaseModelId;
    public int minerCircleModelId0;
    public int minerCircleModelId1;
    public int minerCircleModelId2;
    public int minerCircleModelId3;
    public static float oilSpeedMultiplier = 4E-05f;
}