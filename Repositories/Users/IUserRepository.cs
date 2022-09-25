using PlayerDuo.Database.Entities;

namespace PlayerDuo.Repositories.Users
{
    public interface IUserRepository
    {
        Task<IList<User>> GetUsers();
    }
}
