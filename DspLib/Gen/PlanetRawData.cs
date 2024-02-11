using DspLib.Algorithms;

namespace DspLib.Gen;

public class PlanetRawData
{
    private static Vector3[] verts200;
    private static Vector3[] verts80;
    private static int[] indexMap200;
    private static int[] indexMap80;

    public static Vector3[] poles = new Vector3[6]
    {
        Vector3.right,
        Vector3.left,
        Vector3.up,
        Vector3.down,
        Vector3.forward,
        Vector3.back
    };

    public byte[] biomoData;
    public ushort[] heightData;
    public int[] indexMap;
    public int indexMapCornerStride;
    public int indexMapDataLength;
    public int indexMapFaceStride;
    public int indexMapPrecision;
    public byte[] modData;
    public Vector3[] normals;
    public int precision;
    public short[] temprData;
    private int vegeCapacity;
    public int vegeCursor = 1;
    public ushort[] vegeIds;
    private int veinCapacity;
    public int veinCursor = 1;
    public VeinData[] veinPool;
    public Vector3[] vertices;

    public PlanetRawData(int _precision)
    {
        precision = _precision;
        var dataLength = this.dataLength;
        heightData = new ushort[dataLength];
        vegeIds = new ushort[dataLength];
        biomoData = new byte[dataLength];
        temprData = new short[dataLength];
        vertices = new Vector3[dataLength];
        normals = new Vector3[dataLength];
        indexMapPrecision = precision >> 2;
        indexMapFaceStride = indexMapPrecision * indexMapPrecision;
        indexMapCornerStride = indexMapFaceStride * 3;
        indexMapDataLength = indexMapCornerStride * 8;
        indexMap = new int[indexMapDataLength];
        SetVeinCapacity(32);
    }

    public int dataLength => (precision + 1) * (precision + 1) * 4;

    public int stride => (precision + 1) * 2;

    public int substride => precision + 1;

    public byte[] InitModData(byte[] refModData)
    {
        modData = refModData == null ? new byte[dataLength / 2] : refModData;
        return modData;
    }

    public void Free()
    {
        precision = 0;
        heightData = null;
        biomoData = null;
        temprData = null;
        vertices = null;
        normals = null;
        veinPool = null;
    }

    public void CalcVerts()
    {
        if (precision == 200 && verts200 != null)
        {
            Array.Copy(verts200, vertices, verts200.Length);
            Array.Copy(indexMap200, indexMap, indexMap200.Length);
        }
        else if (precision == 80 && verts80 != null)
        {
            Array.Copy(verts80, vertices, verts80.Length);
            Array.Copy(indexMap80, indexMap, indexMap80.Length);
        }
        else
        {
            for (var index = 0; index < indexMapDataLength; ++index)
                indexMap[index] = -1;
            var num1 = (precision + 1) * 2;
            var num2 = precision + 1;
            for (var index1 = 0; index1 < dataLength; ++index1)
            {
                var num3 = index1 % num1;
                var num4 = index1 / num1;
                var num5 = num3 % num2;
                var num6 = num4 % num2;
                var num7 = ((num3 >= num2 ? 1 : 0) + (num4 >= num2 ? 1 : 0) * 2) * 2 + (num5 >= num6 ? 0 : 1);
                var num8 = num5 >= num6 ? precision - num5 : (float)num5;
                var num9 = num5 >= num6 ? num6 : (float)(precision - num6);
                var num10 = precision - num9;
                var t1 = num9 / precision;
                var t2 = num10 > 0.0 ? num8 / num10 : 0.0f;
                Vector3 pole1;
                Vector3 pole2;
                Vector3 pole3;
                int corner;
                switch (num7)
                {
                    case 0:
                        pole1 = poles[2];
                        pole2 = poles[0];
                        pole3 = poles[4];
                        corner = 7;
                        break;
                    case 1:
                        pole1 = poles[3];
                        pole2 = poles[4];
                        pole3 = poles[0];
                        corner = 5;
                        break;
                    case 2:
                        pole1 = poles[2];
                        pole2 = poles[4];
                        pole3 = poles[1];
                        corner = 6;
                        break;
                    case 3:
                        pole1 = poles[3];
                        pole2 = poles[1];
                        pole3 = poles[4];
                        corner = 4;
                        break;
                    case 4:
                        pole1 = poles[2];
                        pole2 = poles[1];
                        pole3 = poles[5];
                        corner = 2;
                        break;
                    case 5:
                        pole1 = poles[3];
                        pole2 = poles[5];
                        pole3 = poles[1];
                        corner = 0;
                        break;
                    case 6:
                        pole1 = poles[2];
                        pole2 = poles[5];
                        pole3 = poles[0];
                        corner = 3;
                        break;
                    case 7:
                        pole1 = poles[3];
                        pole2 = poles[0];
                        pole3 = poles[5];
                        corner = 1;
                        break;
                    default:
                        pole1 = poles[2];
                        pole2 = poles[0];
                        pole3 = poles[4];
                        corner = 7;
                        break;
                }

                vertices[index1] = Vector3.Slerp(Vector3.Slerp(pole1, pole3, t1), Vector3.Slerp(pole2, pole3, t1), t2);
                var index2 = PositionHash(vertices[index1], corner);
                if (indexMap[index2] == -1)
                    indexMap[index2] = index1;
            }

            var num11 = 0;
            for (var index = 1; index < indexMapDataLength; ++index)
                if (indexMap[index] == -1)
                {
                    indexMap[index] = indexMap[index - 1];
                    ++num11;
                }

            if (precision == 200)
            {
                if (verts200 != null)
                    return;
                verts200 = new Vector3[vertices.Length];
                indexMap200 = new int[indexMap.Length];
                Array.Copy(vertices, verts200, vertices.Length);
                Array.Copy(indexMap, indexMap200, indexMap.Length);
            }
            else
            {
                if (precision != 80 || verts80 != null)
                    return;
                verts80 = new Vector3[vertices.Length];
                indexMap80 = new int[indexMap.Length];
                Array.Copy(vertices, verts80, vertices.Length);
                Array.Copy(indexMap, indexMap80, indexMap.Length);
            }
        }
    }

