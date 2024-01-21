using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Galaxy;

public struct VeinGroup
{
    public EVeinType type;
    public Vector3 pos;
    public int count;
    public long amount;

    public void SetNull()
    {
        type = EVeinType.None;
        pos.x = 0.0f;
        pos.y = 0.0f;
        pos.z = 0.0f;
        count = 0;
        amount = 0L;
    }

    public bool isNull => type == EVeinType.None;

    public override string ToString()
    {
        return string.Format("[{0}] {1:N0} | {2}   @ {3}", type, amount, count, pos);
    }
}