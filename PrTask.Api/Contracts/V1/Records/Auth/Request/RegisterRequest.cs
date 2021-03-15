using System.ComponentModel.DataAnnotations;

namespace PrTask.Api.Contracts.V1.Records.Auth.Request
{
    /// <summary>
    /// Запрос регистрации пользователя
    /// </summary>
    public record RegisterRequest
    {
        /// <summary>
        /// Адресс электронной почты
        /// </summary>
        [Required, EmailAddress]
        public string Email { get; set; }
        /// <summary>
        ///  Пароль
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}