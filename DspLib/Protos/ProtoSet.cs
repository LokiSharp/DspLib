namespace DspLib.Protos;

public class ProtoSet<T> : ProtoTable where T : Proto
{
    public T[] dataArray;
    private Dictionary<int, int> dataIndices;

    public override Proto this[int index]
    {
        get => dataArray[index];
        set => dataArray[index] = value as T;
    }

    public override int Length => dataArray.Length;

    public override void Init(int length)
    {
        dataArray = new T[length];
    }

    public virtual void OnBeforeSerialize()
    {
    }

    public virtual void OnAfterDeserialize()
    {
        dataIndices = new Dictionary<int, int>();
        for (var index = 0; index < dataArray.Length; ++index)
        {
            dataArray[index].name = dataArray[index].Name;
            dataArray[index].sid = dataArray[index].SID;
            dataIndices[dataArray[index].ID] = index;
        }
    }

    public T Select(int id)
    {
        return dataIndices.ContainsKey(id) ? dataArray[dataIndices[id]] : default;
    }

    public bool Exist(int id)
    {
        return dataIndices.ContainsKey(id);
    }
}