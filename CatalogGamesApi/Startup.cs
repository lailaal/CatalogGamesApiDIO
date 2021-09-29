using CatalogGamesApi.Repositories;
using CatalogGames.Repositories;
using CatalogGames.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace CatalogGamesApi
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
            services.AddScoped<CatalogGames.Services.IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoRepository>();

           

            services.AddSingleton<CatalogGames.Controllers.V1.IExemploSingleton, CatalogGames.Controllers.V1.ExemploCicloDeVida>();
            services.AddScoped<CatalogGames.Controllers.V1.IExemploScoped, CatalogGames.Controllers.V1.ExemploCicloDeVida>();
            services.AddTransient<CatalogGames.Controllers.V1.IExemploTransient, CatalogGames.Controllers.V1.ExemploCicloDeVida>();

            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogGames", Version = "v1" });

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", " CatalogGames v1"));
            }

            app.UseMiddleware<Middleware.ExceptionMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
