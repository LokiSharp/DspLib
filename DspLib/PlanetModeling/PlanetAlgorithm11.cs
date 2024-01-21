using DspLib;
using DspLib.Algorithms;
using DspLib.Enum;
using DspLib.Galaxy;

public class PlanetAlgorithm11 : PlanetAlgorithm
{
    private readonly List<Vector2> tmp_vecs = new(100);
    private readonly Vector3[] veinVectors = new Vector3[512];
    private readonly EVeinType[] veinVectorTypes = new EVeinType[512];
    private int veinVectorCount;

    public override void GenerateTerrain(double modX, double modY)
    {
        var num1 = 0.007;
        var num2 = 0.007;
        var num3 = 0.007;
        var num4 = 0.002 * modX;
        var num5 = 0.002 * modX * 4.0;
        var num6 = 0.002 * modX;
        var dotNet35Random = new DotNet35Random(planet.seed);
        var seed1 = dotNet35Random.Next();
        var seed2 = dotNet35Random.Next();
        var seed3 = dotNet35Random.Next();
        var simplexNoise1 = new SimplexNoise(seed1);
        var simplexNoise2 = new SimplexNoise(seed2);
        var simplexNoise3 = new SimplexNoise(seed3);
        var data = planet.data;
        for (var index = 0; index < data.dataLength; ++index)
        {
            var num7 = data.vertices[index].x * (double)planet.radius;
            var num8 = data.vertices[index].y * (double)planet.radius;
            var num9 = data.vertices[index].z * (double)planet.radius;
            var num10 = simplexNoise2.Noise3DFBM(num7 * num1 * 4.0, num8 * num2 * 8.0, num9 * num3 * 4.0, 3);
            var num11 = 0.6;
            var num12 = Maths.Levelize2(
                Math.Pow(
                    Remap(-1.0, 1.0, 0.0, 1.0,
                        simplexNoise1.Noise3DFBM(num7 * num1 * num11, num8 * num1 * 1.5 * 2.5, num9 * num1 * num11, 6,
                            0.45, 1.8) * 0.95 + num10 * 0.05), modY) + 1.0);
            var num13 = Maths.Levelize3(Math.Pow(
                Remap(-1.0, 1.0, 0.0, 1.0, simplexNoise3.Noise3DFBM(num7 * num4, num8 * num5, num9 * num6, 5, 0.55)),
                0.65)) * num12;
            var num14 = Math.Max(-0.3, (num13 - 0.4) * 0.9);
            data.heightData[index] = (ushort)((planet.radius + num14) * 100.0);
            data.biomoData[index] = (byte)Mathf.Clamp((float)(num13 * 100.0), 0.0f, 200f);
        }
    }

    private double Remap(
        double sourceMin,
        double sourceMax,
        double targetMin,
        double targetMax,
        double x)
    {
        return (x - sourceMin) / (sourceMax - sourceMin) * (targetMax - targetMin) + targetMin;
    }

