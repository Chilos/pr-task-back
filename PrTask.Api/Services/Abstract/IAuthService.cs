using System;
using PrTask.Api.Models;

namespace PrTask.Api.Services.Abstract
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Получить данные авторизации
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        AuthData GetAuthData(Guid userId, string email, string username);

        /// <summary>
        /// Захешировать пароль для хранения
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(string password);

        /// <summary>
        /// Проверить пароль с хешем
        /// </summary>
        /// <param name="actualPassword"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        bool VerifyPassword(string actualPassword, string hashedPassword);

        /// <summary>
        /// Сгенерировать токен обновления
        /// </summary>
        /// <returns></returns>
        string GenerateRefreshToken();
    }
}