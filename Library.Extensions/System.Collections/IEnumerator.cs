using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public static class IEnumeratorX
    {
        public static IEnumerable<T> ToIEnumerable<T>(this IEnumerator<T> enumerator)
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
    }
