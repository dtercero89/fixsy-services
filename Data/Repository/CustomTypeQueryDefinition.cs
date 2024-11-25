namespace FixsyWebApi.Data.Repository
{
    public sealed class CustomTypeQueryDefinition
    {
        public string SqlTypeName { get; private set; }

        public List<string> Values { get; private set; }

        public CustomTypeQueryDefinition(string sqlTypeName, List<string> values)
        {
            SqlTypeName = sqlTypeName;
            Values = values;
        }
    }
}
