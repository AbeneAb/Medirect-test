namespace Exchange.Application.IntegrationEvent.EventsHandler
{
    public class TransactionStartedIntegrationEventHandler : IIntegrationEventHandler<TransactionAwaitingValidationIntegrationEvent>
    {
        private readonly IAccountQuery _accountQuery;
        private readonly IEventBus _eventBus;
        private readonly ITransactionRepository _transactionRepository;
        public TransactionStartedIntegrationEventHandler(IAccountQuery accountQuery,
            IEventBus eventBus, ITransactionRepository transactionRepository)
        {
            _accountQuery = accountQuery;
            _eventBus = eventBus;
            _transactionRepository = transactionRepository;
        }
        public async Task Handle(TransactionAwaitingValidationIntegrationEvent @event)
        {
            var hasBalance = await _accountQuery.HasEnoughBalance(@event.BuyerId, @event.Currency, @event.Amount);
            Transaction transaction = await _transactionRepository.FindByIdAsync(@event.TransactionId);
            if (!hasBalance)
            {
                
                _eventBus.Publish(new TransactionCancelledIntegrationEvent(transaction));
                throw new TransactionDomainException("Insufficent Balance fot the selected currency!");
            }
            transaction.SetBalanceConfirmedStatus();
            _transactionRepository.Update(transaction);
            await _transactionRepository.UnitOfWork.SaveChangesAsync();
            _eventBus.Publish(new TransactionBalanceConfirmedIntegrationEvent(@event.TransactionId,@event.Currency,transaction.GetBuyerId));
        }
    }
}
