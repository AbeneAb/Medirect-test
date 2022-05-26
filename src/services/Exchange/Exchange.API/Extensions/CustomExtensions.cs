

using Microsoft.OpenApi.Models;

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

            return services;
        }


    }
}
