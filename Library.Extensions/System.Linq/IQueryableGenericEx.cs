using System.Collections.Generic;
using System.Linq;

public static class IQueryableGenericEx
    {
		#region Methods (1) 

		// Public Methods (1) 

        public static List<T> ToListSafe<T>(this IQueryable<T> iq)
        {
            return iq.Any() ? iq.ToList<T>() : new List<T>();
        }

        public static List<TSource> Page<TSource>(this List<TSource> source, int page, int pageSize = 10)
        {
            
            return source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source,int page, int pageSize = 10)
        {
           
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }


        public static bool CountedZero<T>(this IQueryable<T> data)
        {
            if (data.IsNull())
                return true;
            else
                if (data.Any())
                    return true;
                else
                    return false;
        }
        public static bool CountedZeroOrNegative<T>(this IQueryable<T> data)
        {
            if (data.IsNull())
                return true;
            else
                return data.Any();
        }
        public static bool CountedPositive<T>(this IQueryable<T> data)
        {
            if (data.IsNull())
                return false;
            else
                return data.Any();
        }
        public static bool CountedNegative<T>(this IQueryable<T> data)
        {
            if (data.IsNull())
                return false;
            else
                return data.Any();
        }


    #endregion Methods 
    }

