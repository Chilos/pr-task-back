using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrTask.Api.Contracts.V1;
using PrTask.Api.Contracts.V1.Records.Auth.Request;
using PrTask.DAL.Repositories.Abstract;

namespace PrTask.Api.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с аутентификацией
    /// </summary>
    public class AuthController: Controller
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Authentication.Register)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userId = await _userRepository.UpdateUsers(new()
                {Email = request.Email, Password = request.Password, Username = request.Username});

            return Ok(userId);
        }
    }
}