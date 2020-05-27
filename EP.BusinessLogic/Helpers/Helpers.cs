using EP.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace EP.EntityData.Helpers
{
    public static class DisciplineHelper
    {
        public static string GetDisciplineLatvianName(DisciplineEnum id)
        {
            switch ((int)id)
            {
                case 1:
                    return "Futbols";
                case 2:
                    return "Basketbols";
                case 3:
                    return "KiberSports";
                default:
                    return string.Empty;
            }
        }
    }

    public static class TextHelper
    {
        public static string ToUpperCaseFirstLetter(string text)
        {
            return text.Length > 0
                ? char.ToUpper(text[0]) + text.Substring(1)
                : string.Empty;
        }
    }

    public static class SeasonHelper
    {
        public static List<DateTime> GetSeasonDates(int season)
        {
            return new List<DateTime>()
            {
                new DateTime(season, 1, 1),
                new DateTime(season, 12, 31)
            };
        }

        public static List<Default> GetSeasons()
        {
            var result = new List<Default>();

            for(var i = 2016; i < DateTime.Now.AddYears(1).Year; i++)
            {
                result.Add(new Default
                {
                    Id = i,
                    Name = $"{i}"
                });
            }

            return result;
        }
    }
}