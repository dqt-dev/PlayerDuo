using Microsoft.EntityFrameworkCore;
using PlayerDuo.Database;
using PlayerDuo.Database.Entities;
using PlayerDuo.DTOs.Categories;
using PlayerDuo.Services.Storage;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyDbContext _context;
        private readonly IStorageService _storageService;

        public CategoryRepository(MyDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<ApiResult<string>> CreateCategory(CategoryVm request)
        {
            if(request == null)
                return new ApiResult<string>(false, Message: "Invalid data!");
            Category category = new Category()
            {
                CategoryName = request.CategoryName,
                ImageUrl = request.ImageUrl,
                ImageSmallUrl = request.ImageSmallUrl
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Create new category successfully!");
        }

        public async Task<ApiResult<List<CategoryVm>>> GetCategory()
        {
            var query = await _context.Categories.ToListAsync();
            List<CategoryVm> result = query.Select(c => new CategoryVm()
            {
                CategoryName=c.CategoryName,
                ImageUrl=c.ImageUrl,
                ImageSmallUrl=c.ImageSmallUrl
            }
            ).ToList();
            return new ApiResult<List<CategoryVm>>(true, ResultObj: result);
        }

        public async Task<ApiResult<string>> UpdateCategory(int categoryId, CategoryVm request)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null) return new ApiResult<string>(false, Message: $"No Category match with ID: {categoryId}");
            if (request == null)
                return new ApiResult<string>(false, Message: "Invalid data!");

            category.CategoryName = request.CategoryName;
            category.ImageUrl = request.ImageUrl;
            category.ImageSmallUrl = request.ImageSmallUrl;

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Create new category successfully!");
        }
    }
}
