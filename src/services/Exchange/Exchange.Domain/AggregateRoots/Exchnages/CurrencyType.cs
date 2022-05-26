
public class CurrencyType : IComparable
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Symbol { get; private set; }

    //for this project, I decided to have just those 4 currencies. If required, we can add more.
    public static CurrencyType USD = new("USD", "United States Dollar", "$");
    public static CurrencyType EU = new("EUR", "Euro", "€");
    public static CurrencyType JPY = new("JPY", "Japanese Yen", "¥");
    public static CurrencyType GBP = new CurrencyType("GBP", "British Pound", "£");

    public CurrencyType(string id, string name,string symbol) => (Id, Name,Symbol) = (id, name,symbol);

    public override string ToString() => Name;

    public static IEnumerable<CurrencyType> GetAll()  =>
        typeof(CurrencyType).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                    .Select(f => f.GetValue(null))
                    .Cast<CurrencyType>();

    public override bool Equals(object obj)
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static CurrencyType FromValue(string value)
    {
        var matchingItem = Parse(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static CurrencyType FromDisplayName(string displayName)
    {
        var matchingItem = Parse(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }
    public static CurrencyType FromSymbol(string symbol)
    {
        var matchingItem = Parse(symbol, "symbol", item => item.Symbol == symbol);
        return matchingItem;
    }

    private static CurrencyType Parse<K>(K value, string description, Func<CurrencyType, bool> predicate)
    {
        var matchingItem = GetAll().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(CurrencyType)}");

        return matchingItem;
    }

    public int CompareTo(object other) => Id.CompareTo(((CurrencyType)other).Id);
}