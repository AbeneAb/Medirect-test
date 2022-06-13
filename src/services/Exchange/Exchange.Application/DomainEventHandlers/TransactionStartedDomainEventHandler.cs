
namespace Exchange.Application.DomainEventHandlers
{
    public class TransactionStartedDomainEventHandler : INotificationHandler<TransactionStartedDomainEvent>
    {
        private readonly ILogger<TransactionStartedDomainEventHandler> _logger;
        private readonly IAccountQuery _accountQuery;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IEventBus _eventBus;
        public TransactionStartedDomainEventHandler(ILogger<TransactionStartedDomainEventHandler> handler,
            IAccountQuery accountQuery,ITransactionRepository transactionRepository,
            IEventBus eventBus)
        {
            _logger = handler;
            _accountQuery = accountQuery;
            _transactionRepository = transactionRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            
            Transaction transaction = await _transactionRepository.FindByIdAsync(notification.Transaction.Id);
            notification.Transaction.SetAwaitingValidationStatus();
            _eventBus.Publish(new TransactionAwaitingValidationIntegrationEvent(transaction.Id, transaction.GetBuyerId, transaction.FromCurrency.Name, transaction.GetAmount));
            _logger.LogTrace("Transaction with Id {Id} has been Updated.", notification.Transaction.Id);

        }
    }
}
