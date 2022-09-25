using Microsoft.AspNetCore.Mvc;
using PlayerDuo.Repositories.Users;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var result = await _userRepository.GetUsers();
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
    }
}
