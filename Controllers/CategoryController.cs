using PlayerDuo.DTOs.Skills;
using PlayerDuo.Repositories.Skills;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlayerDuo.Repositories.Categories;

namespace PlayerDuo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet()]
        public async Task<ActionResult> GetCategory()
        {
            var result = await _categoryRepository.GetCategory();
            if (result.ResultObj == null)
            {
                return NoContent();
            }

            return Ok(result.ResultObj);
        }

    }
}
