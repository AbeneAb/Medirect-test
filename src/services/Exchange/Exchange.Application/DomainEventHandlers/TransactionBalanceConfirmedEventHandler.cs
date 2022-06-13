namespace Exchange.Application.DomainEventHandlers
{
    public class TransactionBalanceConfirmedEventHandler : INotificationHandler<TransactionStatusChangedToBalanceConfirmed>
    {
        public Task Handle(TransactionStatusChangedToBalanceConfirmed notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
