using DspLib.Algorithms;
using DspLib.Enum;

namespace DspLib.Protos;

[Serializable]
public class ThemeProto : Proto
{
    public static int[] themeIds;
    public int[] Algos;
    public string BriefIntroduction;
    public float CullingRadius;
    public string DisplayName;
    public EThemeDistribute Distribute;
    public int EigenBit;
    public int[] GasItems;
    public float[] GasSpeeds;
    public int IceFlag;
    public float IonHeight;
    public string MaterialPath;
    public Vector2 ModX;
    public Vector2 ModY;
    public int[] Musics;
    public EPlanetType PlanetType;
    public float[] RareSettings;
    public int[] RareVeins;
    public string SFXPath;
    public float SFXVolume;
    public float Temperature;
    public bool UseHeightForBuild;
    public int[] Vegetables0;
    public int[] Vegetables1;
    public int[] Vegetables2;
    public int[] Vegetables3;
    public int[] Vegetables4;
    public int[] Vegetables5;
    public float[] VeinCount;
    public float[] VeinOpacity;
    public int[] VeinSpot;
    public float WaterHeight;
    public int WaterItemId;
    public float Wind;

    public string displayName { get; set; }
}