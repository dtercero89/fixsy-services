using System.Globalization;

namespace FixsyWebApi.Data.Extensions
{
    public static class StringExtensions
    {
        public static string AddParameters(this string message, params object[] parameters)
        {
            return string.Format(message, parameters);
        }

        public static bool IsMissingValue(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static DateTime ToUtcDateTime(this string isoString)
        {
            if (string.IsNullOrEmpty(isoString))
            {
                throw new ArgumentException("El string no puede ser nulo o vacío.", nameof(isoString));
            }

            return DateTime.Parse(isoString, null, DateTimeStyles.RoundtripKind);
        }

        public static int ToInt(this string str) {

            if (!str.IsMissingValue())
            {
                 return Convert.ToInt32(str.Trim());
            }
            return 0;
        }

        public static string[] IntoArray(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return [];
            }

            var spl = str.Split(',').Select(s=>s.Trim());
            return spl.ToArray();
        }
    }
}
