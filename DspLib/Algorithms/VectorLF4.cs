namespace DspLib.Algorithms;

[Serializable]
public struct VectorLF4
{
    public double x;
    public double y;
    public double z;
    public double w;

    public static VectorLF4 zero => new(0.0, 0.0, 0.0, 0.0);

    public static VectorLF4 one => new(1.0, 1.0, 1.0, 1.0);

    public static VectorLF4 minusone => new(-1.0, -1.0, -1.0, -1.0);

    public static VectorLF4 unit_x => new(1.0, 0.0, 0.0, 0.0);

    public static VectorLF4 unit_y => new(0.0, 1.0, 0.0, 0.0);

    public static VectorLF4 unit_z => new(0.0, 0.0, 1.0, 0.0);

    public static VectorLF4 unit_w => new(0.0, 0.0, 0.0, 1.0);

    public VectorLF4(double x_, double y_, double z_, double w_)
    {
        x = x_;
        y = y_;
        z = z_;
        w = w_;
    }

    public VectorLF4(VectorLF3 v3, double w_)
    {
        x = v3.x;
        y = v3.y;
        z = v3.z;
        w = w_;
    }

    public VectorLF3 xyz => new(x, y, z);

    public Vector3 xyzf => new((float)x, (float)y, (float)z);

    public static bool operator ==(VectorLF4 lhs, VectorLF4 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
    }

    public static bool operator !=(VectorLF4 lhs, VectorLF4 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z || lhs.w != rhs.w;
    }

    public static VectorLF4 operator *(VectorLF4 lhs, VectorLF4 rhs)
    {
        return new VectorLF4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
    }

    public static VectorLF4 operator *(VectorLF4 lhs, double rhs)
    {
        return new VectorLF4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
    }

    public static VectorLF4 operator /(VectorLF4 lhs, double rhs)
    {
        return new VectorLF4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
    }

    public static VectorLF4 operator -(VectorLF4 vec)
    {
        return new VectorLF4(-vec.x, -vec.y, -vec.z, -vec.w);
    }

    public static VectorLF4 operator -(VectorLF4 lhs, VectorLF4 rhs)
    {
        return new VectorLF4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
    }

    public static VectorLF4 operator +(VectorLF4 lhs, VectorLF4 rhs)
    {
        return new VectorLF4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
    }

    public static implicit operator VectorLF4(Vector4 vec4)
    {
        return new VectorLF4(vec4.x, vec4.y, vec4.z, vec4.w);
    }

    public static implicit operator Vector4(VectorLF4 vec4)
    {
        return new Vector4((float)vec4.x, (float)vec4.y, (float)vec4.z, (float)vec4.w);
    }

    public double sqrMagnitude => x * x + y * y + z * z + w * w;

    public double magnitude => Math.Sqrt(x * x + y * y + z * z + w * w);

    public override bool Equals(object obj)
    {
        return obj != null && obj is VectorLF4 vectorLf4 && x == vectorLf4.x && y == vectorLf4.y && z == vectorLf4.z &&
               w == vectorLf4.w;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("[{0},{1},{2},{3}]", x, y, z, w);
    }
}