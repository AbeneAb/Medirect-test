namespace Exchange.Application.Commands
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(c => c.FullName).NotEmpty();
            RuleFor(c=>c.Currency).NotEmpty();
            RuleFor(c => c.Balance).NotEmpty().GreaterThanOrEqualTo(0).WithMessage("Amount should be non-negative and greater than zero");
        }
    }
}
