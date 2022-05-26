
namespace Exchange.API.AutofacModules
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<TransactionRepository>()
                .As<ITransactionRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope();

            //builder.RegisterAssemblyTypes(typeof(CreateOrderCommandHandler).GetTypeInfo().Assembly)
            //    .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));
        }
    }
}
