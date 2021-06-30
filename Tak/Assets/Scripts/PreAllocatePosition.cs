﻿using System.Collections.Generic;
using System.Linq;

public static class PreAllocateList
{
    public static List<T> RepeatedDefault<T>(int count)
    {
        return Repeated(default(T), count);
    }

    public static List<T> Repeated<T>(T value, int count)
    {
        List<T> ret = new List<T>(count);
        ret.AddRange(Enumerable.Repeat(value, count));
        return ret;
    }
}