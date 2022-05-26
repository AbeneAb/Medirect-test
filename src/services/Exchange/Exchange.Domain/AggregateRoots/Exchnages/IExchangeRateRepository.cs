
public interface IExchangeRateRepository
{
    Task<ExchangeRate> GetExchangeRateAsync(string exchangeId);
    Task<bool> UpdateExchangeRate(ExchangeRate exchangeRates);
}

