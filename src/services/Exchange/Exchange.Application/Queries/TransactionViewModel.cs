namespace Exchange.Application.Queries
{
    public record TransactionSummaryViewModel
    {
        public string TransactionId { get; init; }
        public DateTime TransactionDate { get; init; }
        public string Description { get; init; }
        public decimal Amount { get; init; }
        public decimal Rate { get; init; }
        public string FromCurrency { get; init; }
        public string ToCurrency { get; init; }
        public string FullName { get; init; }
        public decimal ConvertedAmount  { get; init; }
        public string Status { get; init; }
    }

}
