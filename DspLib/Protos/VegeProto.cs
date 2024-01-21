using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Protos;

public class VegeProto : Proto
{
    public const int kSpaceCapsuleId = 9999;
    public float CircleRadius;
    public int CollideAudio;
    public int HpMax;
    public int MiningAudio;
    public float[] MiningChance;
    public int[] MiningCount;
    public int MiningEffect;
    public int[] MiningItem;
    public int MiningTime;
    public int ModelIndex;
    public Vector4 ScaleRange;
    public EVegeType Type;
}