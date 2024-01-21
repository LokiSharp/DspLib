namespace DspLib.Algorithms;

public struct Quaternion : IEquatable<Quaternion>
{
    public float x;

    public float y;

    public float z;

    public float w;

    public const float kEpsilon = 1E-06f;

    public Quaternion(float x, float y, float z, float w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => x,
                1 => y,
                2 => z,
                3 => w,
                _ => throw new IndexOutOfRangeException("Invalid Quaternion index!")
            };
        }
        set
        {
            switch (index)
            {
                case 0:
                    x = value;
                    break;
                case 1:
                    y = value;
                    break;
                case 2:
                    z = value;
                    break;
                case 3:
                    w = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Invalid Quaternion index!");
            }
        }
    }

    public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
    {
        fromDirection = Vector3.Normalize(fromDirection);
        toDirection = Vector3.Normalize(toDirection);
        var axis = Vector3.Normalize(Vector3.Cross(fromDirection, toDirection));
        return AngleAxis(Mathf.Acos(Vector3.Dot(fromDirection, toDirection)), axis);
    }

    public static Quaternion Inverse(Quaternion rotation)
    {
        var tmp = System.Numerics.Quaternion.Inverse(new System.Numerics.Quaternion(rotation.x, rotation.y, rotation.z,
            rotation.w));
        return new Quaternion(tmp.X, tmp.Y, tmp.Z, tmp.W);
    }

    public static Quaternion Slerp(Quaternion a, Quaternion b, float t)
    {
        var tmp_a = new System.Numerics.Quaternion(a.x, a.y, a.z, a.w);
        var tmp_b = new System.Numerics.Quaternion(a.x, a.y, a.z, a.w);
        var tmp = System.Numerics.Quaternion.Slerp(tmp_a, tmp_b, t);
        return new Quaternion(tmp.X, tmp.Y, tmp.Z, tmp.W);
    }

    public static Quaternion AngleAxis(float angle, Vector3 axis)
    {
        var tmp = System.Numerics.Quaternion.CreateFromAxisAngle(new System.Numerics.Vector3(axis.x, axis.y, axis.z),
            angle);
        return new Quaternion(tmp.X, tmp.Y, tmp.Z, tmp.W);
    }

    public static Quaternion identity { get; } = new(0f, 0f, 0f, 1f);

    public Quaternion normalized => Normalize(this);


    public void Set(float newX, float newY, float newZ, float newW)
    {
        x = newX;
        y = newY;
        z = newZ;
        w = newW;
    }

    public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
    {
        return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
            lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
            lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
            lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
    }

    public static Vector3 operator *(Quaternion rotation, Vector3 point)
    {
        var num = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num;
        var num5 = rotation.y * num2;
        var num6 = rotation.z * num3;
        var num7 = rotation.x * num2;
        var num8 = rotation.x * num3;
        var num9 = rotation.y * num3;
        var num10 = rotation.w * num;
        var num11 = rotation.w * num2;
        var num12 = rotation.w * num3;
        var result = default(Vector3);
        result.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
        result.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
        result.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
        return result;
    }

    private static bool IsEqualUsingDot(float dot)
    {
        return dot > 0.999999f;
    }

    public static bool operator ==(Quaternion lhs, Quaternion rhs)
    {
        return IsEqualUsingDot(Dot(lhs, rhs));
    }

    public static bool operator !=(Quaternion lhs, Quaternion rhs)
    {
        return !(lhs == rhs);
    }

    public static float Dot(Quaternion a, Quaternion b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
    }

    public static float Angle(Quaternion a, Quaternion b)
    {
        var num = Dot(a, b);
        return !IsEqualUsingDot(num) ? Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2f * 57.29578f : 0f;
    }

    private static Vector3 Internal_MakePositive(Vector3 euler)
    {
        var num = -0.005729578f;
        var num2 = 360f + num;
        if (euler.x < num)
            euler.x += 360f;
        else if (euler.x > num2) euler.x -= 360f;

        if (euler.y < num)
            euler.y += 360f;
        else if (euler.y > num2) euler.y -= 360f;

        if (euler.z < num)
            euler.z += 360f;
        else if (euler.z > num2) euler.z -= 360f;

        return euler;
    }

    public static Quaternion Normalize(Quaternion q)
    {
        var num = Mathf.Sqrt(Dot(q, q));
        if (num < Mathf.Epsilon) return identity;

        return new Quaternion(q.x / num, q.y / num, q.z / num, q.w / num);
    }

    public void Normalize()
    {
        this = Normalize(this);
    }

    public override int GetHashCode()
    {
        return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
    }

    public override bool Equals(object other)
    {
        if (!(other is Quaternion)) return false;

        return Equals((Quaternion)other);
    }

    public bool Equals(Quaternion other)
    {
        return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z) && w.Equals(other.w);
    }

    public override string ToString()
    {
        return string.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", x, y, z, w);
    }

    public string ToString(string format)
    {
        return string.Format("({0}, {1}, {2}, {3})", x.ToString(format), y.ToString(format), z.ToString(format),
            w.ToString(format));
    }
}