public class TransactionStatus : Enumeration
{
    public static TransactionStatus Submitted = new TransactionStatus(1, nameof(Submitted).ToLowerInvariant());
    public static TransactionStatus AwaitingValidation = new TransactionStatus(2, nameof(AwaitingValidation).ToLowerInvariant());
    public static TransactionStatus BalanceConfirmed = new TransactionStatus(3,nameof(BalanceConfirmed).ToLowerInvariant());
    public static TransactionStatus Converted = new TransactionStatus(4, nameof(Converted).ToLowerInvariant());
    public static TransactionStatus Cancelled = new TransactionStatus(5, nameof(Cancelled).ToLowerInvariant());
    public TransactionStatus(int id, string name) : base(id, name)
    {
    }
}