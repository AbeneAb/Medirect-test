
namespace Exchange.Infrastructure
{
    public class ExchnageContextSeed
    {
        public async Task SeedAsync(ExchangeContext context, ILogger<ExchnageContextSeed> logger)
        {
            if(!context.CurrencyTypes.Any())
            {
                context.CurrencyTypes.AddRange(CurrencyType.GetAll());
            }
            if (!context.Status.Any())
            {
                context.Status.AddRange(TransactionStatus.GetAll());
            }
            if (!context.Account.Any())
            {
                Account jonDoe = new Account("John Doe", 200.00m, "USD");
                Account janeDoe = new Account("Jane Doe", 200.00m, "GBP");
                context.Account.AddRange(jonDoe,janeDoe);
            }
            await context.SaveChangesAsync();   
        }
    }
}
