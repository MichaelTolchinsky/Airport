using BL.Services;
using Common.Api;
using Common.Repositories;
using Dal;
using Dal.Repositories;
using FinalProjectServer.Hubs;
using FinalProjectServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace FinalProjectServer
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
            services.AddDbContext<AirportDbContext>(options =>
                {
                    string relativeDataSource = Path.Combine(Environment.CurrentDirectory, @"..\", "DAL", "airport.db");
                    string dataSource = Path.GetFullPath(relativeDataSource);
                    options.UseSqlite($"Data Source={dataSource}");
                    options.ConfigureWarnings(warn => warn.Ignore(CoreEventId.LazyLoadOnDisposedContextWarning));
                });

            services.AddSingleton<IAirportService, AirportService>();
            services.AddSingleton<IAirportNotifierService, AirportNotifierService>();
            services.AddSingleton<IDbNotifier, DbNotifierService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddControllers().AddNewtonsoftJson();
            services.AddSignalR(opt => opt.EnableDetailedErrors = true);
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:8080")
                    .WithMethods("GET", "POST").AllowAnyHeader().AllowCredentials();
            }));
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<AirportHub>(@"/AirportHub");
            });
        }
    }
}
