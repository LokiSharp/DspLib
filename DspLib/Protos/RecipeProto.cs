using DspLib.Enum;

namespace DspLib.Protos;

[Serializable]
public class RecipeProto : Proto
{
    public string Description;
    public bool Explicit;
    public int GridIndex;
    public bool Handcraft;
    public string IconPath;
    public int[] ItemCounts;
    public int[] Items;
    public bool NonProductive;
    public int[] ResultCounts;
    public int[] Results;
    public int TimeSpend;
    public ERecipeType Type;
}