namespace Exchange.Application.Commands
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, bool>
    {
        private readonly IAccountRepository _accountRepository;
        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<bool> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var hasAccount = await _accountRepository.FindByIdandNameAsync(request.FullName, CurrencyType.FromValue(request.Currency).Name);
            if (hasAccount is not null)
                throw new TransactionDomainException("User has account for that currency");
            Account account = new Account(request.FullName, request.Balance, request.Currency);
            _accountRepository.Add(account);
            return await _accountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
