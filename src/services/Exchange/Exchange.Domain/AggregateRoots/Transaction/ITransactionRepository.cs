public interface ITransactionRepository : ITransactionRepository<Transaction>
{
    Transaction Add(Transaction transaction);
    Transaction Update(Transaction transaction);
    Task<IEnumerable<Transaction>> FindByBuyerAsync(int buyerId);
    Task<bool> CanMakeTransaction(int buyerId);
    Task<Transaction> FindByIdAsync(int id);
}

