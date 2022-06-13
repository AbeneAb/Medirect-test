



using Exchange.Application.IntegrationEvent.Events;
using StackExchange.Redis;

namespace Exchange.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
           
        }
        public virtual IServiceProvider ConfigureServices(IServiceCollection services) 
        {
            services.AddDbContext(Configuration)
                .AddCustomMVC()
                .AddCustomSwagger()
                .AddRabbitMQ(Configuration)
                .AddHostedService<CurrencyLoader>();
            services.AddSingleton<ConnectionMultiplexer>(sp =>
            {
                var configuration = ConfigurationOptions.Parse(Configuration["RedisConnectionString"], true);

                return ConnectionMultiplexer.Connect(configuration);
            });

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule(Configuration["ConnectionStrings:ExchangeConnectionString"]));
            return new AutofacServiceProvider(container.Build());
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())

            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Exchnage API v1"));
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapHub<NotificationsHub>("/hub/notificationhub");

            });
            ConfigureEventBus(app);
        }
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransactionAwaitingValidationIntegrationEvent, IIntegrationEventHandler<TransactionAwaitingValidationIntegrationEvent>>();
            eventBus.Subscribe<TransactionBalanceConfirmedIntegrationEvent,IIntegrationEventHandler<TransactionBalanceConfirmedIntegrationEvent>>();
            eventBus.Subscribe<TransactionCancelledIntegrationEvent,IIntegrationEventHandler<TransactionCancelledIntegrationEvent>>();  
            eventBus.Subscribe<TransactionConvertedIntegrationEvent,IIntegrationEventHandler<TransactionConvertedIntegrationEvent>>();

        }

    }
}
