public class ExchangeRate: EntityBase
{
    public Currency Base { get; set; }
    public DateTime DateOnly { get; set; }
    public DateTime Timestamp { get; set; }
    public Currency Currency { get; set; }
    public decimal Rate { get; set; }
    public ExchangeRate()
    {

    }
    public ExchangeRate(string baseCurrency,DateTime dateOnly, DateTime timeStamp,string currency,decimal rate)
    {
        Base = new Currency(CurrencyType.FromValue(baseCurrency));
        DateOnly = dateOnly;
        Timestamp = timeStamp;
        Currency = new Currency(CurrencyType.FromValue(currency));
        Rate = rate;
    }

}