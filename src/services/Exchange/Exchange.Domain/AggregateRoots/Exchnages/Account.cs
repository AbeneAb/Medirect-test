public  class Account : EntityBase, IAggregateRoot 
{
    public string FullName { get; set; }
    public decimal Balance { get; set; }
    public Currency Currency { get; set; }
    public Account()
    {

    }
    public Account(string fullName, decimal balance, string currencyId)
    {
        Currency = new Currency(CurrencyType.FromValue(currencyId));
        FullName = fullName;
        Balance = balance;
    }
}

