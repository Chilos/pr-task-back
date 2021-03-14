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
        public string Email { get; set; }
        /// <summary>
        ///  Пароль
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }
    }
}