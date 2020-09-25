using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DateFormatHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .ToDictionary(c => c.Name, c => c.DateTimeFormat.ShortDatePattern.ToUpper());

            var mostCommon = allCultures.GroupBy(p => p.Value).OrderByDescending(p => p.Count()).First().Key;

            allCultures = allCultures.Where(p => p.Value != mostCommon).ToDictionary(p => p.Key, p => p.Value);

            var reverseDict = allCultures.GroupBy(p => p.Value).ToDictionary(p => p.Key, p => p.Select(q => q.Key).ToList());
            reverseDict[mostCommon] = new System.Collections.Generic.List<string>() { "*" };

            File.WriteAllText("culture-map.js", JsonConvert.SerializeObject(reverseDict, Formatting.Indented));
        }
    }
}
