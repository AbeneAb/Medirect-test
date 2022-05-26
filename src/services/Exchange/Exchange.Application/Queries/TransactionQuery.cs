using Dapper;
using Microsoft.Data.SqlClient;

namespace Exchange.Application.Queries
{
    public class TransactionQuery : ITransactionQuery
    {
        private string _connectionString = string.Empty;
        public TransactionQuery(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<TransactionSummaryViewModel>> GetOrdersFromUserAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<TransactionSummaryViewModel>("");
            }
        }

        public async Task<TransactionSummaryViewModel> GetTransactionsAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<TransactionSummaryViewModel>(@"select t.[Id] as TransactionId,
                 t.CreatedDate as TransactionDate, o.Description as Description,t.FromCurrency_Name as FromCurrency ,t.ToCurrency_Name as ToCurrency
                ,t.Amount as Amount,t.ExchangeRate as Rate,  ts.Name as status
                    FROM exchange.transactions t
                    LEFT JOIN exchange.TransactionStatus ts on o.TransactionStatus = ts.Id
                    WHERE o.Id = @id", new { id });
                return result.FirstOrDefault();
            }
        }
    }
}
