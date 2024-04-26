using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services.Interfaces
{
    public interface IExpenseService
    {
        Task<ServiceDataResponse<IEnumerable<Expense>>> GetExpensesAsync(int quatity = 1, bool takeLast = false);
        Task<ServiceDataResponse<Expense>> GetExpenseByIdAsync(Guid id);
        Task<ServiceDataResponse<Expense>> CreateExpenseAsync(Expense expense);
        Task<ServiceDataResponse<Expense>> UpdateExpenseAsync(Expense expense);
        Task<ServiceResponse> DeleteExpenseAsync(Guid id);
    }
}
