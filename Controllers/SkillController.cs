using PlayerDuo.DTOs.Skills;
using PlayerDuo.Repositories.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillRepository _skillRepository;

        public SkillsController(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        [HttpPost("me")]
        [Authorize]
        public async Task<ActionResult> CreateSkill([FromForm] CreateSkillRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);

            var result = await _skillRepository.CreateSkill(userId, request);

            if(result.IsSuccessed == false)
            {
                return BadRequest(error: result.Message);
            }

            return Ok(result.Message);
        }

        [HttpGet("{skillId}")]
        public async Task<ActionResult> GetSkillBySkillId(int skillId)
        {
            var result = await _skillRepository.GetSkillById(skillId);
            if(result.ResultObj == null)
            {
                return NoContent();
            }

            return Ok(result.ResultObj);
        }

        [HttpPost("")]
        public async Task<ActionResult> GetSkillsByUserId(GetSkillRequest request)
        {
            var result = await _skillRepository.GetSkills(request.UserId, request.IsEnabled);;
            if (result.ResultObj == null)
            {
                return NoContent();
            }

            return Ok(result.ResultObj);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult> GetSkill()
        {
            var claimsPrincipal = this.User;
            var userId = Int32.Parse(claimsPrincipal.FindFirst("id").Value);
            var result = await _skillRepository.GetSkills(userId , null);
            if (result.ResultObj == null)
            {
                return NoContent();
            }

            return Ok(result.ResultObj);
        }

        [HttpPut("{skillId}/disable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableSkill(int skillId)
        {
            try
            {
                var result = await _skillRepository.DisableSkill(skillId);
                if (result.IsSuccessed == false)
                {
                    return NotFound();
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

        [HttpPut("{skillId}/enable")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnableSkill(int skillId)
        {
            try
            {
                var result = await _skillRepository.EnableSkill(skillId);
                if (result.IsSuccessed == false)
                {
                    return NotFound();
                }

                return Ok(result.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "- Server Error: " + ex);
                return StatusCode(500);
            }
        }

 
    }
}
