using PlayerDuo.DTOs.Authen;
using PlayerDuo.Repositories.Authen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenRepository _authenRepository;

        public AuthenController(IAuthenRepository authenRepository)
        {
            _authenRepository = authenRepository;
        }

        [HttpPost("login/admin")]
        [AllowAnonymous]
        public async Task<ActionResult> AdminLogin(LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _authenRepository.AdminLogin(request);
            if (result == null)
            {
                return Forbid();
            }

            return Ok(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _authenRepository.Login(request);
            if (result == null)
            {
                return Unauthorized(value: "Invalid username or password.");
            }

            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var result = await _authenRepository.Register(request);
            if(result == null)
            {
                return BadRequest(error: "Username already exists or password not match");
            }

            return Ok(result);
        }

        [HttpPut("me/change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _authenRepository.ChangePassword(userId, request);

            if(result == null)
            {
                return Unauthorized("Current password invalid.");
            }
            if(result == 0)
            {
                return BadRequest(error: "Something wrong. Cannot change password.");
            }

            return Ok(result);
        }
    }
}
