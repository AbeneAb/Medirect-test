


namespace Exchange.Infrastructure
{
    public class ExchangeContext : DbContext,IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "exchange";
        public DbSet<Account> Account { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<TransactionStatus> Status { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }
        //public DbSet<ExchangeRate> ExchangeRates { get; set; }
        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;
        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options) { }

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public ExchangeContext(DbContextOptions<ExchangeContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


            System.Diagnostics.Debug.WriteLine("ExchangeContext::ctor ->" + this.GetHashCode());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransactionEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionStatusEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyTypeEntityConfiguration());
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
          
            // After executing this line all the changes
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);
            // Dispatch Domain Events collection. 
            await _mediator.DispatchDomainEventsAsync(this);
            return true;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

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
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

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

public class ExchangeContextDesignFactory : IDesignTimeDbContextFactory<ExchangeContext>
{
    public ExchangeContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ExchangeContext>().
            UseSqlServer("Server=localhost,5433;Initial Catalog=exchangedb;User Id=sa;Password=Thynk1234;");

        return new ExchangeContext(optionsBuilder.Options, new NoMediator());
    }

    class NoMediator : IMediator
    {
        public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default(CancellationToken)) where TNotification : INotification
        {
            return Task.CompletedTask;
        }

        public Task Publish(object notification, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.FromResult<TResponse>(default(TResponse));
        }

        public Task<object> Send(object request, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(default(object));
        }
    }
}
