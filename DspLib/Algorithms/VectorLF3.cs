namespace DspLib.Algorithms;

[Serializable]
public struct VectorLF3
{
    public double x;
    public double y;
    public double z;

    public static VectorLF3 zero => new(0.0f, 0.0f, 0.0f);

    public static VectorLF3 one => new(1f, 1f, 1f);

    public static VectorLF3 minusone => new(-1f, -1f, -1f);

    public static VectorLF3 unit_x => new(1f, 0.0f, 0.0f);

    public static VectorLF3 unit_y => new(0.0f, 1f, 0.0f);

    public static VectorLF3 unit_z => new(0.0f, 0.0f, 1f);

    public VectorLF2 xy => new(x, y);

    public VectorLF2 yx => new(y, x);

    public VectorLF2 zx => new(z, x);

    public VectorLF2 xz => new(x, z);

    public VectorLF2 yz => new(y, z);

    public VectorLF2 zy => new(z, y);

    public VectorLF3(VectorLF3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
    }

    public VectorLF3(double x_, double y_, double z_)
    {
        x = x_;
        y = y_;
        z = z_;
    }

    public VectorLF3(float x_, float y_, float z_)
    {
        x = x_;
        y = y_;
        z = z_;
    }

    public static bool operator ==(VectorLF3 lhs, VectorLF3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static bool operator !=(VectorLF3 lhs, VectorLF3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static VectorLF3 operator *(VectorLF3 lhs, VectorLF3 rhs)
    {
        return new VectorLF3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
    }

    public static VectorLF3 operator *(VectorLF3 lhs, double rhs)
    {
        return new VectorLF3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
    }

    public static VectorLF3 operator /(VectorLF3 lhs, double rhs)
    {
        return new VectorLF3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
    }

    public static VectorLF3 operator -(VectorLF3 vec)
    {
        return new VectorLF3(-vec.x, -vec.y, -vec.z);
    }

    public static VectorLF3 operator -(VectorLF3 lhs, VectorLF3 rhs)
    {
        return new VectorLF3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
    }

    public static VectorLF3 operator +(VectorLF3 lhs, VectorLF3 rhs)
    {
        return new VectorLF3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
    }

    public static implicit operator VectorLF3(Vector3 vec3)
    {
        return new VectorLF3(vec3.x, vec3.y, vec3.z);
    }

    public static implicit operator Vector3(VectorLF3 vec3)
    {
        return new Vector3((float)vec3.x, (float)vec3.y, (float)vec3.z);
    }

    public double sqrMagnitude => x * x + y * y + z * z;

    public double magnitude => Math.Sqrt(x * x + y * y + z * z);

    public double Distance(VectorLF3 vec)
    {
        return Math.Sqrt((vec.x - x) * (vec.x - x) + (vec.y - y) * (vec.y - y) + (vec.z - z) * (vec.z - z));
    }

    public override bool Equals(object obj)
    {
        return obj != null && obj is VectorLF3 vectorLf3 && x == vectorLf3.x && y == vectorLf3.y && z == vectorLf3.z;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("[{0},{1},{2}]", x, y, z);
    }

    public static double Dot(VectorLF3 a, VectorLF3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }

    public static VectorLF3 Cross(VectorLF3 a, VectorLF3 b)
    {
        return new VectorLF3(a.y * b.z - b.y * a.z, a.z * b.x - b.z * a.x, a.x * b.y - b.x * a.y);
    }

    public static double AngleRAD(VectorLF3 a, VectorLF3 b)
    {
        var normalized1 = a.normalized;
        var normalized2 = b.normalized;
        var d = normalized1.x * normalized2.x + normalized1.y * normalized2.y + normalized1.z * normalized2.z;
        if (d > 1.0)
            d = 1.0;
        else if (d < -1.0)
            d = -1.0;
        return Math.Acos(d);
    }

    public static double AngleDEG(VectorLF3 a, VectorLF3 b)
    {
        var normalized1 = a.normalized;
        var normalized2 = b.normalized;
        var d = normalized1.x * normalized2.x + normalized1.y * normalized2.y + normalized1.z * normalized2.z;
        if (d > 1.0)
            d = 1.0;
        else if (d < -1.0)
            d = -1.0;
        return Math.Acos(d) / Math.PI * 180.0;
    }

    public static VectorLF3 MoveTowards(VectorLF3 current, VectorLF3 target, double maxDistanceDelta)
    {
        var vectorLf3 = target - current;
        var magnitude = vectorLf3.magnitude;
        return magnitude <= maxDistanceDelta || magnitude == 0.0
            ? target
            : current + vectorLf3 / magnitude * maxDistanceDelta;
    }

    public VectorLF3 normalized
    {
        get
        {
            var d = x * x + y * y + z * z;
            if (d < 1E-34)
                return new VectorLF3(0.0f, 0.0f, 0.0f);
            var num = Math.Sqrt(d);
            return new VectorLF3(x / num, y / num, z / num);
        }
    }
}