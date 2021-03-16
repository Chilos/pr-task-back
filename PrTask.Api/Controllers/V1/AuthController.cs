using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrTask.Api.Contracts.V1;
using PrTask.Api.Contracts.V1.Records.Auth.Request;
using PrTask.Api.Models;
using PrTask.Api.Services.Abstract;
using PrTask.DAL.Repositories.Abstract;

namespace PrTask.Api.Controllers.V1
{
    /// <summary>
    /// Контроллер для работы с аутентификацией
    /// </summary>
    public class AuthController: Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _auth;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AuthController(IUserRepository userRepository, IAuthService auth)
        {
            _userRepository = userRepository;
            _auth = auth;
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
                return BadRequest(ModelState);
            
            var user = await _userRepository.SelectUserByLogin(request.Email);
            if (user != null)
                return BadRequest("This email already exists");
            user = await _userRepository.SelectUserByLogin(request.Username);
            if (user != null)
                return BadRequest("This username already exists");
            var savedUser = await _userRepository.UpdateUsers(new()
                {Email = request.Email, Password = _auth.HashPassword(request.Password), Username = request.Username, RefreshToken = _auth.GenerateRefreshToken()});

            return Ok(_auth.GetAuthData(savedUser.Id, savedUser.Email, savedUser.Username));
        }
        
        /// <summary>
        /// Вход в систему
        /// </summary>
        /// <param name="request">Параметры входа в систему</param>
        /// <returns></returns>
        [HttpPost(ApiRoutes.Authentication.Login)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var user = await _userRepository.SelectUserByLogin(request.Login);
            if (user == null)
                return Unauthorized("This login not found");
            if (!_auth.VerifyPassword(request.Password, user.Password))
                return Unauthorized("Invalid password");
            return Ok(_auth.GetAuthData(user.Id, user.Email, user.Username));
        }
    }
}