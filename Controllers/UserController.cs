using PlayerDuo.DTOs.Users;
using PlayerDuo.Repositories.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _userRepository.GetUserById(userId);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] UpdateUserRequest request)
        {
            try
            {
                var claimsPrincipal = this.User;
                var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

                var result = await _userRepository.UpdateUser(userId, request);
                if (result == null)
                {
                    return NotFound();
                }

                var updatedUser = await _userRepository.GetUserById(userId);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpGet("me/check-enabled")]
        [Authorize]
        public async Task<ActionResult> CheckUserEnabled()
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _userRepository.CheckUserEnabled(userId);
            if (result == null)
            {
                return Forbid();
            }

            return Ok(new { isEnabled = result });
        }

        // ADMIN
        [HttpGet("manage")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersManageRequest request)
        {
            try
            {
                var result = await _userRepository.GetUsers(request);

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{userId}/disable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableUser(int userId)
        {
            try
            {
                var result = await _userRepository.DisableUser(userId);
                if (result == 0)
                {
                    return NotFound();
                }
                var updatedUser = await _userRepository.GetUserById(result);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{userId}/enable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnableUser(int userId)
        {
            try
            {
                var result = await _userRepository.EnableUser(userId);
                if (result == 0)
                {
                    return NotFound();
                }
                var updatedUser = await _userRepository.GetUserById(result);

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }
    }
}
