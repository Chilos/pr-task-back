using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PrTask.Common.Extension.Application
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticateService(this IServiceCollection services, string jwtSecretKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("token", options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
                        };
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                var accessToken = context.Request.Headers["access-token"];
                                if (!string.IsNullOrEmpty(accessToken))
                                    context.Token = accessToken;
                                return Task.CompletedTask;
                            }
                        };
                    }
                );
            return services;
        }
    }
}