namespace FixsyWebApi.Data.Identity
{
public class TransactionIdentity{
        public string TransactionId { get; set; }

        /// <summary>
        /// Server's Date and Time 
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// UTC date and time for the transaction.
        /// </summary>
        public DateTime TransactionUtcDate { get; set; }
}
}