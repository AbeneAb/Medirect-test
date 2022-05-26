namespace Exchange.Application.DomainEventHandlers
{
    public class TransactionStartedDomainEventHandler : INotificationHandler<TransactionStartedDomainEvent>
    {
        public Task Handle(TransactionStartedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
