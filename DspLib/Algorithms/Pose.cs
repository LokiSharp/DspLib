﻿namespace DspLib.Algorithms;

[Serializable]
public struct Pose : IEquatable<Pose>
{
    public Vector3 position;

    public Quaternion rotation;

    public Vector3 forward => rotation * Vector3.forward;

    public Vector3 right => rotation * Vector3.right;

    public Vector3 up => rotation * Vector3.up;

    public static Pose identity { get; } = new(Vector3.zero, Quaternion.identity);

    public Pose(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }

    public override string ToString()
    {
        return $"({position.ToString()}, {rotation.ToString()})";
    }

    public string ToString(string format)
    {
        return $"({position.ToString(format)}, {rotation.ToString(format)})";
    }

    public Pose GetTransformedBy(Pose lhs)
    {
        var result = default(Pose);
        result.position = lhs.position + lhs.rotation * position;
        result.rotation = lhs.rotation * rotation;
        return result;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Pose)) return false;

        return Equals((Pose)obj);
    }

    public bool Equals(Pose other)
    {
        return position == other.position && rotation == other.rotation;
    }

    public override int GetHashCode()
    {
        return position.GetHashCode() ^ (rotation.GetHashCode() << 1);
    }

    public static bool operator ==(Pose a, Pose b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Pose a, Pose b)
    {
        return !(a == b);
    }
}