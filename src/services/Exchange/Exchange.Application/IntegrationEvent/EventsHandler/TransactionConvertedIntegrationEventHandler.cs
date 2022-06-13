namespace Exchange.Application.IntegrationEvent.EventsHandler
{
    public class TransactionConvertedIntegrationEventHandler : IIntegrationEventHandler<TransactionConvertedIntegrationEvent>
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionConvertedIntegrationEventHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task Handle(TransactionConvertedIntegrationEvent @event)
        {
            Transaction transaction = await _transactionRepository.FindByIdAsync(@event.TransactionId);
            if (transaction == null)
                throw new InvalidOperationException("Transaction not found");
            transaction.SetConvertedStatus();
            _transactionRepository.Update(transaction);
            await _transactionRepository.UnitOfWork.SaveChangesAsync();
        }
    }
}
