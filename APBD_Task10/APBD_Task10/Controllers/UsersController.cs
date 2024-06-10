using Microsoft.AspNetCore.Mvc;
using APBD_Task10.Models;
using APBD_Task10.Services;


namespace APBD_Task10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public UsersController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            await _userService.RegisterAsync(model);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            var token = _jwtService.GenerateSecurityToken(user.Username);
            var refreshToken = Guid.NewGuid().ToString();

            await _userService.SaveRefreshTokenAsync(user.Username, refreshToken);

            return Ok(new
            {
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var username = await _userService.GetUsernameByRefreshTokenAsync(model.RefreshToken);

            if (username == null)
                return Unauthorized(new { message = "Invalid refresh token" });

            var newToken = _jwtService.GenerateSecurityToken(username);

            return Ok(new
            {
                AccessToken = newToken
            });
        }
    }
}