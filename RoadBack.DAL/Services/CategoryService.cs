using Microsoft.EntityFrameworkCore;
using RoadBack.DAL.Services.Interfaces;
using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public CategoryService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ServiceDataResponse<Category>> CreateCategoryAsync(Category category)
        {
            if (category == null)
            {
                return ServiceDataResponse<Category>.Failed("Data cannot be null");
            }

            if (await _dbContext.Categories.AnyAsync(c => c.Id == category.Id))
            {
                return ServiceDataResponse<Category>.Failed("Category with this name already exist");
            }

            var categoryId = Guid.NewGuid();
            category.Id = categoryId;
            
            _dbContext.Categories.Add(category);

            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Category>.Succeeded(category);
        }

        public async Task<ServiceResponse> DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (category == null)
            {
                return ServiceResponse.Failed("Category doesnt exist");
            }

            _dbContext.Categories.Remove(category);

            category.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return ServiceResponse.Succeeded();
        }

        public async Task<ServiceDataResponse<IEnumerable<Category>>> GetCategoriesAsync(int quatity)
        {
            var categories = await _dbContext.Categories.Take(quatity).ToListAsync();
            if (categories == null)
            {
                return ServiceDataResponse<IEnumerable<Category>>.Failed("Categories doesnt exist");
            }

            return ServiceDataResponse<IEnumerable<Category>>.Succeeded(categories);
        }

        public async Task<ServiceDataResponse<Category>> GetCategoryByIdAsync(Guid id)
        {
            var category = await _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return ServiceDataResponse<Category>.Failed("Category with this Id doesnt exist");
            }

            return ServiceDataResponse<Category>.Succeeded(category);
        }

        public async Task<ServiceDataResponse<Category>> UpdateCategoryAsync(Category category)
        {
            category = await _dbContext.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (category == null)
            {
                return ServiceDataResponse<Category>.Failed("Category doesnt exist");
            }

            _dbContext.Categories.Update(category);

            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Category>.Succeeded(category);
        }
    }
}
