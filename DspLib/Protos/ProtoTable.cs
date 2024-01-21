namespace DspLib.Protos;

public abstract class ProtoTable
{
    public string Signature;
    public string TableName;

    public abstract Proto this[int index] { get; set; }

    public abstract int Length { get; }

    public abstract void Init(int length);
}