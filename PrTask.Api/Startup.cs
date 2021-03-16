using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrTask.Api.Models;
using PrTask.Api.Services;
using PrTask.Api.Services.Abstract;
using PrTask.Common.Extension.Application;
using PrTask.DAL;
using PrTask.DAL.Repositories;
using PrTask.DAL.Repositories.Abstract;

namespace PrTask.Api
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            AppSettings.Init(configuration);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrTaskSqlContext>
            (
                options =>
                {
                    options.UseSqlServer
                    (
                        AppSettings.Database.ConnectionString,
                        dbContextOptionsBuilder => dbContextOptionsBuilder.CommandTimeout(AppSettings.Database.CommandTimeoutSeconds)
                    );
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                }
            );
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthenticateService(AppSettings.Auth.SecretKey);
            services.AddSwaggerService();
            services.AddHealthChecks();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks();
            if (AppSettings.Testing.IsUseSwagger)
            {
                app.UseSwaggerService("Pr.Task", "1.0.0");
            }
            app.UseRouting();
            

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}