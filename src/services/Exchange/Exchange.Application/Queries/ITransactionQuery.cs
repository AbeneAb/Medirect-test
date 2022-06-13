namespace Exchange.Application.Queries
{
    public interface ITransactionQuery
    {
        Task<TransactionSummaryViewModel> GetTransactionsAsync(int id);
        Task<IEnumerable<TransactionSummaryViewModel>> GetTransactionFromUserAsync(int userId);
        Task<bool> CanMakeTransaction(int buyerId);
    }
}
