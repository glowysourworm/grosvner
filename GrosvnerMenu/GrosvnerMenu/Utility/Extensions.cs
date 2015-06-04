using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrosvnerMenu.Utility
{
    public static class Extensions
    {
        public static IEnumerable<string> Trim(this IEnumerable<string> collection)
        {
            return collection.Select(x => string.IsNullOrEmpty(x) ? x : x.Trim());
        }
        public static IEnumerable<int> ParseInt(this IEnumerable<string> collection)
        {
            return collection.Select(x =>
            {
                var result = 0;
                int.TryParse(x, out result);
                return result;
            });
        }
        public static string Join(this IEnumerable<string> collection, string glue)
        {
            var result = "";
            foreach (var str in collection)
                result += str + glue;

            if (result.EndsWith(glue))
                result = result.TrimEnd(glue.ToCharArray());

            return result;
        }
    }
}
