using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RouletteBettingAPI.Business.Implementation;
using RouletteBettingAPI.Business.Interfaces;
using RouletteBettingAPI.CrossCutting.Implementation;
using RouletteBettingAPI.CrossCutting.Interfaces;

namespace RouletteBettingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IRouletteBusiness, RouletteBusiness>();
            services.AddScoped<IValidParametersRequestEndpoints, ValidParametersRequestEndpoints>();
            services.AddSingleton<IRedisCachingStorage, RedisCachingStorage>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = Configuration.GetValue<string>("SwaggerSettings: Tittle"),
                    Version = Configuration.GetValue<string>("SwaggerSettings: Version"),
                    Description = Configuration.GetValue<string>("SwaggerSettings: Description"),
                });
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings: ConnectionString");
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette Betting API");
            });
        }
    }
}
