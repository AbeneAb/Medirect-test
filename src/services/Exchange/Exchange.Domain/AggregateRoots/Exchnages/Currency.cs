public class Currency : ValueObject
{
    public string Name { get; private set; }
    public string Symbol { get; private set; }
    public Currency()
    {

    }

    public Currency(string name,string symbol) : this()
    {
        Name = name;
        Symbol = symbol;
    }
    public Currency(CurrencyType currencyType)
    {
        Name = currencyType.Name;
        Symbol = currencyType.Symbol;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Symbol;
    }

}

