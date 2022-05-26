public class TransactionCancelledDomainEvent : INotification
{
    public Transaction TransactionId { get; }
    public TransactionCancelledDomainEvent(Transaction transaction)
    {
        TransactionId = transaction; 
    }
}