using System;
using System.Collections.Generic;
using System.Text;

namespace Bleatingsheep.Osu.ApiV2.Utils
{
    public static class StringUtils
    {
        public static string ToUrlParamString(this IDictionary<string, string> args)
        {
            if (args == null || args.Count < 1)
                return "";
            StringBuilder sb = new StringBuilder("?");
            foreach (var item in args)
                sb.Append(item.Key + "=" + item.Value + "&");
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }
    }
}
