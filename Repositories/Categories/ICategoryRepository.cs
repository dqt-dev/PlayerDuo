using PlayerDuo.DTOs.Categories;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Categories
{
    public interface ICategoryRepository
    {
        Task<ApiResult<string>> CreateCategory(CategoryVm request);
        Task<ApiResult<string>> UpdateCategory(int categoryId, CategoryVm request);
        Task<ApiResult<List<CategoryVm>>> GetCategory();
    }
}
