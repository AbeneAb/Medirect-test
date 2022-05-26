public class Transaction : EntityBase, IAggregateRoot
{
    private DateTime _createdDate;
    private DateTime? _updatedDate;
    public DateTime GetCreatedDate => _createdDate;
    public int GetBuyerId => _buyerId;
    private int _buyerId;
    private decimal _amount;
    private decimal _exchangeRate;
    private string _description;
    private decimal _converted;
    public Currency FromCurrency { get; private set; }
    public Currency ToCurrency { get; private set; }
    public TransactionStatus TransactionStatus { get; private set; }
    private int _transactionStatus;
    public Transaction()
    {
        _transactionStatus = TransactionStatus.Submitted.Id;
        _createdDate = DateTime.UtcNow;
    }
    public Transaction(int buyerId,int buyerAccount, string fromCurrency, string toCurrency,decimal amount,decimal rate, decimal converted): base()
    {
        _amount = amount;
        _exchangeRate = rate;
        _converted= converted;
        _buyerId = buyerId;
        FromCurrency = new Currency(CurrencyType.FromValue(fromCurrency));
        ToCurrency = new Currency(CurrencyType.FromValue(toCurrency));
        AddTransactionStartedDomainEvent(FromCurrency, ToCurrency,buyerId, buyerAccount);
    }
    private void AddTransactionStartedDomainEvent(Currency from, Currency to, int buyerAccount,int buyerId)
    {
        var orderStartedDomainEvent = new TransactionStartedDomainEvent(this,buyerAccount,buyerId);
        this.AddDomainEvent(orderStartedDomainEvent);
    }
    public void SetAwaitingValidationStatus()
    {
        if (_transactionStatus == TransactionStatus.Submitted.Id)
        {
            AddDomainEvent(new TransactionStatusChangedToAwaitingValidationDomainEvent(Id, _buyerId));
            _transactionStatus = TransactionStatus.AwaitingValidation.Id;
            _description = $"This transaction is awaiting Validation";
        }
    }
    public void SetBalanceConfirmedStatus()
    {
        if(_transactionStatus == TransactionStatus.AwaitingValidation.Id)
        {
            AddDomainEvent(new TransactionStatusChangedToBalanceConfirmed(Id));
            _transactionStatus= TransactionStatus.BalanceConfirmed.Id;
            _description = "The Buyer has enough balance";
        }
    }
    public void SetConvertedStatus()
    {
        if(_transactionStatus == TransactionStatus.BalanceConfirmed.Id)
        {
            AddDomainEvent(new TransactionStatusChangedToConverted(Id));
            _transactionStatus = TransactionStatus.Converted.Id;
            _description = $"Transaction has successfully converted.";
        }
    }
    public void SetCancelledStatus()
    {
        if (_transactionStatus == TransactionStatus.Converted.Id)
        {
            StatusChangeException(TransactionStatus.Cancelled);
        }

        _transactionStatus = TransactionStatus.Cancelled.Id;
        _description = $"The order was cancelled.";
        AddDomainEvent(new TransactionCancelledDomainEvent(this));
    }

    public void SetCancelledStatusWhenBalanceIsRejected()
    {
        if (_transactionStatus == TransactionStatus.AwaitingValidation.Id)
        {
            _transactionStatus = TransactionStatus.Cancelled.Id;
            _description = "The account  don't have balance";
        }
    }
    private void StatusChangeException(TransactionStatus transactionStatusToChange)
    {
        throw new TransactionDomainException($"Is not possible to change the transaction status from {TransactionStatus.Name} to {transactionStatusToChange.Name}.");
    }
}

