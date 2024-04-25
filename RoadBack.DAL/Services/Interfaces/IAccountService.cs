using RoadBack.Domain;
using RoadBack.Domain.Models;

namespace RoadBack.DAL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceDataResponse<Account>> CreateAccountAsync(Account account);
        Task<ServiceDataResponse<IEnumerable<Account>>> GetAccountsAsync(int quatity);
        Task<ServiceDataResponse<Account>> GetAccountByIdAsync(Guid id);
        Task<ServiceDataResponse<Account>> UpdateAccountAsync(Account account);
        Task<ServiceResponse> DeleteAccountAsync(Guid id);
    }
}
