using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class IntList
{
    public static List<long> ToLongList(this List<int> ints)
    {
        if (ints.IsNull())
        {
            return null;
        }

        var longs = new List<long>();
        foreach (var anInt in ints)
        {
            longs.Add(Convert.ToInt64(anInt));
        }

        return longs;
    }
}