    public int QueryIndex(Vector3 vpos)
    {
        vpos.Normalize();
        var index1 = indexMap[PositionHash(vpos)];
        var num1 = (float)(3.1415927410125732 / (precision * 2) * 0.25);
        var num2 = num1 * num1;
        var stride = this.stride;
        var num3 = 10f;
        var num4 = index1;
        for (var index2 = -1; index2 <= 3; ++index2)
        for (var index3 = -1; index3 <= 3; ++index3)
        {
            var index4 = index1 + index2 + index3 * stride;
            if ((uint)index4 < dataLength)
            {
                var sqrMagnitude = (vertices[index4] - vpos).sqrMagnitude;
                if (sqrMagnitude < (double)num2)
                    return index4;
                if (sqrMagnitude < (double)num3)
                {
                    num3 = sqrMagnitude;
                    num4 = index4;
                }
            }
        }

        return num4;
    }

    public int GetModLevel(int index)
    {
        return (modData[index >> 1] >> ((index & 1) << 2)) & 3;
    }

    public short GetModPlane(int index)
    {
        return (short)(((modData[index >> 1] >> (((index & 1) << 2) + 2)) & 3) * 133 + 20020);
    }

    public void SetModLevel(int index, int level)
    {
        var num1 = (index & 1) << 2;
        var num2 = ~(3 << num1);
        var num3 = (level & 3) << num1;
        modData[index >> 1] &= (byte)num2;
        modData[index >> 1] |= (byte)num3;
    }

    public void SetModPlane(int index, int plane)
    {
        if (plane > 3)
            plane = 3;
        else if (plane < 0)
            plane = 0;
        var num1 = ((index & 1) << 2) + 2;
        var num2 = ~(3 << num1);
        var num3 = (plane & 3) << num1;
        modData[index >> 1] &= (byte)num2;
        modData[index >> 1] |= (byte)num3;
    }

    public bool AddModLevel(int index, int level)
    {
        var num1 = (modData[index >> 1] >> ((index & 1) << 2)) & 3;
        level += num1;
        if (level > 3)
            level = 3;
        if (level == num1)
            return false;
        var num2 = (index & 1) << 2;
        var num3 = ~(3 << num2);
        var num4 = (level & 3) << num2;
        modData[index >> 1] &= (byte)num3;
        modData[index >> 1] |= (byte)num4;
        return true;
    }


