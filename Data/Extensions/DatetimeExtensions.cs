namespace FixsyWebApi.Data.Extensions
{
    public static class DatetimeExtensions
    {
        public static string ToISOString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }
    }
}
