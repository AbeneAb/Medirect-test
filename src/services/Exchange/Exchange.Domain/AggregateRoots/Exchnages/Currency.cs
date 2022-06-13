public class Currency : ValueObject
{
    public string Name { get;  set; }
    public string Symbol { get;  set; }
    //public string Id { get; private set; }
    public Currency()
    {

    }

    public Currency(string name,string symbol,string id) : this()
    {
        Name = name;
        Symbol = symbol;
        //Id = id;
    }
    public Currency(CurrencyType currencyType)
    {
        Name = currencyType.Name;
        Symbol = currencyType.Symbol;
        //Id = currencyType.Id;   
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Symbol;
    }

}

