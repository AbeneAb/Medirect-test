namespace Exchange.Infrastructure.EntityConfigurations
{
    internal class CurrencyTypeEntityConfiguration : IEntityTypeConfiguration<CurrencyType>
    {
        public void Configure(EntityTypeBuilder<CurrencyType> builder)
        {
            builder.ToTable("CurrencyType", ExchangeContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .ValueGeneratedNever()
                .IsRequired();
            builder.Property(o => o.Symbol).IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}
