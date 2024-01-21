namespace DspLib.Algorithms;

public class URandom1
{
    private const int MBIG = 2147483647;
    private const int MSEED = 161803398;
    private const int MZ = 0;
    private readonly int[] SeedArray = new int[56];
    private int inext;
    private int inextp;

    public URandom1()
        : this(Environment.TickCount)
    {
    }

    public URandom1(int Seed)
    {
        var num1 = 161803398 - Math.Abs(Seed);
        SeedArray[55] = num1;
        var num2 = 1;
        for (var index1 = 1; index1 < 55; ++index1)
        {
            var index2 = 21 * index1 % 55;
            SeedArray[index2] = num2;
            num2 = num1 - num2;
            if (num2 < 0)
                num2 += int.MaxValue;
            num1 = SeedArray[index2];
        }

        for (var index3 = 1; index3 < 5; ++index3)
        for (var index4 = 1; index4 < 56; ++index4)
        {
            SeedArray[index4] -= SeedArray[1 + (index4 + 30) % 55];
            if (SeedArray[index4] < 0)
                SeedArray[index4] += int.MaxValue;
        }

        inext = 0;
        inextp = 31;
    }

    protected virtual double Sample()
    {
        if (++inext >= 56)
            inext = 1;
        if (++inextp >= 56)
            inextp = 1;
        var num = SeedArray[inext] - SeedArray[inextp];
        if (num < 0)
            num += int.MaxValue;
        SeedArray[inext] = num;
        return num * 4.6566128752457969E-10;
    }

    public virtual int Next()
    {
        return (int)(Sample() * int.MaxValue);
    }

    public virtual int Next(int maxValue)
    {
        if (maxValue < 0)
            throw new ArgumentOutOfRangeException("Max value is less than min value.");
        return (int)(Sample() * maxValue);
    }

    public virtual int Next(int minValue, int maxValue)
    {
        if (minValue > maxValue)
            throw new ArgumentOutOfRangeException("Min value is greater than max value.");
        var num = (uint)(maxValue - minValue);
        return num <= 1U ? minValue : (int)((uint)(Sample() * num) + minValue);
    }

    public virtual void NextBytes(byte[] buffer)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        for (var index = 0; index < buffer.Length; ++index)
            buffer[index] = (byte)(Sample() * 256.0);
    }

    public virtual double NextDouble()
    {
        return Sample();
    }
}