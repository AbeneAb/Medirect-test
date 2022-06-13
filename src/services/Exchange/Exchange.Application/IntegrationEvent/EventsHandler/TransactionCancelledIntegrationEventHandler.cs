namespace Exchange.Application.IntegrationEvent.EventsHandler
{
    public class TransactionCancelledIntegrationEventHandler : IIntegrationEventHandler<TransactionCancelledIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionCancelledIntegrationEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository; 
        }
        public Task Handle(TransactionCancelledIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
