namespace Exchange.Application.Commands
{
    public class CreateAccountCommand : IRequest<bool>
    {
        public string FullName { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
    }
}
