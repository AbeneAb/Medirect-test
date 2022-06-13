namespace Exchange.Application.IntegrationEvent.Events
{
    public record TransactionAwaitingValidationIntegrationEvent : Event
    {
        public int TransactionId { get; set; }
        public int BuyerId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public TransactionAwaitingValidationIntegrationEvent(int transactionId, int buyerId, string currency, decimal amount)
        {
            BuyerId = buyerId;
            Currency = currency;    
            Amount = amount;
            TransactionId = transactionId;
        }
    }
}
