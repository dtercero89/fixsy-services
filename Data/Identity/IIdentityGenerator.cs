namespace FixsyWebApi.Data.Identity
{
    public interface IIdentityGenerator{
        TransactionIdentity NewSequentialTransactionIdentity();
    }
}