using DspLib.Algorithms;

public class AstroOrbitData
{
    public double orbitalPeriod = 3600.0;
    public float orbitInclination;
    public float orbitLongitude;
    public VectorLF3 orbitNormal;
    public float orbitPhase;
    public float orbitRadius = 1f;
    public Quaternion orbitRotation;
    public float runtimeOrbitPhase;

    public VectorLF3 GetVelocityAtPoint(VectorLF3 center, VectorLF3 upos)
    {
        VectorLF3 v;
        v.x = upos.x - center.x;
        v.y = upos.y - center.y;
        v.z = upos.z - center.z;
        return Maths.QRotateLF(Quaternion.AngleAxis(-360f / (float)(orbitalPeriod * 60.0), orbitNormal), v) - v;
    }

    public float GetEstimatePointOffset(float eta)
    {
        var num = Math.PI / 180.0 * eta * 360.0 / orbitalPeriod;
        return (float)(orbitRadius * 40000.0 * num * num * 0.5);
    }
}