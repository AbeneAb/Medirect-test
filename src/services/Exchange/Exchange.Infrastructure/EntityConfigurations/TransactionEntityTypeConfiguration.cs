namespace Exchange.Infrastructure.EntityConfigurations
{
    internal class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("transactions", ExchangeContext.DEFAULT_SCHEMA);
            builder.HasKey(t => t.Id);
            builder.Ignore(t => t.DomainEvents);
            builder.Property(o => o.Id).UseHiLo("transactionseq", ExchangeContext.DEFAULT_SCHEMA);
            builder.OwnsOne(t => t.FromCurrency, f =>
            {
                f.Property<int>("TransactionId").UseHiLo("transactionseq", ExchangeContext.DEFAULT_SCHEMA);
                f.WithOwner();
            });
            builder.OwnsOne(t => t.ToCurrency, f =>
            {
                f.Property<int>("TransactionId").UseHiLo("transactionseq", ExchangeContext.DEFAULT_SCHEMA);
                f.WithOwner();
            });
            builder.Property<DateTime>("_createdDate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CreatedDate")
                .IsRequired();
            builder.Property<decimal>("_amount")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Amount")
                .IsRequired();
            builder.Property<decimal>("_exchangeRate")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ExchangeRate")
                .IsRequired();
            builder.Property<decimal>("_converted").
                UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Converted");
            builder.Property<string>("_description")
                 .UsePropertyAccessMode(PropertyAccessMode.Field)
                 .HasColumnName("Description").IsRequired(false);
            builder.Property<int>("_transactionStatus")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("TransactionStatus")
                .IsRequired();

            builder.HasOne<Account>()
                .WithMany().IsRequired(false).HasForeignKey("_buyerId");

            builder.HasOne(o => o.TransactionStatus)
                .WithMany()
                .HasForeignKey("_transactionStatus");
        }
    }
}
