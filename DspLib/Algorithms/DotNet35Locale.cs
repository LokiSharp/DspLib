﻿namespace DspLib.Algorithms;

internal sealed class DotNet35Locale
{
    private DotNet35Locale()
    {
    }

    public static string GetText(string msg)
    {
        return msg;
    }

    public static string GetText(string fmt, params object[] args)
    {
        return string.Format(fmt, args);
    }
}