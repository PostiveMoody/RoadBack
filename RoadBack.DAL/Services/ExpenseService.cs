using Microsoft.EntityFrameworkCore;
using RoadBack.DAL.Services.Interfaces;
using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public ExpenseService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceDataResponse<Expense>> CreateExpenseAsync(Expense expense)
        {
            if (expense == null)
            {
                return ServiceDataResponse<Expense>.Failed("Data cannot be null");
            }

            if (await _dbContext.Accounts.AnyAsync(e => e.Id == expense.Id))
            {
                return ServiceDataResponse<Expense>.Failed("Expense with this id already exist");
            }

            var expenseId = Guid.NewGuid();
            expense.Id = expenseId;

            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Expense>.Succeeded(expense);
        }

        public async Task<ServiceResponse> DeleteExpenseAsync(Guid id)
        {
            var expense = await _dbContext.Accounts.SingleOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return ServiceResponse.Failed("Expense with this id doesnt exist");
            }

            _dbContext.Accounts.Remove(expense);

            expense.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return ServiceResponse.Succeeded();
        }

        public async Task<ServiceDataResponse<Expense>> GetExpenseByIdAsync(Guid id)
        {
            var expense = await _dbContext.Expenses.SingleOrDefaultAsync(e => e.Id == id);

            if (expense == null)
            {
                return ServiceDataResponse<Expense>.Failed("Account with this Id doesnt exist");
            }

            return ServiceDataResponse<Expense>.Succeeded(expense);
        }

        public async Task<ServiceDataResponse<IEnumerable<Expense>>> GetExpensesAsync(int quatity = 1, bool takeLast = false)
        {
            var expenses = await _dbContext.Expenses.Take(quatity).ToListAsync();

            if (takeLast)
            {
                expenses =  await _dbContext.Expenses.OrderByDescending(it => it.Id).Take(quatity).ToListAsync();
            }

            if (expenses == null)
            {
                return ServiceDataResponse<IEnumerable<Expense>>.Failed("Expenses doesnt exist");
            }

            return ServiceDataResponse<IEnumerable<Expense>>.Succeeded(expenses);
        }

        public async Task<ServiceDataResponse<Expense>> UpdateExpenseAsync(Expense expense)
        {
            throw new NotImplementedException();
        }
    }
}
