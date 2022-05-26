namespace Exchange.Application.Queries
{
    public interface ITransactionQuery
    {
        Task<TransactionSummaryViewModel> GetTransactionsAsync(int id);
        Task<IEnumerable<TransactionSummaryViewModel>> GetOrdersFromUserAsync(int userId);
    }
}
