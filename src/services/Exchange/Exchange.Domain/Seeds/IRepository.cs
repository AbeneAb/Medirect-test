
public interface ITransactionRepository<T> where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}

