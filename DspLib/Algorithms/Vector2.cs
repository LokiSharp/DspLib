﻿namespace DspLib.Algorithms;

[Serializable]
public struct Vector2 : IEquatable<Vector2>
{
    public float x;

    public float y;

    public const float kEpsilon = 1E-05f;

    public const float kEpsilonNormalSqrt = 1E-15f;

    public float this[int index]
    {
        get
        {
            return index switch
            {
                0 => x,
                1 => y,
                _ => throw new IndexOutOfRangeException("Invalid Vector2 index!")
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
                default:
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
            }
        }
    }

    public Vector2 normalized
    {
        get
        {
            var result = new Vector2(x, y);
            result.Normalize();
            return result;
        }
    }

    public float magnitude => Mathf.Sqrt(x * x + y * y);

    public float sqrMagnitude => x * x + y * y;

    public static Vector2 zero { get; } = new(0f, 0f);

    public static Vector2 one { get; } = new(1f, 1f);

    public static Vector2 up { get; } = new(0f, 1f);

    public static Vector2 down { get; } = new(0f, -1f);

    public static Vector2 left { get; } = new(-1f, 0f);

    public static Vector2 right { get; } = new(1f, 0f);

    public static Vector2 positiveInfinity { get; } = new(float.PositiveInfinity, float.PositiveInfinity);

    public static Vector2 negativeInfinity { get; } = new(float.NegativeInfinity, float.NegativeInfinity);

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public void Set(float newX, float newY)
    {
        x = newX;
        y = newY;
    }

    public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        t = Mathf.Clamp01(t);
        return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
    }

    public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
    {
        return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
    }

    public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
    {
        var vector = target - current;
        var num = vector.magnitude;
        if (num <= maxDistanceDelta || num == 0f) return target;

        return current + vector / num * maxDistanceDelta;
    }

    public static Vector2 Scale(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public void Scale(Vector2 scale)
    {
        x *= scale.x;
        y *= scale.y;
    }

    public void Normalize()
    {
        var num = magnitude;
        if (num > 1E-05f)
            this /= num;
        else
            this = zero;
    }

    public override string ToString()
    {
        return string.Format("({0:F1}, {1:F1})", new object[2] { x, y });
    }

    public string ToString(string format)
    {
        return string.Format("({0}, {1})", new object[2]
        {
            x.ToString(format),
            y.ToString(format)
        });
    }

    public override int GetHashCode()
    {
        var x_hash = x.GetHashCode();
        var y_hash = y.GetHashCode();

        return x_hash ^ (y_hash << 2);
    }

    public override bool Equals(object other)
    {
        if (!(other is Vector2)) return false;

        return Equals((Vector2)other);
    }

    public bool Equals(Vector2 other)
    {
        return x.Equals(other.x) && y.Equals(other.y);
    }

    public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
    {
        return -2f * Dot(inNormal, inDirection) * inNormal + inDirection;
    }

    public static Vector2 Perpendicular(Vector2 inDirection)
    {
        return new Vector2(0f - inDirection.y, inDirection.x);
    }

    public static float Dot(Vector2 lhs, Vector2 rhs)
    {
        return lhs.x * rhs.x + lhs.y * rhs.y;
    }

    public static float Angle(Vector2 from, Vector2 to)
    {
        var num = Mathf.Sqrt(from.sqrMagnitude * to.sqrMagnitude);
        if (num < 1E-15f) return 0f;

        var f = Mathf.Clamp(Dot(from, to) / num, -1f, 1f);
        return Mathf.Acos(f) * 57.29578f;
    }

    public static float SignedAngle(Vector2 from, Vector2 to)
    {
        var num = Angle(from, to);
        var num2 = Mathf.Sign(from.x * to.y - from.y * to.x);
        return num * num2;
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        return (a - b).magnitude;
    }

    public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
    {
        if (vector.sqrMagnitude > maxLength * maxLength) return vector.normalized * maxLength;

        return vector;
    }

    public static float SqrMagnitude(Vector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public float SqrMagnitude()
    {
        return x * x + y * y;
    }

    public static Vector2 Min(Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
    }

    public static Vector2 Max(Vector2 lhs, Vector2 rhs)
    {
        return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
    }

    public static Vector2 operator +(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x + b.x, a.y + b.y);
    }

    public static Vector2 operator -(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x - b.x, a.y - b.y);
    }

    public static Vector2 operator *(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x * b.x, a.y * b.y);
    }

    public static Vector2 operator /(Vector2 a, Vector2 b)
    {
        return new Vector2(a.x / b.x, a.y / b.y);
    }

    public static Vector2 operator -(Vector2 a)
    {
        return new Vector2(0f - a.x, 0f - a.y);
    }

    public static Vector2 operator *(Vector2 a, float d)
    {
        return new Vector2(a.x * d, a.y * d);
    }

    public static Vector2 operator *(float d, Vector2 a)
    {
        return new Vector2(a.x * d, a.y * d);
    }

    public static Vector2 operator /(Vector2 a, float d)
    {
        return new Vector2(a.x / d, a.y / d);
    }

    public static bool operator ==(Vector2 lhs, Vector2 rhs)
    {
        return (lhs - rhs).sqrMagnitude < 9.9999994E-11f;
    }

    public static bool operator !=(Vector2 lhs, Vector2 rhs)
    {
        return !(lhs == rhs);
    }

    public static implicit operator Vector2(Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static implicit operator Vector3(Vector2 v)
    {
        return new Vector3(v.x, v.y, 0f);
    }
}