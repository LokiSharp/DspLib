namespace DspLib.Gen;

public class GalaxyData
{
    public const double AU = 40000.0;
    public const double LY = 2400000.0;
    public Dictionary<int, AstroData> astrosData;
    public int birthPlanetId;
    public int birthStarId;
    public int habitableCount;
    public int seed;
    public int starCount;
    public StarData[] stars;

    public GalaxyData()
    {
        astrosData = new Dictionary<int, AstroData>();
    }
}