namespace Exchange.Application.IntegrationEvent.Events
{
    public record TransactionBalanceConfirmedIntegrationEvent : Event
    {
        public int TransactionId { get; set; }
        public string Currency { get; set; }
        public int BuyerId { get; set; }
        public TransactionBalanceConfirmedIntegrationEvent(int transactionId,string currency, int buyerId)
        {
            TransactionId = transactionId;
            Currency = currency;
            BuyerId = buyerId;

        }
    }
}
