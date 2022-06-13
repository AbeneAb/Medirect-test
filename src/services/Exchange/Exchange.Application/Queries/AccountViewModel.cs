namespace Exchange.Application.Queries
{
    public record AccountViewModel
    {
        public string Id { get; init; }
        public string FullName { get; init; }
        public string Currency_Name { get; init; }
        public string Currency_Symbol { get; init; }
        public decimal Balance { get; init; }
    }
}
