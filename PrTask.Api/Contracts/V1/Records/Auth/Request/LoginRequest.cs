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
        public string Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
    }
}