namespace Exchange.Application.Queries
{
    public class TransactionQuery : ITransactionQuery
    {
        private string _connectionString = string.Empty;
        public TransactionQuery(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> CanMakeTransaction(int buyerId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result= await connection.QueryAsync<int>(@"select COUNT(t.[Id]) as value from exchange.transactions as t  where t.[CreatedDate] <= DATEADD(HOUR, -1, GETUTCDATE()) and  t.[_buyerId] = @buyerId", new {buyerId});
                return result.FirstOrDefault() < 10;
            }
        }

        public async Task<IEnumerable<TransactionSummaryViewModel>> GetTransactionFromUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<TransactionSummaryViewModel>(@"select t.[Id] as TransactionId,
                 t.CreatedDate as TransactionDate,a.FullName, ts.[Name] as Status,t.FromCurrency_Name as FromCurrency ,t.ToCurrency_Name as ToCurrency
                ,t.Amount as Amount,t.ExchangeRate as Rate, t.[Converted] as ConvertedAmount,t.[Description]
                    FROM exchange.transactions t
                    inner JOIN exchange.TransactionStatus ts on ts.[id] = t.[TransactionStatus]
					inner join exchange.Accounts as a on t.[_buyerId] = a.Id
                    WHERE t.[_buyerId] = @userId", new { userId });
                
            }
        }

        public async Task<TransactionSummaryViewModel> GetTransactionsAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<TransactionSummaryViewModel>(@"select t.[Id] as TransactionId,
                 t.CreatedDate as TransactionDate,a.FullName, ts.[Name] as Status,t.FromCurrency_Name as FromCurrency ,t.ToCurrency_Name as ToCurrency
                ,t.Amount as Amount,t.ExchangeRate as Rate, t.[Converted] as ConvertedAmount,t.[Description]
                    FROM exchange.transactions t
                    inner JOIN exchange.TransactionStatus ts on ts.[id] = t.[TransactionStatus]
					inner join exchange.Accounts as a on t.[_buyerId] = a.Id
                    WHERE t.[Id] = @id", new { id });
                return result.FirstOrDefault();
            }
        }
    }
}
