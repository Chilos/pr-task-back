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
        /// <param name="request">Регистрационные параметры <see cref="RegisterRequest"/></param>
        /// <returns>Id нового пользователя</returns>
        [HttpPost(ApiRoutes.Authentication.Register)]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model is not valid");
            }
            var user = await _userRepository.SelectUserByLogin(request.Email);
            if (user != null)
                return BadRequest("This email already exists");
            user = await _userRepository.SelectUserByLogin(request.Username);
            if (user != null)
                return BadRequest("This username already exists");
            var userId = await _userRepository.UpdateUsers(new()
                {Email = request.Email, Password = request.Password, Username = request.Username});

            return Ok(userId);
        }
        
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="request">Параметры входа в систему</param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.SelectUserByLogin(request.Login);
            if (user == null)
                return BadRequest("This login not found");
            if (user.Password != request.Password)
                return BadRequest("Password is not valid");
            return Ok(new {accessToken = "asuklhgdfkasdhf", refreshToken = "serfgsadfgerdg"});
        }
    }
}