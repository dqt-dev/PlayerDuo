using PlayerDuo.DTOs.Common;
using PlayerDuo.DTOs.Users;

namespace PlayerDuo.Repositories.Users
{
    public interface IUserRepository
    {
        Task<UserVm> GetUserById(int userId);
        Task<bool?> UpdateUser(int userId, UpdateUserRequest request);
        Task<bool?> UpdateStatus(int userId, bool status);
        Task<bool?> CheckEmail(string email, string password);
        Task<bool?> CheckUserEnabled(int userId);


        // for admin
        Task<PagedResult<UserVm>> GetUsers(GetUsersManageRequest request);
        Task<int> DisableUser(int userId);
        Task<int> EnableUser(int userId);
    }
}
