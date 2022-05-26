


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
                .AddCustomSwagger();
            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule());

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
            //ConfigureEventBus(app);
        }

    }
}
