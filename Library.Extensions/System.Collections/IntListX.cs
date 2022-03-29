using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class IntListX
{
    public static List<long> UpcastToInt64(this IEnumerable<int> list)
    {
        var longs = new List<Int64>();
        list.ToList().ForEach(item => longs.Add(item));
        return longs;
    }

    public static List<int> DowncastToInt64(this IEnumerable<long> list)
    {
        var ints = new List<Int32>();
        list.ToList().ForEach(item => ints.Add(Convert.ToInt32(item)));
        return ints;
    }
}