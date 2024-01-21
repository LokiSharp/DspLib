public class PlanetAlgorithm0 : PlanetAlgorithm
{
    public override void GenerateTerrain(double modX, double modY)
    {
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            data.heightData[index] = (ushort)(planet.radius * 100.0);
            data.biomoData[index] = 0;
        }
    }
}