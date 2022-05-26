namespace Exchange.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        private readonly ExchangeContext _context;

        public TransactionRepository(ExchangeContext exchangeContext)
        {
            _context = exchangeContext;
        }
        public Transaction Add(Transaction transaction)
        {
            if (transaction.IsTransient())
            {
                return _context.Transaction
                    .Add(transaction)
                    .Entity;
            }
            else
            {
                return transaction;
            }
        }
        public async Task<IEnumerable<Transaction>> FindByBuyerAsync(int buyerId)
        {
            var transactions = await _context.Transaction.
               Include(b => b.FromCurrency)
               .Include(b => b.ToCurrency).Where(b => b.GetBuyerId == buyerId)
               .ToListAsync();
            return transactions;
        }
        public async Task<Transaction> FindByIdAsync(int id)
        {
            var transaction = await _context.Transaction.
                Include(b => b.FromCurrency)
                .Include(b=>b.ToCurrency).Where(b => b.Id == id)
                .SingleOrDefaultAsync();

            return transaction;
        }
        public Transaction Update(Transaction transaction)
        {
            return _context.Transaction.Update(transaction).Entity;
        }

        public async Task<bool> CanMakeTransaction(int buyerId)
        {
            var transactionsInLastHour = await _context.Transaction.Where(t=>t.GetBuyerId == buyerId && t.GetCreatedDate <= DateTime.UtcNow.AddHours(-1)).CountAsync();
            return (transactionsInLastHour <= 10);
        }
    }
}
