namespace Exchange.Application.DomainEventHandlers
{
    public class TransactionCancelledDomianEventHandler : INotificationHandler<TransactionCancelledDomainEvent>
    {
        public Task Handle(TransactionCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
