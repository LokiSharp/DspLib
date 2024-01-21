namespace DspLib.Protos;

[Serializable]
public abstract class Proto
{
    public int ID;
    public string Name;
    public string SID;

    public string name { get; set; }

    public string sid { get; set; }
}