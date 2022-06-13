namespace Exchange.Application.Queries
{
    public class AccountQuery : IAccountQuery
    {
        private string _connectionString = string.Empty;
        public AccountQuery(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<AccountViewModel>> GetAllAccount()
        {
            using(var connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();
                var result = await connection.QueryAsync<AccountViewModel>(@"select a.* from [exchange].[Accounts] as a");
                return result;
            }
        }

        public async Task<AccountViewModel> GetBalance(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<AccountViewModel>(@"select a.* from [exchange].[Accounts] as a where
                   a.[Id] = @id", new { id });
                return result.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<AccountViewModel>> GetBalanceByUser(string name)
        {
            using (var connection = new SqlConnection(_connectionString)) 
            {
                connection.Open();
                var result = await connection.QueryAsync<AccountViewModel>(@"select a.* from [exchange].[Accounts] as a where
                   a.[FullName] = @name", new { name });
                return result;
            }
        }

        public async Task<bool> HasEnoughBalance(int buyerId, string currency,decimal tranactionAmount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<decimal>(@"select a.[Balance] from [exchange].[Accounts] as a 
                 where a.[Id] = @buyerId and a.[Currency_Name] = @currency", new { buyerId, currency });
                return result.FirstOrDefault() - tranactionAmount >= 0;
            }
        }
    }
}
