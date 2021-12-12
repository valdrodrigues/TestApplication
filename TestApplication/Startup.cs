using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestApplication.Application;
using TestApplication.Domain.IApplication;
using TestApplication.Domain.IRepository;
using TestApplication.Infra.Configuration;
using TestApplication.Infra.Repository;

namespace TestApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // appsettings Config
            string basePath = Directory.GetCurrentDirectory();
            string environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new HostBuilder()
                .ConfigureAppConfiguration(cb =>
                {
                    cb.SetBasePath(basePath)
                      .AddJsonFile($"appsettings.{environmentName}.json", false)
                      .AddEnvironmentVariables();
                });

            services.AddControllers();

            // SwaggerGen Config
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API de teste",
                    Description = "Esta API foi desenvolvida para testes do Valdeci",
                    Contact = new OpenApiContact
                    {
                        Name = "Contact",
                        Url = new Uri("https://www.linkedin.com/in/valdeci-rodrigues-junior")
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            // Injecao Dependencia
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ISingletonCalculadorInssApplication, CalculadorInssApplication>();

            // MongoDB Config
            services.Configure<TabelaDescontoConfiguration>(Configuration.GetSection("INSSConnectionSettings"));
            services.AddSingleton<ISingletonTabelaDescontoService, TabelaDescontoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
