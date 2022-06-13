namespace Exchange.Application.IntegrationEvent.Events
{
    public record TransactionConvertedIntegrationEvent : Event
    {
        public int TransactionId { get; set; }

        public TransactionConvertedIntegrationEvent(int transactionId)
        {
            TransactionId = transactionId;
        }
    }
}
