using System;
using Microsoft.Extensions.Configuration;

namespace PrTask.Api.Models
{
    /// <summary>
    /// Класс для доступа к AppSettingsJson
    /// </summary>
    public static class AppSettings
    {
        /// <summary>
        /// Секция настроек аутентификации 
        /// </summary>
        public static class Auth
        {
            /// <summary>
            /// Ключ шифрования токена авторизации
            /// </summary>
            public static string SecretKey { get; private set; }
            /// <summary>
            /// Инициализация cекции настроек аутентификации 
            /// </summary>
            public static void Init()
            {
                SecretKey = Configuration["Auth:SecretKey"];
            }
        }
        
        /// <summary>Класс настроек тестирования.</summary>
        public static class Testing
        {
            /// <summary>Признак использования службы Swagger.</summary>
            public static bool IsUseSwagger { get; private set; }

            /// <summary>Инициализировать данные класса.</summary>
            internal static void Init()
            {
                IsUseSwagger = Configuration.GetValue<bool>("Testing:IsUseSwagger");
            }
        }
        
        /// <summary>
        /// Секция настроек базы данных
        /// </summary>
        public static class Database
        {
            /// <summary>
            /// Строка подключения
            /// </summary>
            public static string ConnectionString { get; private set; }
            /// <summary>
            /// Timeout запроса бд
            /// </summary>
            public static  int CommandTimeoutSeconds { get; private set; }
            internal static void Init()
            {
                ConnectionString = Configuration["Database:ConnectionString"];
                CommandTimeoutSeconds = Configuration.GetValue<int>("Database:CommandTimeoutSeconds");
            }
        }

        /// <summary>
        /// Конфигурации
        /// </summary>
        public static IConfiguration Configuration { get; private set; }
        /// <summary>
        /// Инициализация класса настроек
        /// </summary>
        public static void Init(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Auth.Init();
            Testing.Init();
            Database.Init();
        }
    }
}