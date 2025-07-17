using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : DbContext(options), IUnitOfWork
    {
        private readonly IMediator _mediator = mediator;
        private IDbContextTransaction _currentTransaction;

        /// <summary>
        /// Gets or sets the Orders DbSet.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets the current transaction.
        /// </summary>
        /// <returns>The current transaction.</returns>
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        /// <summary>
        /// Gets a value indicating whether there is an active transaction.
        /// </summary>
        public bool HasActiveTransaction => _currentTransaction != null;

        /// <summary>
        /// Configures the model that was discovered by convention from the entity types
        /// exposed in DbSet properties on your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
        }

        /// <summary>
        /// Saves the entities asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success.</returns>
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        /// <summary>
        /// Begins a new transaction asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the new transaction.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        /// <summary>
        /// Commits the given transaction asynchronously.
        /// </summary>
        /// <param name="transaction">The transaction to commit.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the transaction is null.</exception>
        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction == transaction)
                {
                    _currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// Rolls back the current transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}