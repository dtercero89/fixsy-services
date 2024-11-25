namespace FixsyWebApi.Data.Agg
{
    public class EntityBase
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string ModifiedBy { get; set; }
        public string TransactionName { get; set; }
        public string TransactionId { get; set; }
    }
}
