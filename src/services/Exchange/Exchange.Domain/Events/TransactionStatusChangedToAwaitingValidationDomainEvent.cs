public class TransactionStatusChangedToAwaitingValidationDomainEvent : INotification
{
    public int TransactionId { get; }
    public int BuyerId { get; }
    public TransactionStatusChangedToAwaitingValidationDomainEvent(int id, int buyerId)
    {
        TransactionId = id;
        BuyerId = buyerId;
    }
}