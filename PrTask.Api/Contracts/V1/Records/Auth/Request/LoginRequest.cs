using System.ComponentModel.DataAnnotations;

namespace PrTask.Api.Contracts.V1.Records.Auth.Request
{
    /// <summary>
    /// Запрос логина пользователя
    /// </summary>
    public record LoginRequest
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}