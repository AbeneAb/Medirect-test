using System.Text.Json.Serialization;

namespace Exchange.API.BackgroundTask
{
    public class CurrencyLoader : BackgroundService
    {
        private readonly ILogger<CurrencyLoader> _logger;
        private readonly IExchangeRateRepository _exchangeRateRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private Timer _timer;
        public CurrencyLoader(IExchangeRateRepository exchangeRateRepository, IConfiguration configuration, ILogger<CurrencyLoader> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["RateProvider:URL"]);
            _httpClient.DefaultRequestHeaders.Add("apikey", _configuration["RateProvider:token"]);
            _exchangeRateRepository = exchangeRateRepository;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed hosted service starting");
            _timer = new Timer(async o => await LoadCurrency(o), null, TimeSpan.Zero, TimeSpan.FromMinutes(28));
        }
        private async Task LoadCurrency(object? obj)
        {
            var response = await _httpClient.GetAsync($"?base={_configuration["RateProvider:base"]}&symbols=EUR,GBP,JPY,USD");
            var data = await response.Content.ReadFromJsonAsync<CurrencyAPIResponse>();
            await StoreInCache(data);
        }
        private async Task StoreInCache(CurrencyAPIResponse response) 
        {
            foreach(var item in response.rates)
            {
                ExchangeRate rate = new ExchangeRate(response.Base,response.date, new DateTime(response.timeStamp), item.Key, item.Value);
                var saved = await _exchangeRateRepository.UpdateExchangeRate(rate);
            }
        }



    }
    public record CurrencyAPIResponse
    {
        public Int32 timeStamp { get; set; }
        [JsonPropertyName("base")]
        public string Base { get; set; }
        public DateTime date { get; set; }
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> rates { get; init; }
        public CurrencyAPIResponse()
        {
        }

    }
}
   