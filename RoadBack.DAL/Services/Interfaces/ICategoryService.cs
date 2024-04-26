using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceDataResponse<Category>> CreateCategoryAsync(Category category);
        Task<ServiceDataResponse<IEnumerable<Category>>> GetCategoriesAsync(int quatity, bool takeLast);
        Task<ServiceDataResponse<Category>> GetCategoryByIdAsync(Guid id);
        Task<ServiceDataResponse<Category>> UpdateCategoryAsync(Category category);
        Task<ServiceResponse> DeleteCategoryAsync(Guid categoryId);
    }
}
