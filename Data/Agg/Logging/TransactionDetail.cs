namespace FixsyWebApi.Data.Agg.Logging
{
public class TransactionDetail{
         
      public string TransactionId { get; set; }
        public string TableName { get; set; }
        public string CrudOperation { get; set; }
        public string TransactionType { get; set; }

        
}
}