using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace PrTask.Common.Extension.Application
{
    public static class ApiHealthChecksExtensions
    {
        /// <summary>Использовать проверку доступности API.</summary>
        /// <param name="app">Экземпляр класса реализующего <see cref="T:IApplicationBuilder" /> для расширения его этим методом.</param>
        /// <param name="path">Путь ("/api/health-check" - по умолчанию).</param>
        /// <exception cref="T:ArgumentNullException"></exception>
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app, string path = "/api/health-check")
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseHealthChecks
            (
                path,
                new HealthCheckOptions
                {
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        var result = JsonConvert.SerializeObject
                        (
                            new
                            {
                                status = report.Status.ToString(),
                                errors = report.Entries.Select
                                (
                                    keyValuePair => new
                                    {
                                        key = keyValuePair.Key,
                                        value = Enum.GetName(typeof(HealthStatus), keyValuePair.Value.Status)
                                    }
                                )
                            }
                        );
                        await context.Response.WriteAsync(result);
                    }
                }
            );
            return app;
        }
    }
}