    public override void GenerateVeins()
    {
        lock (planet)
        {
            var themeProto = LDB.themes.Select(planet.theme);
            if (themeProto == null)
                return;
            var dotNet35Random1 = new DotNet35Random(planet.seed);
            dotNet35Random1.Next();
            dotNet35Random1.Next();
            dotNet35Random1.Next();
            dotNet35Random1.Next();
            var _birthSeed = dotNet35Random1.Next();
            var dotNet35Random2 = new DotNet35Random(dotNet35Random1.Next());
            var data = planet.data;
            var num1 = 2.1f / planet.radius;
            var veinProtos = PlanetModelingManager.veinProtos;
            var veinModelIndexs = PlanetModelingManager.veinModelIndexs;
            var veinModelCounts = PlanetModelingManager.veinModelCounts;
            var veinProducts = PlanetModelingManager.veinProducts;
            var destinationArray1 = new int[veinProtos.Length];
            var destinationArray2 = new float[veinProtos.Length];
            var destinationArray3 = new float[veinProtos.Length];
            if (themeProto.VeinSpot != null)
                Array.Copy(themeProto.VeinSpot, 0, destinationArray1, 1,
                    Math.Min(themeProto.VeinSpot.Length, destinationArray1.Length - 1));
            if (themeProto.VeinCount != null)
                Array.Copy(themeProto.VeinCount, 0, destinationArray2, 1,
                    Math.Min(themeProto.VeinCount.Length, destinationArray2.Length - 1));
            if (themeProto.VeinOpacity != null)
                Array.Copy(themeProto.VeinOpacity, 0, destinationArray3, 1,
                    Math.Min(themeProto.VeinOpacity.Length, destinationArray3.Length - 1));
            var p = 1f;
            var spectr = planet.star.spectr;
            switch (planet.star.type)
            {
                case EStarType.MainSeqStar:
                    switch (spectr)
                    {
                        case ESpectrType.M:
                            p = 2.5f;
                            break;
                        case ESpectrType.K:
                            p = 1f;
                            break;
                        case ESpectrType.G:
                            p = 0.7f;
                            break;
                        case ESpectrType.F:
                            p = 0.6f;
                            break;
                        case ESpectrType.A:
                            p = 1f;
                            break;
                        case ESpectrType.B:
                            p = 0.4f;
                            break;
                        case ESpectrType.O:
                            p = 1.6f;
                            break;
                    }

                    break;
                case EStarType.GiantStar:
                    p = 2.5f;
                    break;
                case EStarType.WhiteDwarf:
                    p = 3.5f;
                    ++destinationArray1[9];
                    ++destinationArray1[9];
                    for (var index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.44999998807907104; ++index)
                        ++destinationArray1[9];
                    destinationArray2[9] = 0.7f;
                    destinationArray3[9] = 1f;
                    ++destinationArray1[10];
                    ++destinationArray1[10];
                    for (var index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.44999998807907104; ++index)
                        ++destinationArray1[10];
                    destinationArray2[10] = 0.7f;
                    destinationArray3[10] = 1f;
                    ++destinationArray1[12];
                    for (var index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.5; ++index)
                        ++destinationArray1[12];
                    destinationArray2[12] = 0.7f;
                    destinationArray3[12] = 0.3f;
                    break;
                case EStarType.NeutronStar:
                    p = 4.5f;
                    ++destinationArray1[14];
                    for (var index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.64999997615814209; ++index)
                        ++destinationArray1[14];
                    destinationArray2[14] = 0.7f;
                    destinationArray3[14] = 0.3f;
                    break;
                case EStarType.BlackHole:
                    p = 5f;
                    ++destinationArray1[14];
                    for (var index = 1; index < 12 && dotNet35Random1.NextDouble() < 0.64999997615814209; ++index)
                        ++destinationArray1[14];
                    destinationArray2[14] = 0.7f;
                    destinationArray3[14] = 0.3f;
                    break;
            }

            for (var index1 = 0; index1 < themeProto.RareVeins.Length; ++index1)
            {
                var rareVein = themeProto.RareVeins[index1];
                var num2 = planet.star.index == 0
                    ? themeProto.RareSettings[index1 * 4]
                    : themeProto.RareSettings[index1 * 4 + 1];
                var rareSetting1 = themeProto.RareSettings[index1 * 4 + 2];
                var rareSetting2 = themeProto.RareSettings[index1 * 4 + 3];
                var num3 = rareSetting2;
                var num4 = 1f - Mathf.Pow(1f - num2, p);
                var num5 = 1f - Mathf.Pow(1f - rareSetting2, p);
                var num6 = 1f - Mathf.Pow(1f - num3, p);
                if (dotNet35Random1.NextDouble() < num4)
                {
                    ++destinationArray1[rareVein];
                    destinationArray2[rareVein] = num5;
                    destinationArray3[rareVein] = num5;
                    for (var index2 = 1; index2 < 12 && dotNet35Random1.NextDouble() < rareSetting1; ++index2)
                        ++destinationArray1[rareVein];
                }
            }

            var flag1 = planet.galaxy.birthPlanetId == planet.id;
            if (flag1)
                planet.GenBirthPoints(data, _birthSeed);
            var f = planet.star.resourceCoef;
            var num7 = 1f * 1.1f;
            Array.Clear(veinVectors, 0, veinVectors.Length);
            Array.Clear(veinVectorTypes, 0, veinVectorTypes.Length);
            veinVectorCount = 0;
            Vector3 birthPoint;
            if (flag1)
            {
                birthPoint = planet.birthPoint;
                birthPoint.Normalize();
                birthPoint *= 0.75f;
            }
            else
            {
                birthPoint.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                birthPoint.y = (float)dotNet35Random2.NextDouble() - 0.5f;
                birthPoint.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                birthPoint.Normalize();
                birthPoint *= (float)(dotNet35Random2.NextDouble() * 0.4 + 0.2);
            }

            planet.veinBiasVector = birthPoint;
            if (flag1)
            {
                veinVectorTypes[0] = EVeinType.Iron;
                veinVectors[0] = planet.birthResourcePoint0;
                veinVectorTypes[1] = EVeinType.Copper;
                veinVectors[1] = planet.birthResourcePoint1;
                veinVectorCount = 2;
            }

            for (var index3 = 1; index3 < 15 && veinVectorCount < veinVectors.Length; ++index3)
            {
                var eveinType = (EVeinType)index3;
                var num8 = destinationArray1[index3];
                if (num8 > 1)
                    num8 += dotNet35Random2.Next(-1, 2);
                for (var index4 = 0; index4 < num8; ++index4)
                {
                    var num9 = 0;
                    var zero = Vector3.zero;
                    var flag2 = false;
                    while (num9++ < 200)
                    {
                        zero.x = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        zero.y = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        zero.z = (float)(dotNet35Random2.NextDouble() * 2.0 - 1.0);
                        if (eveinType != EVeinType.Oil)
                            zero += birthPoint;
                        zero.Normalize();
                        var num10 = data.QueryHeight(zero);
                        if (num10 >= (double)planet.radius &&
                            (eveinType != EVeinType.Oil || num10 >= planet.radius + 0.5) &&
                            (eveinType > EVeinType.Copper || num10 <= planet.radius + 0.699999988079071) &&
                            ((eveinType != EVeinType.Silicium && eveinType != EVeinType.Titanium) ||
                             num10 > planet.radius + 0.699999988079071))
                        {
                            var flag3 = false;
                            var num11 = eveinType == EVeinType.Oil ? 100f : 196f;
                            for (var index5 = 0; index5 < veinVectorCount; ++index5)
                                if ((veinVectors[index5] - zero).sqrMagnitude < num1 * (double)num1 * num11)
                                {
                                    flag3 = true;
                                    break;
                                }

                            if (!flag3)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }

                    if (flag2)
                    {
                        veinVectors[veinVectorCount] = zero;
                        veinVectorTypes[veinVectorCount] = eveinType;
                        ++veinVectorCount;
                        if (veinVectorCount == veinVectors.Length)
                            break;
                    }
                }
            }

            data.veinCursor = 1;
            tmp_vecs.Clear();
            var vein = new VeinData();
            for (var index6 = 0; index6 < veinVectorCount; ++index6)
            {
                tmp_vecs.Clear();
                var normalized = veinVectors[index6].normalized;
                var veinVectorType = veinVectorTypes[index6];
                var index7 = (int)veinVectorType;
                var rotation = Quaternion.FromToRotation(Vector3.up, normalized);
                var vector3_1 = rotation * Vector3.right;
                var vector3_2 = rotation * Vector3.forward;
                tmp_vecs.Add(Vector2.zero);
                var num12 = Mathf.RoundToInt(destinationArray2[index7] * dotNet35Random2.Next(20, 25));
                if (veinVectorType == EVeinType.Oil)
                    num12 = 1;
                var num13 = destinationArray3[index7];
                if (flag1 && index6 < 2)
                {
                    num12 = 6;
                    num13 = 0.2f;
                }

                var num14 = 0;
                while (num14++ < 20)
                {
                    var count = tmp_vecs.Count;
                    for (var index8 = 0; index8 < count && tmp_vecs.Count < num12; ++index8)
                    {
                        var vector2_1 = tmp_vecs[index8];
                        if (vector2_1.sqrMagnitude <= 36.0)
                        {
                            var num15 = dotNet35Random2.NextDouble() * Math.PI * 2.0;
                            var vector2_2 = new Vector2((float)Math.Cos(num15), (float)Math.Sin(num15));
                            vector2_2 += tmp_vecs[index8] * 0.2f;
                            vector2_2.Normalize();
                            var vector2_3 = tmp_vecs[index8] + vector2_2;
                            var flag4 = false;
                            for (var index9 = 0; index9 < tmp_vecs.Count; ++index9)
                            {
                                vector2_1 = tmp_vecs[index9] - vector2_3;
                                if (vector2_1.sqrMagnitude < 0.85000002384185791)
                                {
                                    flag4 = true;
                                    break;
                                }
                            }

                            if (!flag4)
                                tmp_vecs.Add(vector2_3);
                        }
                    }

                    if (tmp_vecs.Count >= num12)
                        break;
                }

                var num16 = f;
                if (veinVectorType == EVeinType.Oil)
                    num16 = Mathf.Pow(f, 0.5f);
                var num17 = Mathf.RoundToInt(num13 * 100000f * num16);
                if (num17 < 20)
                    num17 = 20;
                var num18 = num17 < 16000 ? Mathf.FloorToInt(num17 * (15f / 16f)) : 15000;
                var minValue = num17 - num18;
                var maxValue = num17 + num18 + 1;
                for (var index10 = 0; index10 < tmp_vecs.Count; ++index10)
                {
                    var vector3_3 = (tmp_vecs[index10].x * vector3_1 + tmp_vecs[index10].y * vector3_2) * num1;
                    vein.type = veinVectorType;
                    vein.groupIndex = (short)(index6 + 1);
                    vein.modelIndex = (short)dotNet35Random2.Next(veinModelIndexs[index7],
                        veinModelIndexs[index7] + veinModelCounts[index7]);
                    vein.amount = Mathf.RoundToInt(dotNet35Random2.Next(minValue, maxValue) * num7);
                    if (vein.amount < 1)
                        vein.amount = 1;
                    vein.productId = veinProducts[index7];
                    vein.pos = normalized + vector3_3;
                    vein.minerCount = 0;
                    var num19 = data.QueryHeight(vein.pos);
                    vein.pos = vein.pos.normalized * num19;
                    if (planet.waterItemId == 0 || num19 >= (double)planet.radius)
                        data.AddVeinData(vein);
                }
            }

            tmp_vecs.Clear();
        }
    }
}