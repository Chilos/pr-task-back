using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CryptoHelper;
using Microsoft.IdentityModel.Tokens;
using PrTask.Api.Models;
using PrTask.Api.Services.Abstract;

namespace PrTask.Api.Services
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Получить данные авторизации
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public AuthData GetAuthData(Guid userId, string email, string username)
        {
            var expirationTime = DateTime.UtcNow.AddMinutes(AppSettings.Auth.LifespanMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserId", userId.ToString()),
                    new Claim("Email", email), 
                    new Claim("Username", username), 

                }),
                Expires = expirationTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.Auth.SecretKey)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
            
            return new AuthData
            {
                Token = token,
                TokenExpirationTime = ((DateTimeOffset)expirationTime).ToUnixTimeSeconds(),
                RefreshToken = GenerateRefreshToken()
            };
        }

        /// <summary>
        /// Сгенерировать токен обновления
        /// </summary>
        /// <returns></returns>
        public string GenerateRefreshToken()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        
        /// <summary>
        /// Захешировать пароль для хранения
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        /// <summary>
        /// Проверить пароль с хешем
        /// </summary>
        /// <param name="actualPassword"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        public bool VerifyPassword(string actualPassword, string hashedPassword)
        {
            return Crypto.VerifyHashedPassword(hashedPassword, actualPassword);
        }
    }
}