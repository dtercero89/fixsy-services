using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using FixsyWebApi.Data.Extensions;
using FixsyWebApi.Data.Identity;
using EntityBase = FixsyWebApi.Data.Agg.EntityBase;
using Transaction = FixsyWebApi.Data.Agg.Logging.Transaction;

namespace FixsyWebApi.Data.UnitOfWork
{
    public class BCUnitOfWork : DbContext
    {
        public BCUnitOfWork(DbContextOptions<BCUnitOfWork> options):
            base (options)
        {
            
        }
        public void Commit()
        {
             foreach (var entry in ChangeTracker.Entries().Where(
                e => (e.Entity is EntityBase || e.Entity is EntityBase) &&
                (e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted)))
            {
                ((EntityBase)entry.Entity).TransactionDate = DateTime.Now;
            }

            base.SaveChanges();
        }

        public void CommitWithTransactionalTable(TransactionalInfo transaction)
        {
            this.Commit(transaction);
        }

        public void Commit(TransactionalInfo transactionInfo)
        {
            var transaccion = IdentityFactory.CreateIdentity().NewSequentialTransactionIdentity();

            var registroTransaccion = BuildTransaction(transaccion, transactionInfo);

            var modifiedEntities = ChangeTracker.Entries().Where(
                e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted).ToList();

            foreach (var entry in modifiedEntities)
            {
                ((EntityBase)entry.Entity).TransactionType = entry.State.ToString();
                ((EntityBase)entry.Entity).ModifiedBy = transactionInfo.ModifiedBy;
                ((EntityBase)entry.Entity).TransactionDate = DateTime.UtcNow;
                ((EntityBase)entry.Entity).TransactionName = transactionInfo.TransactionName;
                ((EntityBase)entry.Entity).TransactionId = transaccion.TransactionId;

                registroTransaccion.AddDetail(GetTableName(entry), entry.State.ToString(), transactionInfo.TransactionName);

                string modifiedEntity = entry.Entity.GetType().BaseType.ToString();

            }

            base.SaveChanges();
        }

        private bool IdEntityModified(string entityName, string entityEntry)
        {
            return entityEntry.Contains(entityName);
        }

        private string GetTableName(EntityEntry dbEntry)
        {
            var tipo = dbEntry.Entity.GetType();
            string name = dbEntry.Entity.GetType().Name;
            if (tipo.FullName != null)
            {
                    if (tipo.BaseType != null)
                    {
                        var fullName = (tipo.BaseType).FullName;
                        if (fullName != null)
                        {
                            name = fullName.Split('.').Last();
                        }
                    }
            }

            return name;
        }

        private Transaction BuildTransaction(TransactionIdentity transaccion, TransactionalInfo transactionInfo)
        {
            return new Transaction
            {
                TransactionId = transaccion.TransactionId,
                TransactionDate = transaccion.TransactionDate,
                TransactionType = transactionInfo.TransactionName,
                ModifiedBy = transactionInfo.ModifiedBy,
            };
        }

        public void CommitAndRefreshChanges()
        {
                bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        public void CommitMultipleTransactions(TransactionalInfo transactionInfo, string transactionId)
        {
            this.Commit();
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker
            // as 'unchanged state'
            ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, List<DbParameter> parameters) where TEntity : new()
        {
            return Database.FromSqlQuery<TEntity>(sqlQuery, parameters);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }
        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            //attach and set as unchanged
            Entry(item).State = EntityState.Unchanged;
        }

        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            //this operation also attach item in object state manager
            Entry(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            //if it is not attached, attach original and set current values
            Entry(original).CurrentValues.SetValues(current);
        }
    }

}