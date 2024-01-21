using DspLib.Algorithms;

public static class Maths
{
    public static double Clamp01(double val)
    {
        if (val < 0.0)
            return 0.0;
        return val > 1.0 ? 1.0 : val;
    }

    public static double Clamp(double val, double min, double max)
    {
        if (val < min)
            return min;
        return val > max ? max : val;
    }

    public static double Repeat(double x, double t)
    {
        if (t == 0.0)
            return 0.0;
        var d = x / t;
        return (d - Math.Floor(d)) * t;
    }

    public static float VectorSqrDistance(ref Vector3 a, ref Vector3 b)
    {
        var num1 = a.x - (double)b.x;
        var num2 = a.y - b.y;
        var num3 = a.z - b.z;
        return (float)(num1 * num1 + num2 * (double)num2 + num3 * (double)num3);
    }

    public static double VectorSqrDistance(ref VectorLF3 a, ref VectorLF3 b)
    {
        var num1 = a.x - b.x;
        var num2 = a.y - b.y;
        var num3 = a.z - b.z;
        return num1 * num1 + num2 * num2 + num3 * num3;
    }

    public static Vector3 Forward(this Quaternion rotation)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.y * num2;
        var num6 = rotation.x * (double)num3;
        var num7 = rotation.y * num3;
        var num8 = rotation.w * num1;
        double num9 = rotation.w * num2;
        return new Vector3((float)(num6 + num9), num7 - num8, (float)(1.0 - (num4 + (double)num5)));
    }

    public static Vector3 Up(this Quaternion rotation)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.z * num3;
        var num6 = rotation.x * (double)num2;
        var num7 = rotation.y * num3;
        var num8 = rotation.w * num1;
        double num9 = rotation.w * num3;
        return new Vector3((float)(num6 - num9), (float)(1.0 - (num4 + (double)num5)), num7 + num8);
    }

    public static void ForwardUp(this Quaternion rotation, out Vector3 fwd, out Vector3 up)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.y * num2;
        var num6 = rotation.z * num3;
        var num7 = rotation.x * num2;
        var num8 = rotation.x * num3;
        var num9 = rotation.y * num3;
        var num10 = rotation.w * num1;
        var num11 = rotation.w * num2;
        var num12 = rotation.w * num3;
        fwd = new Vector3(num8 + num11, num9 - num10, (float)(1.0 - (num4 + (double)num5)));
        up = new Vector3(num7 - num12, (float)(1.0 - (num4 + (double)num6)), num9 + num10);
    }

    public static Vector3 Right(this Quaternion rotation)
    {
        var num1 = rotation.y * 2f;
        var num2 = rotation.z * 2f;
        var num3 = rotation.y * num1;
        var num4 = rotation.z * num2;
        var num5 = rotation.x * num1;
        var num6 = rotation.x * num2;
        var num7 = rotation.w * num1;
        var num8 = rotation.w * num2;
        return new Vector3((float)(1.0 - (num3 + (double)num4)), num5 + num8, num6 - num7);
    }

    public static void ForwardRight(this Quaternion rotation, out Vector3 fwd, out Vector3 right)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.y * num2;
        var num6 = rotation.z * num3;
        var num7 = rotation.x * num2;
        var num8 = rotation.x * num3;
        var num9 = rotation.y * num3;
        var num10 = rotation.w * num1;
        var num11 = rotation.w * num2;
        var num12 = rotation.w * num3;
        fwd = new Vector3(num7 - num12, (float)(1.0 - (num4 + (double)num6)), num9 + num10);
        right = new Vector3((float)(1.0 - (num5 + (double)num6)), num7 + num12, num8 - num11);
    }

    public static Vector3 Back(this Quaternion rotation)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.y * num2;
        var num6 = rotation.x * (double)num3;
        var num7 = rotation.y * num3;
        var num8 = rotation.w * num1;
        var num9 = rotation.w * num2;
        return new Vector3((float)-num6 - num9, num8 - num7, (float)(num4 + (double)num5 - 1.0));
    }

    public static Vector3 Down(this Quaternion rotation)
    {
        var num1 = rotation.x * 2f;
        var num2 = rotation.y * 2f;
        var num3 = rotation.z * 2f;
        var num4 = rotation.x * num1;
        var num5 = rotation.z * num3;
        var num6 = rotation.x * num2;
        var num7 = rotation.y * num3;
        var num8 = rotation.w * num1;
        return new Vector3(rotation.w * num3 - num6, (float)(num4 + (double)num5 - 1.0), -num7 - num8);
    }

    public static Vector3 Left(this Quaternion rotation)
    {
        var num1 = rotation.y * 2f;
        var num2 = rotation.z * 2f;
        var num3 = rotation.y * (double)num1;
        var num4 = rotation.z * num2;
        var num5 = rotation.x * num1;
        var num6 = rotation.x * num2;
        var num7 = rotation.w * num1;
        var num8 = rotation.w * num2;
        double num9 = num4;
        return new Vector3((float)(num3 + num9 - 1.0), -num5 - num8, num7 - num6);
    }

    public static Vector4 ConvertToVec4(this Quaternion q)
    {
        return new Vector4(q.x, q.y, q.z, q.w);
    }

    public static void EulerTo3Axis(
        this Vector3 euler,
        out Vector3 right,
        out Vector3 up,
        out Vector3 forward)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        var num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        var num5 = Mathf.Sin(euler.z * ((float)Math.PI / 180f));
        var num6 = Mathf.Cos(euler.z * ((float)Math.PI / 180f));
        right = new Vector3((float)(num6 * (double)num4 + num5 * (double)num1 * num3), num2 * num5,
            (float)(num4 * (double)num5 * num1 - num6 * (double)num3));
        up = new Vector3((float)(num6 * (double)num1 * num3 - num4 * (double)num5), num6 * num2,
            (float)(num5 * (double)num3 + num6 * (double)num4 * num1));
        forward = new Vector3(num3 * num2, -num1, num4 * num2);
    }

    public static Vector3 StandardEuler(this Vector3 euler)
    {
        euler.x = Mathf.Repeat(euler.x + 180f, 360f) - 180f;
        if (euler.x < -90.0)
        {
            euler.x = -180f - euler.x;
            euler.y += 180f;
        }
        else if (euler.x > 90.0)
        {
            euler.x = 180f - euler.x;
            euler.y += 180f;
        }

        euler.y = Mathf.Repeat(euler.y + 180f, 360f) - 180f;
        euler.z = Mathf.Repeat(euler.z + 180f, 360f) - 180f;
        return euler;
    }

    public static Vector3 EulerForward(this Vector3 euler)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        double num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        double num5 = num2;
        return new Vector3((float)(num3 * num5), -num1, num4 * num2);
    }

    public static Vector3 EulerUp(this Vector3 euler)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        var num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        var num5 = Mathf.Sin(euler.z * ((float)Math.PI / 180f));
        var num6 = Mathf.Cos(euler.z * ((float)Math.PI / 180f));
        return new Vector3((float)(num6 * (double)num1 * num3 - num4 * (double)num5), num6 * num2,
            (float)(num5 * (double)num3 + num6 * (double)num4 * num1));
    }

    public static Vector3 EulerRight(this Vector3 euler)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        var num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        var num5 = Mathf.Sin(euler.z * ((float)Math.PI / 180f));
        var num6 = Mathf.Cos(euler.z * ((float)Math.PI / 180f));
        return new Vector3((float)(num6 * (double)num4 + num5 * (double)num1 * num3), num2 * num5,
            (float)(num4 * (double)num5 * num1 - num6 * (double)num3));
    }

    public static Vector3 EulerBack(this Vector3 euler)
    {
        var y = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num1 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        double num2 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num3 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        return new Vector3((float)-num2 * num1, y, -num3 * num1);
    }

    public static Vector3 EulerDown(this Vector3 euler)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        var num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        var num5 = Mathf.Sin(euler.z * ((float)Math.PI / 180f));
        var num6 = Mathf.Cos(euler.z * ((float)Math.PI / 180f));
        return new Vector3((float)(num4 * (double)num5 - num6 * (double)num1 * num3), -num6 * num2,
            (float)(-(double)num5 * num3 - num6 * (double)num4 * num1));
    }

    public static Vector3 EulerLeft(this Vector3 euler)
    {
        var num1 = Mathf.Sin(euler.x * ((float)Math.PI / 180f));
        var num2 = Mathf.Cos(euler.x * ((float)Math.PI / 180f));
        var num3 = Mathf.Sin(euler.y * ((float)Math.PI / 180f));
        var num4 = Mathf.Cos(euler.y * ((float)Math.PI / 180f));
        var num5 = Mathf.Sin(euler.z * ((float)Math.PI / 180f));
        var num6 = Mathf.Cos(euler.z * ((float)Math.PI / 180f));
        return new Vector3((float)(-(double)num6 * num4 - num5 * (double)num1 * num3), -num2 * num5,
            (float)(num6 * (double)num3 - num4 * (double)num5 * num1));
    }

    public static Vector3 HorzVector(Vector3 v, Vector3 n)
    {
        var num = (float)(v.x * (double)n.x + v.y * (double)n.y + v.z * (double)n.z);
        return new Vector3(v.x - n.x * num, v.y - n.y * num, v.z - n.z * num);
    }

    public static void HorzVertVector(Vector3 v, Vector3 n, out Vector3 horz, out Vector3 vert)
    {
        var num = (float)(v.x * (double)n.x + v.y * (double)n.y + v.z * (double)n.z);
        vert = new Vector3(n.x * num, n.y * num, n.z * num);
        horz = new Vector3(v.x - vert.x, v.y - vert.y, v.z - vert.z);
    }

    public static VectorLF3 HorzVector(VectorLF3 v, VectorLF3 n)
    {
        var num = v.x * n.x + v.y * n.y + v.z * n.z;
        return new VectorLF3(v.x - n.x * num, v.y - n.y * num, v.z - n.z * num);
    }

    public static void HorzVertVector(
        VectorLF3 v,
        VectorLF3 n,
        out VectorLF3 horz,
        out VectorLF3 vert)
    {
        var num = v.x * n.x + v.y * n.y + v.z * n.z;
        vert = new VectorLF3(n.x * num, n.y * num, n.z * num);
        horz = new VectorLF3(v.x - vert.x, v.y - vert.y, v.z - vert.z);
    }

    public static Vector3 QRotate(Quaternion q, Vector3 v)
    {
        v.x *= 2f;
        v.y *= 2f;
        v.z *= 2f;
        var num1 = (float)(q.w * (double)q.w - 0.5);
        var num2 = (float)(q.x * (double)v.x + q.y * (double)v.y + q.z * (double)v.z);
        return new Vector3(
            (float)(v.x * (double)num1 + (q.y * (double)v.z - q.z * (double)v.y) * q.w + q.x * (double)num2),
            (float)(v.y * (double)num1 + (q.z * (double)v.x - q.x * (double)v.z) * q.w + q.y * (double)num2),
            (float)(v.z * (double)num1 + (q.x * (double)v.y - q.y * (double)v.x) * q.w +
                    q.z * (double)num2));
    }

    public static void QRotate_ref(ref Quaternion q, ref Vector3 v, ref Vector3 result)
    {
        var num1 = 2f * v.x;
        var num2 = 2f * v.y;
        var num3 = 2f * v.z;
        var num4 = (float)(q.w * (double)q.w - 0.5);
        var num5 = (float)(q.x * (double)num1 + q.y * (double)num2 + q.z * (double)num3);
        result.x = (float)(num1 * (double)num4 + (q.y * (double)num3 - q.z * (double)num2) * q.w + q.x * (double)num5);
        result.y = (float)(num2 * (double)num4 + (q.z * (double)num1 - q.x * (double)num3) * q.w + q.y * (double)num5);
        result.z = (float)(num3 * (double)num4 + (q.x * (double)num2 - q.y * (double)num1) * q.w + q.z * (double)num5);
    }

    public static Vector3 QInvRotate(Quaternion q, Vector3 v)
    {
        v.x *= 2f;
        v.y *= 2f;
        v.z *= 2f;
        var num1 = (float)(q.w * (double)q.w - 0.5);
        var num2 = (float)(q.x * (double)v.x + q.y * (double)v.y + q.z * (double)v.z);
        return new Vector3(
            (float)(v.x * (double)num1 - (q.y * (double)v.z - q.z * (double)v.y) * q.w + q.x * (double)num2),
            (float)(v.y * (double)num1 - (q.z * (double)v.x - q.x * (double)v.z) * q.w + q.y * (double)num2),
            (float)(v.z * (double)num1 - (q.x * (double)v.y - q.y * (double)v.x) * q.w +
                    q.z * (double)num2));
    }

    public static void QInvRotate_ref(ref Quaternion q, ref Vector3 v, ref Vector3 result)
    {
        var num1 = 2f * v.x;
        var num2 = 2f * v.y;
        var num3 = 2f * v.z;
        var num4 = (float)(q.w * (double)q.w - 0.5);
        var num5 = (float)(q.x * (double)num1 + q.y * (double)num2 + q.z * (double)num3);
        result.x = (float)(num1 * (double)num4 - (q.y * (double)num3 - q.z * (double)num2) * q.w + q.x * (double)num5);
        result.y = (float)(num2 * (double)num4 - (q.z * (double)num1 - q.x * (double)num3) * q.w + q.y * (double)num5);
        result.z = (float)(num3 * (double)num4 - (q.x * (double)num2 - q.y * (double)num1) * q.w + q.z * (double)num5);
    }

    public static VectorLF3 QRotateLF(Quaternion q, VectorLF3 v)
    {
        v.x *= 2.0;
        v.y *= 2.0;
        v.z *= 2.0;
        var num1 = q.w * (double)q.w - 0.5;
        var num2 = q.x * v.x + q.y * v.y + q.z * v.z;
        return new VectorLF3(v.x * num1 + (q.y * v.z - q.z * v.y) * q.w + q.x * num2,
            v.y * num1 + (q.z * v.x - q.x * v.z) * q.w + q.y * num2,
            v.z * num1 + (q.x * v.y - q.y * v.x) * q.w + q.z * num2);
    }

    public static void QTransformLF_ref(
        ref VectorLF3 p,
        ref Quaternion q,
        ref VectorLF3 v,
        out VectorLF3 result)
    {
        var num1 = 2.0 * v.x;
        var num2 = 2.0 * v.y;
        var num3 = 2.0 * v.z;
        var num4 = q.w * (double)q.w - 0.5;
        var num5 = q.x * num1 + q.y * num2 + q.z * num3;
        result.x = num1 * num4 + (q.y * num3 - q.z * num2) * q.w + q.x * num5 + p.x;
        result.y = num2 * num4 + (q.z * num1 - q.x * num3) * q.w + q.y * num5 + p.y;
        result.z = num3 * num4 + (q.x * num2 - q.y * num1) * q.w + q.z * num5 + p.z;
    }

    public static void QRotateLF_ref(ref Quaternion q, ref VectorLF3 v, ref VectorLF3 result)
    {
        var num1 = 2.0 * v.x;
        var num2 = 2.0 * v.y;
        var num3 = 2.0 * v.z;
        var num4 = q.w * (double)q.w - 0.5;
        var num5 = q.x * num1 + q.y * num2 + q.z * num3;
        result.x = num1 * num4 + (q.y * num3 - q.z * num2) * q.w + q.x * num5;
        result.y = num2 * num4 + (q.z * num1 - q.x * num3) * q.w + q.y * num5;
        result.z = num3 * num4 + (q.x * num2 - q.y * num1) * q.w + q.z * num5;
    }

    public static VectorLF3 RotateLF(
        double axisx,
        double axisy,
        double axisz,
        double halfRad,
        VectorLF3 v)
    {
        var num1 = Math.Sin(halfRad);
        var num2 = axisx * num1;
        var num3 = axisy * num1;
        var num4 = axisz * num1;
        var num5 = Math.Cos(halfRad);
        v.x *= 2.0;
        v.y *= 2.0;
        v.z *= 2.0;
        var num6 = num5 * num5 - 0.5;
        var num7 = num2 * v.x + num3 * v.y + num4 * v.z;
        return new VectorLF3(v.x * num6 + (num3 * v.z - num4 * v.y) * num5 + num2 * num7,
            v.y * num6 + (num4 * v.x - num2 * v.z) * num5 + num3 * num7,
            v.z * num6 + (num2 * v.y - num3 * v.x) * num5 + num4 * num7);
    }

    public static VectorLF3 QInvRotateLF(Quaternion q, VectorLF3 v)
    {
        v.x *= 2.0;
        v.y *= 2.0;
        v.z *= 2.0;
        var num1 = q.w * (double)q.w - 0.5;
        var num2 = q.x * v.x + q.y * v.y + q.z * v.z;
        return new VectorLF3(v.x * num1 - (q.y * v.z - q.z * v.y) * q.w + q.x * num2,
            v.y * num1 - (q.z * v.x - q.x * v.z) * q.w + q.y * num2,
            v.z * num1 - (q.x * v.y - q.y * v.x) * q.w + q.z * num2);
    }

    public static void QInvRotateLF_ref(ref Quaternion q, ref VectorLF3 v, ref VectorLF3 result)
    {
        var num1 = 2.0 * v.x;
        var num2 = 2.0 * v.y;
        var num3 = 2.0 * v.z;
        var num4 = q.w * (double)q.w - 0.5;
        var num5 = q.x * num1 + q.y * num2 + q.z * num3;
        result.x = num1 * num4 - (q.y * num3 - q.z * num2) * q.w + q.x * num5;
        result.y = num2 * num4 - (q.z * num1 - q.x * num3) * q.w + q.y * num5;
        result.z = num3 * num4 - (q.x * num2 - q.y * num1) * q.w + q.z * num5;
    }

    public static void QInvRotateLF_refout(ref Quaternion q, ref VectorLF3 v, out VectorLF3 result)
    {
        var num1 = 2.0 * v.x;
        var num2 = 2.0 * v.y;
        var num3 = 2.0 * v.z;
        var num4 = q.w * (double)q.w - 0.5;
        var num5 = q.x * num1 + q.y * num2 + q.z * num3;
        result.x = num1 * num4 - (q.y * num3 - q.z * num2) * q.w + q.x * num5;
        result.y = num2 * num4 - (q.z * num1 - q.x * num3) * q.w + q.y * num5;
        result.z = num3 * num4 - (q.x * num2 - q.y * num1) * q.w + q.z * num5;
    }

    public static void QInvRotateLF_ref(ref Quaternion q, ref VectorLF3 v, ref Vector3 result)
    {
        var num1 = 2.0 * v.x;
        var num2 = 2.0 * v.y;
        var num3 = 2.0 * v.z;
        var num4 = q.w * (double)q.w - 0.5;
        var num5 = q.x * num1 + q.y * num2 + q.z * num3;
        result.x = (float)(num1 * num4 - (q.y * num3 - q.z * num2) * q.w + q.x * num5);
        result.y = (float)(num2 * num4 - (q.z * num1 - q.x * num3) * q.w + q.y * num5);
        result.z = (float)(num3 * num4 - (q.x * num2 - q.y * num1) * q.w + q.z * num5);
    }

    public static Quaternion QMultiply(Quaternion q1, Quaternion q2)
    {
        return new Quaternion(
            (float)(q1.x * (double)q2.w + q1.y * (double)q2.z - q1.z * (double)q2.y + q1.w * (double)q2.x),
            (float)(-(double)q1.x * q2.z + q1.y * (double)q2.w + q1.z * (double)q2.x + q1.w * (double)q2.y),
            (float)(q1.x * (double)q2.y - q1.y * (double)q2.x + q1.z * (double)q2.w + q1.w * (double)q2.z),
            (float)(-(double)q1.x * q2.x - q1.y * (double)q2.y - q1.z * (double)q2.z +
                    q1.w * (double)q2.w));
    }

    public static void QMultiply_ref(ref Quaternion q1, ref Quaternion q2, out Quaternion result)
    {
        result.x = (float)(q1.x * (double)q2.w + q1.y * (double)q2.z - q1.z * (double)q2.y + q1.w * (double)q2.x);
        result.y = (float)(-(double)q1.x * q2.z + q1.y * (double)q2.w + q1.z * (double)q2.x + q1.w * (double)q2.y);
        result.z = (float)(q1.x * (double)q2.y - q1.y * (double)q2.x + q1.z * (double)q2.w + q1.w * (double)q2.z);
        result.w = (float)(-(double)q1.x * q2.x - q1.y * (double)q2.y - q1.z * (double)q2.z + q1.w * (double)q2.w);
    }

    public static Quaternion QInvMultiply(Quaternion rot, Quaternion q)
    {
        return new Quaternion(
            (float)(rot.x * (double)q.w + rot.y * (double)q.z - rot.z * (double)q.y - rot.w * (double)q.x),
            (float)(-(double)rot.x * q.z + rot.y * (double)q.w + rot.z * (double)q.x - rot.w * (double)q.y),
            (float)(rot.x * (double)q.y - rot.y * (double)q.x + rot.z * (double)q.w - rot.w * (double)q.z),
            (float)(-(double)rot.x * q.x - rot.y * (double)q.y - rot.z * (double)q.z -
                    rot.w * (double)q.w));
    }

    public static void QInvMultiply_ref(ref Quaternion rot, ref Quaternion q, out Quaternion result)
    {
        result.x = (float)(rot.x * (double)q.w + rot.y * (double)q.z - rot.z * (double)q.y - rot.w * (double)q.x);
        result.y = (float)(-(double)rot.x * q.z + rot.y * (double)q.w + rot.z * (double)q.x - rot.w * (double)q.y);
        result.z = (float)(rot.x * (double)q.y - rot.y * (double)q.x + rot.z * (double)q.w - rot.w * (double)q.z);
        result.w = (float)(-(double)rot.x * q.x - rot.y * (double)q.y - rot.z * (double)q.z - rot.w * (double)q.w);
    }

    public static void LookRotation(Vector3 forward, Vector3 upDirection, out Quaternion result)
    {
        var rhs = Vector3.Cross(upDirection, forward);
        var vector3 = Vector3.Cross(forward, rhs);
        var num1 = rhs.x + vector3.y + forward.z;
        if (num1 > 0.0)
        {
            var num2 = 2f * Mathf.Sqrt(num1 + 1f);
            result.w = 0.25f * num2;
            result.x = (vector3.z - forward.y) / num2;
            result.y = (forward.x - rhs.z) / num2;
            result.z = (rhs.y - vector3.x) / num2;
        }
        else if (rhs.x > (double)vector3.y && rhs.x > (double)forward.z)
        {
            var num3 = 2f * Mathf.Sqrt(1f + rhs.x - vector3.y - forward.z);
            result.w = (vector3.z - forward.y) / num3;
            result.x = 0.25f * num3;
            result.y = (vector3.x + rhs.y) / num3;
            result.z = (forward.x + rhs.z) / num3;
        }
        else if (vector3.y > (double)forward.z)
        {
            var num4 = 2f * Mathf.Sqrt(1f + vector3.y - rhs.x - forward.z);
            result.w = (forward.x - rhs.z) / num4;
            result.x = (vector3.x + rhs.y) / num4;
            result.y = 0.25f * num4;
            result.z = (forward.y + vector3.z) / num4;
        }
        else
        {
            var num5 = 2f * Mathf.Sqrt(1f + forward.z - rhs.x - vector3.y);
            result.w = (rhs.y - vector3.x) / num5;
            result.x = (forward.x + rhs.z) / num5;
            result.y = (forward.y + vector3.z) / num5;
            result.z = 0.25f * num5;
        }

        var num6 = Mathf.Sqrt((float)(result.x * (double)result.x + result.y * (double)result.y +
                                      result.z * (double)result.z + result.w * (double)result.w));
        result.x /= num6;
        result.y /= num6;
        result.z /= num6;
        result.w /= num6;
    }

    public static void LookRotation(
        float fx,
        float fy,
        float fz,
        float ux,
        float uy,
        float uz,
        out Quaternion result)
    {
        var vector3_1 = new Vector3((float)(uy * (double)fz - fy * (double)uz),
            (float)(uz * (double)fx - fz * (double)ux), (float)(ux * (double)fy - fx * (double)uy));
        var vector3_2 = new Vector3((float)(fy * (double)vector3_1.z - vector3_1.y * (double)fz),
            (float)(fz * (double)vector3_1.x - vector3_1.z * (double)fx),
            (float)(fx * (double)vector3_1.y - vector3_1.x * (double)fy));
        var num1 = vector3_1.x + vector3_2.y + fz;
        if (num1 > 0.0)
        {
            var num2 = 2f * Mathf.Sqrt(num1 + 1f);
            result.w = 0.25f * num2;
            result.x = (vector3_2.z - fy) / num2;
            result.y = (fx - vector3_1.z) / num2;
            result.z = (vector3_1.y - vector3_2.x) / num2;
        }
        else if (vector3_1.x > (double)vector3_2.y && vector3_1.x > (double)fz)
        {
            var num3 = 2f * Mathf.Sqrt(1f + vector3_1.x - vector3_2.y - fz);
            result.w = (vector3_2.z - fy) / num3;
            result.x = 0.25f * num3;
            result.y = (vector3_2.x + vector3_1.y) / num3;
            result.z = (fx + vector3_1.z) / num3;
        }
        else if (vector3_2.y > (double)fz)
        {
            var num4 = 2f * Mathf.Sqrt(1f + vector3_2.y - vector3_1.x - fz);
            result.w = (fx - vector3_1.z) / num4;
            result.x = (vector3_2.x + vector3_1.y) / num4;
            result.y = 0.25f * num4;
            result.z = (fy + vector3_2.z) / num4;
        }
        else
        {
            var num5 = 2f * Mathf.Sqrt(1f + fz - vector3_1.x - vector3_2.y);
            result.w = (vector3_1.y - vector3_2.x) / num5;
            result.x = (fx + vector3_1.z) / num5;
            result.y = (fy + vector3_2.z) / num5;
            result.z = 0.25f * num5;
        }

        var num6 = Mathf.Sqrt((float)(result.x * (double)result.x + result.y * (double)result.y +
                                      result.z * (double)result.z + result.w * (double)result.w));
        result.x /= num6;
        result.y /= num6;
        result.z /= num6;
        result.w /= num6;
    }

    public static void LookRotation(
        ref Vector3 forward,
        ref Vector3 upDirection,
        out Quaternion result)
    {
        var vector3_1 = new Vector3((float)(upDirection.y * (double)forward.z - forward.y * (double)upDirection.z),
            (float)(upDirection.z * (double)forward.x - forward.z * (double)upDirection.x),
            (float)(upDirection.x * (double)forward.y - forward.x * (double)upDirection.y));
        var vector3_2 = new Vector3((float)(forward.y * (double)vector3_1.z - vector3_1.y * (double)forward.z),
            (float)(forward.z * (double)vector3_1.x - vector3_1.z * (double)forward.x),
            (float)(forward.x * (double)vector3_1.y - vector3_1.x * (double)forward.y));
        var num1 = vector3_1.x + vector3_2.y + forward.z;
        if (num1 > 0.0)
        {
            var num2 = 2f * Mathf.Sqrt(num1 + 1f);
            result.w = 0.25f * num2;
            result.x = (vector3_2.z - forward.y) / num2;
            result.y = (forward.x - vector3_1.z) / num2;
            result.z = (vector3_1.y - vector3_2.x) / num2;
        }
        else if (vector3_1.x > (double)vector3_2.y && vector3_1.x > (double)forward.z)
        {
            var num3 = 2f * Mathf.Sqrt(1f + vector3_1.x - vector3_2.y - forward.z);
            result.w = (vector3_2.z - forward.y) / num3;
            result.x = 0.25f * num3;
            result.y = (vector3_2.x + vector3_1.y) / num3;
            result.z = (forward.x + vector3_1.z) / num3;
        }
        else if (vector3_2.y > (double)forward.z)
        {
            var num4 = 2f * Mathf.Sqrt(1f + vector3_2.y - vector3_1.x - forward.z);
            result.w = (forward.x - vector3_1.z) / num4;
            result.x = (vector3_2.x + vector3_1.y) / num4;
            result.y = 0.25f * num4;
            result.z = (forward.y + vector3_2.z) / num4;
        }
        else
        {
            var num5 = 2f * Mathf.Sqrt(1f + forward.z - vector3_1.x - vector3_2.y);
            result.w = (vector3_1.y - vector3_2.x) / num5;
            result.x = (forward.x + vector3_1.z) / num5;
            result.y = (forward.y + vector3_2.z) / num5;
            result.z = 0.25f * num5;
        }

        var num6 = Mathf.Sqrt((float)(result.x * (double)result.x + result.y * (double)result.y +
                                      result.z * (double)result.z + result.w * (double)result.w));
        result.x /= num6;
        result.y /= num6;
        result.z /= num6;
        result.w /= num6;
    }

    public static float SphericalAngleAOBInRAD(Vector3 O, Vector3 A, Vector3 B)
    {
        var num1 = Math.Sqrt(O.x * (double)O.x + O.y * (double)O.y + O.z * (double)O.z);
        if (num1 < 9.9999997473787516E-05)
            return 0.0f;
        Vector3 vector3_1;
        vector3_1.x = O.x / (float)num1;
        vector3_1.y = O.y / (float)num1;
        vector3_1.z = O.z / (float)num1;
        Vector3 vector3_2;
        vector3_2.x = A.x - O.x;
        vector3_2.y = A.y - O.y;
        vector3_2.z = A.z - O.z;
        Vector3 vector3_3;
        vector3_3.x = B.x - O.x;
        vector3_3.y = B.y - O.y;
        vector3_3.z = B.z - O.z;
        var num2 = (float)(vector3_2.x * (double)vector3_1.x + vector3_2.y * (double)vector3_1.y +
                           vector3_2.z * (double)vector3_1.z);
        var num3 = (float)(vector3_3.x * (double)vector3_1.x + vector3_3.y * (double)vector3_1.y +
                           vector3_3.z * (double)vector3_1.z);
        vector3_2.x -= num2 * vector3_1.x;
        vector3_2.y -= num2 * vector3_1.y;
        vector3_2.z -= num2 * vector3_1.z;
        vector3_3.x -= num3 * vector3_1.x;
        vector3_3.y -= num3 * vector3_1.y;
        vector3_3.z -= num3 * vector3_1.z;
        var num4 = vector3_2.x * (double)vector3_3.x + vector3_2.y * (double)vector3_3.y +
                   vector3_2.z * (double)vector3_3.z;
        var d1 = vector3_2.x * (double)vector3_2.x + vector3_2.y * (double)vector3_2.y +
                 vector3_2.z * (double)vector3_2.z;
        var d2 = vector3_3.x * (double)vector3_3.x + vector3_3.y * (double)vector3_3.y +
                 vector3_3.z * (double)vector3_3.z;
        var num5 = Math.Sqrt(d1) * Math.Sqrt(d2);
        return (float)Math.Acos(num4 / num5);
    }

    public static float SphericalSlopeRatio(Vector3 start, Vector3 end)
    {
        var magnitude1 = start.magnitude;
        var magnitude2 = end.magnitude;
        if (magnitude1 == 0.0 && magnitude2 == 0.0)
            return 0.0f;
        if (magnitude1 == 0.0)
            return float.PositiveInfinity;
        if (magnitude2 == 0.0)
            return float.NegativeInfinity;
        var magnitude3 = (end * (magnitude1 / magnitude2) - start).magnitude;
        if (magnitude3 != 0.0)
            return (magnitude2 - magnitude1) / magnitude3;
        if (magnitude1 == (double)magnitude2)
            return 0.0f;
        return magnitude1 <= (double)magnitude2 ? float.PositiveInfinity : float.NegativeInfinity;
    }

    public static Vector3 GetPosByLatitudeAndLongitude(float lat, float log, float radius)
    {
        lat *= (float)Math.PI / 180f;
        log *= (float)Math.PI / 180f;
        var y = Mathf.Sin(lat);
        double num = Mathf.Cos(lat);
        var x = (float)num * Mathf.Sin(log);
        var z = (float)-num * Mathf.Cos(log);
        return new Vector3(x, y, z).normalized * radius;
    }

    public static void GetLatitudeLongitude(
        Vector3 pos,
        out int latd,
        out int latf,
        out int logd,
        out int logf,
        out bool north,
        out bool south,
        out bool west,
        out bool east)
    {
        pos = pos.normalized;
        var f = Mathf.Asin(pos.y);
        var num1 = 0.000145f;
        north = pos.y > (double)num1;
        south = pos.y < -(double)num1;
        var num2 = 0.0f;
        if (Mathf.Abs(pos.x) > 0.00011999999696854502 || Mathf.Abs(pos.z) > 0.00011999999696854502)
        {
            num2 = Mathf.Atan2(pos.x, -pos.z);
            east = pos.x > num1 * (double)Mathf.Cos(f);
            west = pos.x < -(double)num1 * Mathf.Cos(f);
        }
        else
        {
            east = false;
            west = false;
        }

        var num3 = Mathf.RoundToInt(Mathf.Abs((float)(num2 * 57.295780181884766 * 60.0)));
        var num4 = Mathf.RoundToInt(Mathf.Abs((float)(f * 57.295780181884766 * 60.0)));
        logd = num3 / 60;
        latd = num4 / 60;
        logf = num3 % 60;
        latf = num4 % 60;
    }

    public static void GetLatitudeLongitudeSpace(
        VectorLF3 pos,
        out int latd,
        out int latf,
        out int logd,
        out int logf,
        out double alt,
        out bool north,
        out bool south)
    {
        var magnitude = pos.magnitude;
        alt = magnitude;
        if (magnitude < 1.0)
        {
            latd = 0;
            latf = 0;
            logd = 0;
            logf = 0;
            north = false;
            south = false;
        }
        else
        {
            pos /= magnitude;
            var num1 = Math.Asin(pos.y);
            var num2 = 0.000145f;
            north = pos.y > num2;
            south = pos.y < -(double)num2;
            double num3;
            if (Math.Abs(pos.x) > 0.00011999999696854502 || Math.Abs(pos.z) > 0.00011999999696854502)
            {
                num3 = Math.Atan2(pos.x, pos.z);
                if (num3 < 0.0)
                    num3 += 2.0 * Math.PI;
            }
            else
            {
                num3 = 0.0;
            }

            var num4 = (int)(Math.Abs(num3 * (180.0 / Math.PI) * 60.0) + 0.5);
            var num5 = (int)(Math.Abs(num1 * (180.0 / Math.PI) * 60.0) + 0.5);
            logd = num4 / 60;
            latd = num5 / 60;
            logf = num4 % 60;
            latf = num5 % 60;
        }
    }

    public static Vector3 Elerp(Vector3 a, Vector3 b, float t)
    {
        var num1 = 1f - t;
        var num2 = (float)Math.Sqrt(a.x * (double)a.x + a.y * (double)a.y + a.z * (double)a.z);
        var num3 = (float)Math.Sqrt(b.x * (double)b.x + b.y * (double)b.y + b.z * (double)b.z);
        if (num2 < 9.9999999747524271E-07 || num3 < 9.9999999747524271E-07)
            return new Vector3((float)(a.x * (double)num1 + b.x * (double)t),
                (float)(a.y * (double)num1 + b.y * (double)t), (float)(a.z * (double)num1 + b.z * (double)t));
        var vector3_1 = new Vector3(a.x / num2, a.y / num2, a.z / num2);
        var vector3_2 = new Vector3(b.x / num3, b.y / num3, b.z / num3);
        var num4 = Math.Asin(vector3_1.y);
        var num5 = (float)Math.Atan2(vector3_1.z, vector3_1.x);
        var num6 = (float)Math.Asin(vector3_2.y);
        var num7 = (float)Math.Atan2(vector3_2.z, vector3_2.x);
        if (vector3_1.x < 9.9999999747524271E-07 && vector3_1.x > -9.9999999747524271E-07 &&
            vector3_1.z < 9.9999999747524271E-07 && vector3_1.z > -9.9999999747524271E-07)
            num5 = num7;
        if (vector3_2.x < 9.9999999747524271E-07 && vector3_2.x > -9.9999999747524271E-07 &&
            vector3_2.z < 9.9999999747524271E-07 && vector3_2.z > -9.9999999747524271E-07)
            num7 = num5;
        double num8 = num1;
        var num9 = (float)(num4 * num8 + num6 * (double)t);
        var num10 = Mathf.Repeat(num7 - num5, 6.28318548f);
        if (num10 > 3.1415927410125732)
            num10 -= 6.28318548f;
        var num11 = num5 + num10 * (double)t;
        var num12 = (float)(num2 * (double)num1 + num3 * (double)t);
        var num13 = (float)Math.Cos(num11);
        var num14 = (float)Math.Cos(num9);
        var num15 = (float)Math.Sin(num11);
        var num16 = (float)Math.Sin(num9);
        return new Vector3(num12 * num13 * num14, num12 * num16, num12 * num15 * num14);
    }

    public static bool RayIntersectsTriangle(
        Vector3 org,
        Vector3 dir,
        Vector3 A,
        Vector3 B,
        Vector3 C)
    {
        var lhs = B - A;
        var rhs = C - A;
        var normalized = Vector3.Cross(lhs, rhs).normalized;
        var num1 = (float)(normalized.x * (double)dir.x + normalized.y * (double)dir.y + normalized.z * (double)dir.z);
        if (num1 < 9.9999997473787516E-05 && num1 > -9.9999997473787516E-05)
            return false;
        var vector3_1 =
            org + dir * ((float)-(Vector3.Dot(normalized, org) + (double)Vector3.Dot(normalized, A)) / num1);
        var num2 = (float)(lhs.x * (double)lhs.x + lhs.y * (double)lhs.y + lhs.z * (double)lhs.z);
        var num3 = (float)(rhs.x * (double)rhs.x + rhs.y * (double)rhs.y + rhs.z * (double)rhs.z);
        var num4 = (float)(lhs.x * (double)rhs.x + lhs.y * (double)rhs.y + lhs.z * (double)lhs.z);
        var num5 = (float)(num2 * (double)num3 - num4 * (double)num4);
        if (num5 < 9.9999997473787516E-05 && num5 > -9.9999997473787516E-05)
            return false;
        var vector3_2 = vector3_1 - A;
        var num6 = (float)(vector3_2.x * (double)lhs.x + vector3_2.y * (double)lhs.y + vector3_2.z * (double)lhs.z);
        var num7 = (float)(vector3_2.x * (double)rhs.x + vector3_2.y * (double)rhs.y + vector3_2.z * (double)rhs.z);
        var num8 = 1f / num5;
        var num9 = (float)(num3 * (double)num6 - num4 * (double)num7) * num8;
        var num10 = (float)(-(double)num4 * num6 + num2 * (double)num7) * num8;
        return num9 >= 0.0 && num10 >= 0.0 && num9 + (double)num10 <= 1.0;
    }

    public static double Levelize(double f, double level = 1.0, double offset = 0.0)
    {
        f = f / level - offset;
        var num1 = Math.Floor(f);
        var num2 = f - num1;
        var num3 = (3.0 - num2 - num2) * num2 * num2;
        f = num1 + num3;
        f = (f + offset) * level;
        return f;
    }

    public static double Levelize2(double f, double level = 1.0, double offset = 0.0)
    {
        f = f / level - offset;
        var num1 = Math.Floor(f);
        var num2 = f - num1;
        var num3 = (3.0 - num2 - num2) * num2 * num2;
        var num4 = (3.0 - num3 - num3) * num3 * num3;
        f = num1 + num4;
        f = (f + offset) * level;
        return f;
    }

    public static double Levelize3(double f, double level = 1.0, double offset = 0.0)
    {
        f = f / level - offset;
        var num1 = Math.Floor(f);
        var num2 = f - num1;
        var num3 = (3.0 - num2 - num2) * num2 * num2;
        var num4 = (3.0 - num3 - num3) * num3 * num3;
        var num5 = (3.0 - num4 - num4) * num4 * num4;
        f = num1 + num5;
        f = (f + offset) * level;
        return f;
    }

    public static double Levelize4(double f, double level = 1.0, double offset = 0.0)
    {
        f = f / level - offset;
        var num1 = Math.Floor(f);
        var num2 = f - num1;
        var num3 = (3.0 - num2 - num2) * num2 * num2;
        var num4 = (3.0 - num3 - num3) * num3 * num3;
        var num5 = (3.0 - num4 - num4) * num4 * num4;
        var num6 = (3.0 - num5 - num5) * num5 * num5;
        f = num1 + num6;
        f = (f + offset) * level;
        return f;
    }

    public static double Random01(ref uint seed)
    {
        seed = (uint)((seed % 2147483646U + 1U) * 48271UL % int.MaxValue) - 1U;
        return seed / 2147483646.0;
    }

    public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd)
    {
        var rhs = point - lineStart;
        var vector3 = lineEnd - lineStart;
        var magnitude = vector3.magnitude;
        var lhs = vector3;
        if (magnitude > 9.99999997475243E-07)
            lhs /= magnitude;
        var num = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0.0f, magnitude);
        return Vector3.Magnitude(lineStart + lhs * num - point);
    }

    public static Vector2 SLerp(ref Vector2 a, ref Vector2 b, float t)
    {
        if (t < 0.0)
            t = 0.0f;
        if (t > 1.0)
            t = 1f;
        var y1 = a.y;
        var x1 = a.x;
        var y2 = b.y;
        var x2 = b.x;
        if ((Mathf.Abs(y1) < 1.0 / 1000.0 && Mathf.Abs(x1) < 1.0 / 1000.0) ||
            (Mathf.Abs(y2) < 1.0 / 1000.0 && Mathf.Abs(x2) < 1.0 / 1000.0))
        {
            var num1 = 1f - t;
            var num2 = t;
            return new Vector2((float)(x1 * (double)num1 + x2 * (double)num2),
                (float)(y1 * (double)num1 + y2 * (double)num2));
        }

        var x3 = (float)((y1 * (double)y2 + x1 * (double)x2) / (a.magnitude * (double)b.magnitude));
        float num3;
        float num4;
        if (x3 >= 0.99900001287460327)
        {
            num3 = 1f - t;
            num4 = t;
        }
        else if (x3 <= -0.99900001287460327)
        {
            var x4 = -0.999f;
            var y3 = Mathf.Sqrt((float)(1.0 - x4 * (double)x4));
            var num5 = Mathf.Atan2(y3, x4);
            var num6 = 1f / y3;
            num3 = Mathf.Sin((1f - t) * num5) * num6;
            num4 = Mathf.Sin(t * num5) * num6;
        }
        else
        {
            var y4 = Mathf.Sqrt((float)(1.0 - x3 * (double)x3));
            var num7 = Mathf.Atan2(y4, x3);
            var num8 = 1f / y4;
            num3 = Mathf.Sin((1f - t) * num7) * num8;
            num4 = Mathf.Sin(t * num7) * num8;
        }

        return new Vector2((float)(x1 * (double)num3 + x2 * (double)num4),
            (float)(y1 * (double)num3 + y2 * (double)num4));
    }

    public static float TrapezoidalSpeedAlgo(
        float offset,
        float uniformSpeed,
        float initialSpeed,
        float finalSpeed,
        float speedUpAcc,
        float speedDownAcc,
        float t,
        int type = 0)
    {
        if (offset < 0.0 || t < 0.0)
            return 0.0f;
        var num1 = speedUpAcc;
        var num2 = speedDownAcc <= 0.0 ? speedDownAcc : -speedDownAcc;
        if (initialSpeed > (double)uniformSpeed)
            initialSpeed = uniformSpeed;
        if (finalSpeed > (double)uniformSpeed)
            finalSpeed = uniformSpeed;
        var num3 = 0.0f;
        var num4 = 0.0f;
        var num5 = 0.0f;
        var num6 = 0.0f;
        var num7 = 0.0f;
        var num8 = Mathf.Sqrt((float)((-2.0 * num1 * num2 * offset - initialSpeed * (double)initialSpeed * num2 +
                                       finalSpeed * (double)finalSpeed * num1) / (num1 - (double)num2)));
        float num9;
        if (num8 > (double)uniformSpeed)
        {
            num9 = uniformSpeed;
            num3 = (num9 - initialSpeed) / num1;
            var num10 = (finalSpeed - num9) / num2;
            num5 = (float)(initialSpeed * (double)num3 + 0.5 * num1 * num3 * num3);
            var num11 = (float)(num9 * (double)num10 + 0.5 * num2 * num10 * num10);
            num6 = offset - num5 - num11;
            num4 = num6 / num9;
        }
        else if (initialSpeed < (double)num8 && num8 < (double)finalSpeed)
        {
            finalSpeed = Mathf.Sqrt((float)(initialSpeed * (double)initialSpeed + 2.0 * num1 * offset));
            num9 = finalSpeed;
            num3 = (finalSpeed - initialSpeed) / num1;
            num5 = (float)(initialSpeed * (double)num3 + 0.5 * num1 * num3 * num3);
        }
        else if (finalSpeed < (double)num8 && num8 < (double)initialSpeed)
        {
            finalSpeed = Mathf.Sqrt((float)(initialSpeed * (double)initialSpeed + 2.0 * num2 * offset));
            num9 = initialSpeed;
            var num12 = (finalSpeed - initialSpeed) / num2;
            num7 = (float)(num9 * (double)num12 + 0.5 * num2 * num12 * num12);
        }
        else
        {
            num9 = num8;
            num3 = (num9 - initialSpeed) / num1;
            var num13 = (finalSpeed - num9) / num2;
            num5 = (float)(initialSpeed * (double)num3 + 0.5 * num1 * num3 * num3);
            num7 = (float)(num9 * (double)num13 + 0.5 * num2 * num13 * num13);
        }

        switch (type)
        {
            case 0:
                if (num3 > 0.0 && t < (double)num3)
                    return (float)(initialSpeed * (double)t + 0.5 * num1 * t * t);
                if (num4 > 0.0 && t > num3 + (double)num4)
                    return num5 + num9 * (t - num3);
                var num14 = t - num3 - num4;
                return (float)(num5 + (double)num6 + num9 * (double)num14 + 0.5 * num2 * num14 * num14);
            case 1:
                if (num3 > 0.0 && t < (double)num3)
                    return initialSpeed + num1 * t;
                return num4 > 0.0 && t > num3 + (double)num4 ? num9 : num9 + num2 * (t - num3 - num4);
            case 2:
                if (num3 > 0.0 && t < (double)num3)
                    return num1;
                return num4 > 0.0 && t > num3 + (double)num4 ? 0.0f : num2;
            default:
                return 0.0f;
        }
    }

    public static int SolveQuadraticEq(
        double a,
        double b,
        double c,
        out double result0,
        out double result1)
    {
        result0 = 0.0;
        result1 = 0.0;
        var num = 0;
        var d = b * b - 4.0 * a * c;
        if (Math.Abs(d) < 1E-09)
        {
            num = 1;
            result0 = -b / (a + a);
        }
        else if (d > 0.0)
        {
            num = 2;
            result0 = (Math.Sqrt(d) - b) / (a + a);
            result1 = (-Math.Sqrt(d) - b) / (a + a);
        }

        return num;
    }
}