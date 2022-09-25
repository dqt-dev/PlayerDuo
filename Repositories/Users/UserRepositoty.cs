using Microsoft.EntityFrameworkCore;
using PlayerDuo.Database;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Repositories.Users
{
    public class UserRepositoty : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepositoty(MyDbContext context)
        {
            _context = context;
        }

         public async Task<IList<User>> GetUsers()
         {
            List<User> users = new List<User>();

            return await _context.Users.ToListAsync();
            
         }
    }
}
