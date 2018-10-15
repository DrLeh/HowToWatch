using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToWatch.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return Enumerable.Empty<T>();

            return list;
        }
    }
}
