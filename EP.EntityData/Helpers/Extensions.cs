using System.Text.RegularExpressions;
using System;

namespace EP.EntityData.Helpers
{
    public static class EnumExtension
    {
        public static string Wordify(this Enum value)
        {
            var r = new Regex("(?<=[a-z])(?<m>[A-Z])|(?<=.)(?<m>[A-Z])(?=[a-z])");

            return r.Replace(value.ToString(), " ${m}");
        }
    }
}
