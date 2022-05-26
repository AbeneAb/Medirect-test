namespace Exchange.Infrastructure.EntityConfigurations
{
    internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts", ExchangeContext.DEFAULT_SCHEMA);
            builder.HasKey(ac => ac.Id);
            builder.Property(o => o.Id).UseHiLo("accountseq", ExchangeContext.DEFAULT_SCHEMA);

            builder.Property(ac => ac.FullName).IsRequired();
            builder.Property(ac => ac.Balance).HasDefaultValue(0).IsRequired();
            builder.OwnsOne(ac => ac.Currency, f =>
              {
                  f.Property<int>("AccountId").UseHiLo("accountseq", ExchangeContext.DEFAULT_SCHEMA);
                  f.WithOwner();
              });

        }
    }
}
