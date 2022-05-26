
public class TransactionStartedDomainEvent : INotification
{
    public Transaction Transaction { get; }
    public int BuyerId { get; private set; }
    public int BuyerAccount { get; private set; }
    public TransactionStartedDomainEvent(Transaction transaction, int buyerId, int buyerAccount)
    {
        Transaction = transaction;
        BuyerId = buyerId;
        BuyerAccount = buyerAccount;
    }
}