using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        private readonly ITransactionRepository _transactionRepository;
        public CreateTransactionCommandValidator(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
            RuleFor(c=>c.BuyerId).NotEmpty();
            RuleFor(c=>c.Amount).NotEmpty().GreaterThan(0).WithMessage("Amount should be non-negative and greater than zero");
            RuleFor(c => c.ToCurrency).NotEqual(c => c.FromCurrency).WithMessage("Currency has to be different!");
            RuleFor(b => b)
                  .MustAsync(async (entity, value, c) => await IsLessthan10TransactionInLastHour(entity))
                  .WithMessage("More than 10 transaction in the last hour.Please wait for a while");
        }
        private async Task<bool> IsLessthan10TransactionInLastHour(CreateTransactionCommand createBookingCommand)
        {
            var count = await _transactionRepository.CanMakeTransaction(createBookingCommand.BuyerId);
            return count;
        }
    }
}
