namespace Exchange.Application.Queries
{
    public interface IAccountQuery
    {
        Task<bool> HasEnoughBalance(int buyerId, string currency, decimal tranactionAmount);
        Task<AccountViewModel> GetBalance(int id);
        Task<IEnumerable<AccountViewModel>> GetAllAccount();
        Task<IEnumerable<AccountViewModel>> GetBalanceByUser(string name);
    }
}
