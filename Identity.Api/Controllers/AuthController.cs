using Microsoft.AspNetCore.Authorization;

namespace Identity.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService
            , ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAppSecret()
        {
            var result = await _authService.GetAppSecret();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            var result = await _authService.Login(model);
            if (result == null)
                return Unauthorized(new { message = "Unauthorized User Access" });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            bool IsUserExist = await _authService.FindUser(model.AppId, model.SecretKey);
            if (IsUserExist)
                return BadRequest(new { message = "User Already Exist" });

            bool result = await _authService.Register(model);
            if (!result)
            {
                return BadRequest(new { message = "Something Went Wrong" });
            }
            return Ok(new { message = "User Added Successfully" });
        }
    }
}
