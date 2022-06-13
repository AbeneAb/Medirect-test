


namespace Exchange.API.AutofacModules
{
    public class InfrastructureModule : Autofac.Module
    {
        public string ConnectionString { get; }
        public InfrastructureModule(string connectionString)
        {
            ConnectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c=> new TransactionQuery(ConnectionString))
                .As<ITransactionQuery>()
                .InstancePerLifetimeScope();
            builder.Register(c => new AccountQuery(ConnectionString))
              .As<IAccountQuery>()
              .InstancePerLifetimeScope();
            builder.RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExchangeRateRepository>()
                .As<IExchangeRateRepository>()
                .InstancePerLifetimeScope();
          builder.RegisterAssemblyTypes(typeof(TransactionBalanceConfirmedIntegrationEventHandler)
              .GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IIntegrationEventHandler<>));

            
        }
    }
}
