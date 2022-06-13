
namespace Exchange.Application.IntegrationEvent
{
    public class TransactionEventService : ITransactionEventService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<TransactionEventService> _logger;
        public TransactionEventService(IEventBus eventBus, ILogger<TransactionEventService> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
        }
        public Task AddAndSaveEventAsync(Event evt)
        {
            throw new NotImplementedException();
        }

        public Task PublishEventsThroughEventBusAsync(Guid transactionId)
        {
            throw new NotImplementedException();
        }
    }
}
