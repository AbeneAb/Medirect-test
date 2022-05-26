public interface IAccountRepository : ITransactionRepository<Account>
{
    Account Add(Account account);
    Account Update(Account account);
    Task<Account> FindByIdAsync(int id);
    Task<bool> ValidateBalanceAsync(int id,decimal amount);
    Task<IEnumerable<Account>> GetAllAsync();
}