using FixsyWebApi.Data.UnitOfWork;
using FixsyWebApi.DTO;

namespace FixsyWebApi.Data.Extensions
{
    public static class TransactionalInfoExtension
    {
        public static TransactionalInfo GetTransactionInfo(this RequestBase request, string transaction )
        {
            return new TransactionalInfo
            {
                ModifiedBy = request.UserId.ToString(),
                TransactionName = transaction  ,
                TransactionDate = DateTime.UtcNow,
            };
        }
    }
}
