using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;
using IDatabase = StackExchange.Redis.IDatabase;

namespace Exchange.Infrastructure.Repositories
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly ILogger<ExchangeRateRepository> _logger;
        private readonly IDatabase _database;
        private readonly ConnectionMultiplexer _redis;

        public ExchangeRateRepository(ILoggerFactory loggerFactory,ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<ExchangeRateRepository>();
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<ExchangeRate> GetExchangeRateAsync(string exchangeId)
        {
            var data = await _database.StringGetAsync(exchangeId);

            if (data.IsNullOrEmpty)
            {
                return null;
            }

            return JsonSerializer.Deserialize<ExchangeRate>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        public async Task<bool> UpdateExchangeRate(ExchangeRate exchangeRate)
        {
            var created = await _database.StringSetAsync(exchangeRate.Currency.Name, JsonSerializer.Serialize(exchangeRate));

            if (!created)
            {
                _logger.LogInformation("Problem occur persisting the item.");
                return false;
            }

            _logger.LogInformation("Basket item persisted succesfully.");
            return true;
        }
    }
}
