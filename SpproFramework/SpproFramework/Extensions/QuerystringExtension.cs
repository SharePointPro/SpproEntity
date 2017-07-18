using Microsoft.SharePoint.Client.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        internal static string RemoveFromQueryString(this string queryString, string keyToRemove)
        {
            NameValueCollection queryParameters = new NameValueCollection();
            string[] querySegments = queryString.Split('&');
            foreach (string segment in querySegments)
            {
                string[] parts = segment.Split('=');
                if (parts.Length > 0)
                {
                    string key = parts[0].Trim(new char[] { '?', ' ' });
                    string val = parts[1].Trim();
                    queryParameters.Add(key, val);
                }
            }
            queryParameters.Remove(keyToRemove);
            return String.Join("&",
                        queryParameters.AllKeys.Select(a => a + "=" + (queryParameters[a])));
        }

    }
}
