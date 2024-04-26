using Microsoft.EntityFrameworkCore;
using RoadBack.DAL.Services.Interfaces;
using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services
{
    public class AccountService : IAccountService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public AccountService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceDataResponse<Account>> CreateAccountAsync(Account account)
        {
            if (account == null)
            {
                return ServiceDataResponse<Account>.Failed("Data cannot be null");
            }

            if (await _dbContext.Accounts.AnyAsync(a => a.Name == account.Name))
            {
                return ServiceDataResponse<Account>.Failed("Account with this name already exist");
            }

            var accountId = Guid.NewGuid();
            account.Id = accountId;

            _dbContext.Accounts.Add(account);
            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Account>.Succeeded(account);
        }

        public async Task<ServiceResponse> DeleteAccountAsync(Guid id)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Id == id);
            if (account == null)
            {
                return ServiceResponse.Failed("Account doesnt exist");
            }

            _dbContext.Accounts.Remove(account);

            account.IsDeleted = true;
            await _dbContext.SaveChangesAsync();

            return ServiceResponse.Succeeded();
        }

        public async Task<ServiceDataResponse<Account>> GetAccountByIdAsync(Guid id)
        {
            var account = await _dbContext.Accounts.SingleOrDefaultAsync(a => a.Id == id);

            if (account == null)
            {
                return ServiceDataResponse<Account>.Failed("Account with this Id doesnt exist");
            }

            return ServiceDataResponse<Account>.Succeeded(account);
        }

        public async Task<ServiceDataResponse<IEnumerable<Account>>> GetAccountsAsync(int quatity = 1, bool takeLast = false)
        {
            var accounts = await _dbContext.Accounts.Take(quatity).ToListAsync();

            if (accounts == null)
            {
                return ServiceDataResponse<IEnumerable<Account>>.Failed("Accounts doesnt exist");
            }

            return ServiceDataResponse<IEnumerable<Account>>.Succeeded(accounts);
        }

        public async Task<ServiceDataResponse<Account>> UpdateAccountAsync(Account account)
        {
            account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id);

            if (account == null)
            {
                return ServiceDataResponse<Account>.Failed("Account doesnt exist");
            }

            _dbContext.Accounts.Update(account);

            await _dbContext.SaveChangesAsync();

            return ServiceDataResponse<Account>.Succeeded(account);
        }
    }
}
