using Microsoft.Extensions.Logging;

namespace Exchange.Application.Commands
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, bool>
    {
        private readonly ILogger<CreateTransactionCommandHandler> _logger;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IExchangeRateRepository _exchangeRateRepository;
        public CreateTransactionCommandHandler(ITransactionRepository transactionRepository,
            IAccountRepository accountRepository,IExchangeRateRepository exchangeRateRepository,
            ILogger<CreateTransactionCommandHandler> logger)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _exchangeRateRepository = exchangeRateRepository;
            _logger = logger;
        }
        public async Task<bool> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _accountRepository.FindByIdAsync(request.BuyerId);
            if (buyer is null)
                throw new TransactionDomainException("Account not found!");
            var rate = await _exchangeRateRepository.GetExchangeRateAsync(request.FromCurrency);
            if (rate is null)
                throw new TransactionDomainException("Exchange rate not found!");
            var exchange = rate.Rate * request.Amount;
            var transaction = new Transaction(buyer.Id, buyer.Id, request.FromCurrency, request.ToCurrency,request.Amount,rate.Rate,exchange);
            _transactionRepository.Add(transaction);
            _logger.LogInformation("----- Creating Transaction - Transaction: {@Transaction}", transaction);

            return await _transactionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
