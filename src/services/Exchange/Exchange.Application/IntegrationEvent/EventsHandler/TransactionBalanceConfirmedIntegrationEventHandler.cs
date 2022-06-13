
namespace Exchange.Application.IntegrationEvent.EventsHandler
{
    public class TransactionBalanceConfirmedIntegrationEventHandler : IIntegrationEventHandler<TransactionBalanceConfirmedIntegrationEvent>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionQuery _transactionQuery;
        private readonly IEventBus _eventBus;
        public TransactionBalanceConfirmedIntegrationEventHandler(IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,ITransactionQuery transactionQuery,IEventBus eventBus)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _transactionQuery = transactionQuery;
            _eventBus = eventBus;
        }
        public async Task Handle(TransactionBalanceConfirmedIntegrationEvent @event)
        {
            //check if account exists 
            var data = await _transactionQuery.GetTransactionsAsync(@event.TransactionId);
            // if exists add transaction balance, otherwise create account
            Account account = await _accountRepository.FindByIdandNameAsync(data.FullName, data.ToCurrency);
            if (account == null)
                _accountRepository.Add(new Account(data.FullName, data.ConvertedAmount, CurrencyType.FromDisplayName(data.ToCurrency).Id));
            else
            {
                account.UpdateBalance(data.ConvertedAmount);
                _accountRepository.Update(account);
            }
            Account accountToDeduct = await _accountRepository.FindByIdandNameAsync(data.FullName,data.FromCurrency);
            accountToDeduct.UpdateBalance(-1*data.Amount);
            _accountRepository.Update(accountToDeduct);
            await _accountRepository.UnitOfWork.SaveChangesAsync();
            _eventBus.Publish(new TransactionConvertedIntegrationEvent(@event.TransactionId));
        }
    }
}
