using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Services;

namespace WeatherApp
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Other configurations...

            app.UseCors("AllowReactClient");

            // Other middleware configurations...
             app.UseRouting();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
        }
            // Other methods

        public void ConfigureServices(IServiceCollection services)
        {
            // Other configurations
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactClient", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddHttpClient<GeocodingService>();
            services.AddHttpClient<WeatherService>();
        }

   

        // Other methods
    }
}
