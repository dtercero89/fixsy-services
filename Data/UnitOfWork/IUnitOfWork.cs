
namespace FixsyWebApi.Data.UnitOfWork
{
    /// <summary>
    /// Contract for ‘UnitOfWork pattern’. For more
    /// related info see http://martinfowler.com/eaaCatalog/unitOfWork.html or
    /// http://msdn.microsoft.com/en-us/magazine/dd882510.aspx
    /// In this solution, the Unit Of Work is implemented using the out-of-box 
    /// Entity Framework Context (EF 4.1 DbContext) persistence engine. But in order to
    /// comply the PI (Persistence Ignorant) principle in our Domain, we implement this interface/contract. 
    /// This interface/contract should be complied by any UoW implementation to be used with this Domain.
    /// </summary>
    public interface IUnitOfWork
        : IDisposable
    {
        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown
        ///</remarks>
        void Commit();

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown.
        /// Also logs information  for the transaction.
        ///</remarks>
        /// <param name="transactionInfo">Client's information to add to the Transaction's info</param>
        void Commit(TransactionalInfo transactionInfo);

        /// <summary>
        /// Commit all changes made in a container and updates the field "TipoTransaccion" separately for each entity.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,  
        /// then an exception is thrown.
        /// Also logs information  for the transaction.
        ///</remarks>
        /// <param name="transactionInfo">Client's information to add to the Transaction's info</param>
        /// <param name="transactionId">Transaction Id.</param>
        void CommitMultipleTransactions(TransactionalInfo transactionInfo, string transactionId);

        /// <summary>
        /// Commit all changes made in  a container.
        /// </summary>
        ///<remarks>
        /// If the entity have fixed properties and any optimistic concurrency problem exists,
        /// then 'client changes' are refreshed - Client wins
        ///</remarks>
        void CommitAndRefreshChanges();


        /// <summary>
        /// Rollback tracked changes. See references of UnitOfWork pattern
        /// </summary>
        void RollbackChanges();
    }
}
