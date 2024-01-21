namespace DspLib.Algorithms;

public struct Mathf
{
    public const float PI = 3.14159274f;
    public const float Infinity = float.PositiveInfinity;
    public const float NegativeInfinity = float.NegativeInfinity;
    public const float Deg2Rad = 0.0174532924f;
    public const float Rad2Deg = 57.29578f;

    public static readonly float Epsilon = !MathfInternal.IsFlushToZeroEnabled
        ? MathfInternal.FloatMinDenormal
        : MathfInternal.FloatMinNormal;

    public static float Sin(float f)
    {
        return (float)Math.Sin(f);
    }

    public static float Cos(float f)
    {
        return (float)Math.Cos(f);
    }

    public static float Tan(float f)
    {
        return (float)Math.Tan(f);
    }

    public static float Asin(float f)
    {
        return (float)Math.Asin(f);
    }

    public static float Acos(float f)
    {
        return (float)Math.Acos(f);
    }

    public static float Atan(float f)
    {
        return (float)Math.Atan(f);
    }

    public static float Atan2(float y, float x)
    {
        return (float)Math.Atan2(y, x);
    }

    public static float Sqrt(float f)
    {
        return (float)Math.Sqrt(f);
    }

    public static float Abs(float f)
    {
        return Math.Abs(f);
    }

    public static int Abs(int value)
    {
        return Math.Abs(value);
    }

    public static float Min(float a, float b)
    {
        return a < (double)b ? a : b;
    }

    public static float Min(params float[] values)
    {
        var length = values.Length;
        float num1;
        if (length == 0)
        {
            num1 = 0.0f;
        }
        else
        {
            var num2 = values[0];
            for (var index = 1; index < length; ++index)
                if (values[index] < (double)num2)
                    num2 = values[index];
            num1 = num2;
        }

        return num1;
    }

    public static int Min(int a, int b)
    {
        return a < b ? a : b;
    }

    public static int Min(params int[] values)
    {
        var length = values.Length;
        int num1;
        if (length == 0)
        {
            num1 = 0;
        }
        else
        {
            var num2 = values[0];
            for (var index = 1; index < length; ++index)
                if (values[index] < num2)
                    num2 = values[index];
            num1 = num2;
        }

        return num1;
    }

    public static float Max(float a, float b)
    {
        return a > (double)b ? a : b;
    }

    public static float Max(params float[] values)
    {
        var length = values.Length;
        float num1;
        if (length == 0)
        {
            num1 = 0.0f;
        }
        else
        {
            var num2 = values[0];
            for (var index = 1; index < length; ++index)
                if (values[index] > (double)num2)
                    num2 = values[index];
            num1 = num2;
        }

        return num1;
    }

    public static int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    public static int Max(params int[] values)
    {
        var length = values.Length;
        int num1;
        if (length == 0)
        {
            num1 = 0;
        }
        else
        {
            var num2 = values[0];
            for (var index = 1; index < length; ++index)
                if (values[index] > num2)
                    num2 = values[index];
            num1 = num2;
        }

        return num1;
    }

    public static float Pow(float f, float p)
    {
        return (float)Math.Pow(f, p);
    }

    public static float Exp(float power)
    {
        return (float)Math.Exp(power);
    }

    public static float Log(float f, float p)
    {
        return (float)Math.Log(f, p);
    }

    public static float Log(float f)
    {
        return (float)Math.Log(f);
    }

    public static float Log10(float f)
    {
        return (float)Math.Log10(f);
    }

    public static float Ceil(float f)
    {
        return (float)Math.Ceiling(f);
    }

    public static float Floor(float f)
    {
        return (float)Math.Floor(f);
    }

    public static float Round(float f)
    {
        return (float)Math.Round(f);
    }

    public static int CeilToInt(float f)
    {
        return (int)Math.Ceiling(f);
    }

    public static int FloorToInt(float f)
    {
        return (int)Math.Floor(f);
    }

    public static int RoundToInt(float f)
    {
        return (int)Math.Round(f);
    }

    public static float Sign(float f)
    {
        return f >= 0.0 ? 1f : -1f;
    }

