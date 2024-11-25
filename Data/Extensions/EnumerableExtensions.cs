namespace FixsyWebApi.Data.Extensions
{

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Items<T>(this IEnumerable<T> list)
        {
            return list ?? new HashSet<T>();
        }

        public static bool HasItems<T>(this IEnumerable<T> list)
        {
            return list?.Any() ?? false;
        }

        public static bool IsNull(this object item)
        {
            return item == null;
        }

        public static bool IsNotNull(this object item)
        {
            return item != null;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            // Usamos el método HasItems directamente
            return !list.HasItems();
        }

        public static string ToStringConcatenated(this IEnumerable<string> list)
        {
            if (list.HasItems())
            {
                return string.Join(",", list.ToArray());
            }
            return string.Empty;
        }
    }
}
