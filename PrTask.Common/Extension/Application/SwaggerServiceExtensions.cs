using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace PrTask.Common.Extension.Application
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, string applicationName = null, string applicationVersion = "1.0.0")
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen
            (
                options =>
                {
                    options.SwaggerDoc
                    (
                        $"v{applicationVersion}",
                        new OpenApiInfo
                        {
                            Title = applicationName,
                            Description = $"Swagger {applicationName}",
                            Version = $"v{applicationVersion}"
                        }
                    );

                    foreach (var fileInfo in new DirectoryInfo(AppContext.BaseDirectory).EnumerateFiles("*.xml", SearchOption.AllDirectories))
                    {
                        options.IncludeXmlComments(fileInfo.FullName, true);
                    }

                    options.DescribeAllParametersInCamelCase();

                    options.IgnoreObsoleteActions();
                    options.IgnoreObsoleteProperties();

                    options.UseInlineDefinitionsForEnums();
                    // options.UseAllOfToExtendReferenceSchemas();
                    // options.UseAllOfForInheritance();
                    // options.UseOneOfForPolymorphism();

                }
            );
            return services;
        }

        /// <summary>Использовать службу Swagger.</summary>
        /// <param name="app">Экземпляр класса реализующего <see cref="T:IApplicationBuilder" /> для расширения его этим методом.</param>
        /// <param name="applicationName">Имя приложения (имя сборки по-умолчанию).</param>
        /// <param name="applicationVersion">Версия приложения ("1.0.0" - по-умолчанию).</param>
        /// <exception cref="T:ArgumentNullException"></exception>
        public static IApplicationBuilder UseSwaggerService(this IApplicationBuilder app, string applicationName = null, string applicationVersion = "1.0.0")
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/v{applicationVersion}/swagger.json", $"{applicationName} v{applicationVersion}");
                options.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
            });
            return app;
        }
    }
}