    public static float Clamp(float value, float min, float max)
    {
        if (value < (double)min)
            value = min;
        else if (value > (double)max)
            value = max;
        return value;
    }

    public static int Clamp(int value, int min, int max)
    {
        if (value < min)
            value = min;
        else if (value > max)
            value = max;
        return value;
    }

    public static float Clamp01(float value)
    {
        return value >= 0.0 ? value <= 1.0 ? value : 1f : 0.0f;
    }

    public static float Lerp(float a, float b, float t)
    {
        return a + (b - a) * Clamp01(t);
    }

    public static float LerpUnclamped(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    public static float LerpAngle(float a, float b, float t)
    {
        var num = Repeat(b - a, 360f);
        if (num > 180.0)
            num -= 360f;
        return a + num * Clamp01(t);
    }

    public static float MoveTowards(float current, float target, float maxDelta)
    {
        return Abs(target - current) > (double)maxDelta ? current + Sign(target - current) * maxDelta : target;
    }

    public static float MoveTowardsAngle(float current, float target, float maxDelta)
    {
        var num1 = DeltaAngle(current, target);
        float num2;
        if (-(double)maxDelta < num1 && num1 < (double)maxDelta)
        {
            num2 = target;
        }
        else
        {
            target = current + num1;
            num2 = MoveTowards(current, target, maxDelta);
        }

        return num2;
    }

    public static float SmoothStep(float from, float to, float t)
    {
        t = Clamp01(t);
        t = (float)(-2.0 * t * t * t + 3.0 * t * t);
        return (float)(to * (double)t + from * (1.0 - t));
    }

    public static float Repeat(float t, float length)
    {
        return Clamp(t - Floor(t / length) * length, 0.0f, length);
    }

    public static float PingPong(float t, float length)
    {
        t = Repeat(t, length * 2f);
        return length - Abs(t - length);
    }

    public static float InverseLerp(float a, float b, float value)
    {
        return a == (double)b ? 0.0f : Clamp01((float)((value - (double)a) / (b - (double)a)));
    }

    public static float DeltaAngle(float current, float target)
    {
        var num = Repeat(target - current, 360f);
        if (num > 180.0)
            num -= 360f;
        return num;
    }

    internal static bool LineIntersection(
        Vector2 p1,
        Vector2 p2,
        Vector2 p3,
        Vector2 p4,
        ref Vector2 result)
    {
        var num1 = p2.x - p1.x;
        var num2 = p2.y - p1.y;
        var num3 = p4.x - p3.x;
        var num4 = p4.y - p3.y;
        var num5 = (float)(num1 * (double)num4 - num2 * (double)num3);
        bool flag;
        if (num5 == 0.0)
        {
            flag = false;
        }
        else
        {
            var num6 = p3.x - (double)p1.x;
            var num7 = p3.y - p1.y;
            double num8 = num4;
            var num9 = (float)(num6 * num8 - num7 * (double)num3) / num5;
            result = new Vector2(p1.x + num9 * num1, p1.y + num9 * num2);
            flag = true;
        }

        return flag;
    }

    internal static bool LineSegmentIntersection(
        Vector2 p1,
        Vector2 p2,
        Vector2 p3,
        Vector2 p4,
        ref Vector2 result)
    {
        var num1 = p2.x - p1.x;
        var num2 = p2.y - p1.y;
        var num3 = p4.x - p3.x;
        var num4 = p4.y - p3.y;
        var num5 = (float)(num1 * (double)num4 - num2 * (double)num3);
        bool flag;
        if (num5 == 0.0)
        {
            flag = false;
        }
        else
        {
            var num6 = p3.x - p1.x;
            var num7 = p3.y - p1.y;
            var num8 = (float)(num6 * (double)num4 - num7 * (double)num3) / num5;
            if (num8 < 0.0 || num8 > 1.0)
            {
                flag = false;
            }
            else
            {
                var num9 = (float)(num6 * (double)num2 - num7 * (double)num1) / num5;
                if (num9 < 0.0 || num9 > 1.0)
                {
                    flag = false;
                }
                else
                {
                    result = new Vector2(p1.x + num8 * num1, p1.x + num8 * num2);
                    flag = true;
                }
            }
        }

        return flag;
    }
}