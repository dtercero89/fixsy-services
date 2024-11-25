namespace FixsyWebApi.Data.Extensions
{
    public static class NumbersExtension
    {
        public static decimal ToFraction(this decimal percentage)
        {
            return percentage / 100;
        }

        public static decimal WithPrecision(this decimal value, int decimals)
        {
            return Math.Round(value, decimals);
        }
    }
}
