

using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using RabbitMQEventbus;
using RabbitMQEventbus.RabbitMQ;

namespace Exchange.API.Extensions
{
    public static class CustomExtensions
    {
        public static IServiceCollection AddDbContext(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExchangeContext>(options => options.UseSqlServer(configuration.GetConnectionString("ExchangeConnectionString")));
            return services;
        }
        public static IServiceCollection AddCustomMVC(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MeDirect - Exchange HTTP API",
                    Version = "v1",
                    Description = "The Exchange Service HTTP API"
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services,IConfiguration Configuration)
        {
            services.AddSingleton<IRabbitMQPersistentConnection>(s => {
                var logger = s.GetRequiredService<ILogger<RabbitMQPersistentConnection>>();
                var factory = new ConnectionFactory()
                {
                    HostName = Configuration["RabbitMq:Hostname"],
                    DispatchConsumersAsync = true,
                    UserName = Configuration["RabbitMq:UserName"],
                    Password = Configuration["RabbitMq:Password"],
                    Port = int.Parse(Configuration["RabbitMq:Port"])
                };
                var retryCount = 5;
                return new RabbitMQPersistentConnection(factory, logger, retryCount);
            });
            services.AddSingleton<IEventBus, RabbitMQEventBus>(sp =>
            {
                var subscriptionClientName = Configuration["RabbitMq:SubscriptionClientName"];
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<RabbitMQEventBus>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var ilifeTimeScope = sp.GetRequiredService<ILifetimeScope>();
                var retryCount = 5;
                if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                {
                    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                }

                return new RabbitMQEventBus(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager, ilifeTimeScope, subscriptionClientName, retryCount);
            });
            services.AddSingleton<IEventBusSubscriptionsManager, EventBusSubscriptionsManager>();
            return services;
        }


    }
}
