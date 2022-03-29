using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class LongList
{
    public static List<int> ToIntList(this List<long> longs)
    {
        if (longs.IsNull())
        {
            return null;
        }

        var ints = new List<int>();

        foreach (var aLong in longs)
        {
            ints.Add(Convert.ToInt32(aLong));
        }

        return ints;
    }
}


