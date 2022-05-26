public class TransactionStatusChangedToConverted : INotification
{
    public int TransactionId { get; }
    public TransactionStatusChangedToConverted(int id)
    {
        TransactionId = id;
    }
}