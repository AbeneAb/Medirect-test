public class TransactionStatusChangedToBalanceConfirmed : INotification
{
    public int TransactionId { get;}
    public TransactionStatusChangedToBalanceConfirmed(int transactionId)
    {
        TransactionId = transactionId;
    }
}