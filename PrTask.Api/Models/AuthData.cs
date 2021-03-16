using System;

namespace PrTask.Api.Models
{
    /// <summary>
    /// Данные авторизации
    /// </summary>
    public class AuthData
    {
        /// <summary>
        /// Токен авторизации
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Время жизни токена авторизации
        /// </summary>
        public long TokenExpirationTime { get; set; }
        /// <summary>
        /// Токен обновления токена авторизации
        /// </summary>
        public string RefreshToken { get; set; }
    }
}