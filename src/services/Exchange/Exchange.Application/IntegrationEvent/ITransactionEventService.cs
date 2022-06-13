
namespace Exchange.Application.IntegrationEvent
{
    public interface ITransactionEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(Event evt);
    }
}
