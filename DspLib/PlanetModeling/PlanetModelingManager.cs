using DspLib;
using DspLib.Enum;
using DspLib.Gen;
using DspLib.Protos;

public static class PlanetModelingManager
{
    public static int[] veinProducts;
    public static int[] veinModelIndexs;
    public static int[] veinModelCounts;
    public static VeinProto[] veinProtos;

    public static PlanetAlgorithm Algorithm(PlanetData planet)
    {
        PlanetAlgorithm planetAlgorithm;
        switch (planet.algoId)
        {
            case 1:
                planetAlgorithm = new PlanetAlgorithm1();
                break;
            case 2:
                planetAlgorithm = new PlanetAlgorithm2();
                break;
            case 3:
                planetAlgorithm = new PlanetAlgorithm3();
                break;
            case 4:
                planetAlgorithm = new PlanetAlgorithm4();
                break;
            case 5:
                planetAlgorithm = new PlanetAlgorithm5();
                break;
            case 6:
                planetAlgorithm = new PlanetAlgorithm6();
                break;
            case 7:
                planetAlgorithm = new PlanetAlgorithm7();
                break;
            case 8:
                planetAlgorithm = new PlanetAlgorithm8();
                break;
            case 9:
                planetAlgorithm = new PlanetAlgorithm9();
                break;
            case 10:
                planetAlgorithm = new PlanetAlgorithm10();
                break;
            case 11:
                planetAlgorithm = new PlanetAlgorithm11();
                break;
            case 12:
                planetAlgorithm = new PlanetAlgorithm12();
                break;
            case 13:
                planetAlgorithm = new PlanetAlgorithm13();
                break;
            case 14:
                planetAlgorithm = new PlanetAlgorithm14();
                break;
            default:
                planetAlgorithm = new PlanetAlgorithm0();
                break;
        }

        planetAlgorithm?.Reset(planet.seed, planet);
        return planetAlgorithm;
    }

    public static int[] RefreshPlanetData(PlanetData planetData)
    {
        var planetAlgorithm = Algorithm(planetData);
        if (planetAlgorithm != null)
            if (planetData.type != EPlanetType.Gas)
                return planetAlgorithm.SimpleGenerateVeins();

        return null;
    }

    public static void Start()
    {
        PrepareWorks();
    }

    private static void PrepareWorks()
    {
        var length = 0;
        var dataArray2 = LDB.veins.dataArray;
        for (var index = 0; index < dataArray2.Length; ++index)
            length = dataArray2[index].ID + 1;
        veinProducts = new int[length];
        veinModelIndexs = new int[length];
        veinModelCounts = new int[length];
        veinProtos = new VeinProto[length];
        for (var index = 0; index < dataArray2.Length; ++index)
        {
            veinProducts[dataArray2[index].ID] = dataArray2[index].MiningItem;
            veinModelIndexs[dataArray2[index].ID] = dataArray2[index].ModelIndex;
            veinModelCounts[dataArray2[index].ID] = dataArray2[index].ModelCount;
            veinProtos[dataArray2[index].ID] = dataArray2[index];
        }
    }
}