namespace Exchange.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        private readonly ExchangeContext _context;
        public AccountRepository(ExchangeContext exchangeContext)
        {
            _context = exchangeContext;
        }
        public Account Add(Account account)
        {
            if (account.IsTransient())
            {
                return _context.Account
                    .Add(account)
                    .Entity;
            }
            else
            {
                return account;
            }
        }

        public async Task<Account> FindByIdAsync(int id)
        {
            var account = await _context.Account.Include(b => b.Currency)
             .Where(b => b.Id == id)
             .SingleOrDefaultAsync();

            return account;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var accounts = await _context.Account.AsNoTracking().ToListAsync();
            return accounts;
        }

        public Account Update(Account account)
        {
            return _context.Account.Update(account).Entity;
        }

        public async Task<bool> ValidateBalanceAsync(int id,decimal amount)
        {
            var account = await _context.Account.FindAsync(id);
            if (account is null)
                return false;
            return account.Balance > amount;
        }

        public async Task<Account> FindByIdandNameAsync(string name, string currencyName)
        {
            var account = await _context.Account.Where(a=>a.FullName == name && a.Currency.Name == currencyName).FirstOrDefaultAsync();
            return account;
        }
    }
}
