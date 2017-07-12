using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproFramework.Extensions
{
    internal static class QuerystringExtension
    {
        /// <summary>
        /// Remove Query string key and value and return sliced string
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static string RemoveFromQueryString(this string queryString, string value)
        {
            if (queryString.Contains(value))
            {
                var startIndex = queryString.IndexOf(value);
                var endIndex = queryString.IndexOf("&", queryString.IndexOf(value)) + 1;
                if (endIndex == 0)
                    endIndex = queryString.Count();

                return queryString.Remove(startIndex, endIndex);
            }
            else
            {
                return queryString;
            }

        }

    }
}
