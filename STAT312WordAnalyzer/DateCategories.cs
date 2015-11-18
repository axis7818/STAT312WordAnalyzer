using System;

namespace STAT312WordAnalyzer
{
    public static class DateCategories
    {
        public static readonly string NONE = "NONE";
        public static readonly string EARLY = "1850-1900";
        public static readonly string MIDDLE = "1901-1980";
        public static readonly string LATE = "1981-PRESENT";

        public static readonly DateTime _1850 = new DateTime(1850, 1, 1);
        public static readonly DateTime _1900 = new DateTime(1900, 1, 1);
        public static readonly DateTime _1980 = new DateTime(1980, 1, 1);
    }
}
