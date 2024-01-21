namespace DspLib.Algorithms;

[Serializable]
public struct VectorLF2
{
    public double x;
    public double y;

    public static VectorLF2 zero => new(0.0f, 0.0f);

    public static VectorLF2 one => new(1f, 1f);

    public static VectorLF2 minusone => new(-1f, -1f);

    public static VectorLF2 unit_x => new(1f, 0.0f);

    public static VectorLF2 unit_y => new(0.0f, 1f);

    public VectorLF2(VectorLF2 vec)
    {
        x = vec.x;
        y = vec.y;
    }

    public VectorLF2(double x_, double y_)
    {
        x = x_;
        y = y_;
    }

    public VectorLF2(float x_, float y_)
    {
        x = x_;
        y = y_;
    }

    public static bool operator ==(VectorLF2 lhs, VectorLF2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(VectorLF2 lhs, VectorLF2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public static VectorLF2 operator *(VectorLF2 lhs, VectorLF2 rhs)
    {
        return new VectorLF2(lhs.x * rhs.x, lhs.y * rhs.y);
    }

    public static VectorLF2 operator *(VectorLF2 lhs, double rhs)
    {
        return new VectorLF2(lhs.x * rhs, lhs.y * rhs);
    }

    public static VectorLF2 operator /(VectorLF2 lhs, double rhs)
    {
        return new VectorLF2(lhs.x / rhs, lhs.y / rhs);
    }

    public static VectorLF2 operator -(VectorLF2 vec)
    {
        return new VectorLF2(-vec.x, -vec.y);
    }

    public static VectorLF2 operator -(VectorLF2 lhs, VectorLF2 rhs)
    {
        return new VectorLF2(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static VectorLF2 operator +(VectorLF2 lhs, VectorLF2 rhs)
    {
        return new VectorLF2(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static implicit operator VectorLF2(Vector2 vec2)
    {
        return new VectorLF2(vec2.x, vec2.y);
    }

    public static implicit operator Vector2(VectorLF2 vec2)
    {
        return new Vector2((float)vec2.x, (float)vec2.y);
    }

    public double sqrMagnitude => x * x + y * y;

    public double magnitude => Math.Sqrt(x * x + y * y);

    public double Distance(VectorLF2 vec)
    {
        return Math.Sqrt((vec.x - x) * (vec.x - x) + (vec.y - y) * (vec.y - y));
    }

    public override bool Equals(object obj)
    {
        return obj != null && obj is VectorLF2 vectorLf2 && x == vectorLf2.x && y == vectorLf2.y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("[{0},{1}]", x, y);
    }

    public VectorLF2 normalized
    {
        get
        {
            var d = x * x + y * y;
            if (d < 1E-34)
                return new VectorLF2(0.0f, 0.0f);
            var num = Math.Sqrt(d);
            return new VectorLF2(x / num, y / num);
        }
    }
}