    public float QueryHeight(Vector3 vpos)
    {
        vpos.Normalize();
        var index1 = indexMap[PositionHash(vpos)];
        var num1 = (float)(3.1415927410125732 / (precision * 2) * 1.2000000476837158);
        var num2 = num1 * num1;
        var num3 = 0.0f;
        var num4 = 0.0f;
        var stride = this.stride;
        for (var index2 = -1; index2 <= 3; ++index2)
        for (var index3 = -1; index3 <= 3; ++index3)
        {
            var index4 = index1 + index2 + index3 * stride;
            if ((uint)index4 < dataLength)
            {
                var sqrMagnitude = (vertices[index4] - vpos).sqrMagnitude;
                if (sqrMagnitude <= (double)num2)
                {
                    var num5 = (float)(1.0 - Mathf.Sqrt(sqrMagnitude) / (double)num1);
                    float num6 = heightData[index4];
                    num3 += num5;
                    num4 += num6 * num5;
                }
            }
        }

        if (num3 != 0.0)
            return (float)(num4 / (double)num3 * 0.0099999997764825821);
        return heightData[0] * 0.01f;
    }

    public float QueryModifiedHeight(Vector3 vpos)
    {
        vpos.Normalize();
        var index1 = indexMap[PositionHash(vpos)];
        var num1 = (float)(3.1415927410125732 / (precision * 2) * 1.2000000476837158);
        var num2 = num1 * num1;
        var num3 = 0.0f;
        var num4 = 0.0f;
        var stride = this.stride;
        for (var index2 = -1; index2 <= 3; ++index2)
        for (var index3 = -1; index3 <= 3; ++index3)
        {
            var index4 = index1 + index2 + index3 * stride;
            if ((uint)index4 < dataLength)
            {
                var sqrMagnitude = (vertices[index4] - vpos).sqrMagnitude;
                if (sqrMagnitude <= (double)num2)
                {
                    var num5 = (float)(1.0 - Mathf.Sqrt(sqrMagnitude) / (double)num1);
                    var modLevel = GetModLevel(index4);
                    float num6 = heightData[index4];
                    if (modLevel > 0)
                    {
                        float modPlane = GetModPlane(index4);
                        if (modLevel == 3)
                        {
                            num6 = modPlane;
                        }
                        else
                        {
                            var num7 = modLevel * 0.3333333f;
                            num6 = (float)(heightData[index4] * (1.0 - num7) + modPlane * (double)num7);
                        }
                    }

                    num3 += num5;
                    num4 += num6 * num5;
                }
            }
        }

        if (num3 != 0.0)
            return (float)(num4 / (double)num3 * 0.0099999997764825821);
        return heightData[0] * 0.01f;
    }

    private void SetVeinCapacity(int newCapacity)
    {
        var veinPool = this.veinPool;
        this.veinPool = new VeinData[newCapacity];
        if (veinPool != null)
            Array.Copy(veinPool, this.veinPool, newCapacity > veinCapacity ? veinCapacity : newCapacity);
        veinCapacity = newCapacity;
    }

    public int AddVeinData(VeinData vein)
    {
        vein.id = veinCursor++;
        if (vein.id == veinCapacity)
            SetVeinCapacity(veinCapacity * 2);
        veinPool[vein.id] = vein;
        return vein.id;
    }


    private int trans(float x, int pr)
    {
        var num = (int)((Mathf.Sqrt(x + 0.23f) - 0.479583203792572) / 0.62947052717208862 * pr);
        if (num >= pr)
            num = pr - 1;
        return num;
    }

    public int PositionHash(Vector3 v, int corner = 0)
    {
        if (corner == 0)
            corner = (v.x > 0.0 ? 1 : 0) + (v.y > 0.0 ? 2 : 0) + (v.z > 0.0 ? 4 : 0);
        if (v.x < 0.0)
            v.x = -v.x;
        if (v.y < 0.0)
            v.y = -v.y;
        if (v.z < 0.0)
            v.z = -v.z;
        if (v.x < 1E-06 && v.y < 1E-06 && v.z < 1E-06)
            return 0;
        int num1;
        int num2;
        int num3;
        if (v.x >= (double)v.y && v.x >= (double)v.z)
        {
            num1 = 0;
            num2 = trans(v.z / v.x, indexMapPrecision);
            num3 = trans(v.y / v.x, indexMapPrecision);
        }
        else if (v.y >= (double)v.x && v.y >= (double)v.z)
        {
            num1 = 1;
            num2 = trans(v.x / v.y, indexMapPrecision);
            num3 = trans(v.z / v.y, indexMapPrecision);
        }
        else
        {
            num1 = 2;
            num2 = trans(v.x / v.z, indexMapPrecision);
            num3 = trans(v.y / v.z, indexMapPrecision);
        }

        return num2 + num3 * indexMapPrecision + num1 * indexMapFaceStride + corner * indexMapCornerStride;
    }
}