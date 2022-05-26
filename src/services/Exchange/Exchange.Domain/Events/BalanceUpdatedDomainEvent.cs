public class BalanceUpdatedDomainEvent : INotification
{
    public Account Balance { get; }
    public BalanceUpdatedDomainEvent(Account balance)
    {
        Balance = balance;
    }
}