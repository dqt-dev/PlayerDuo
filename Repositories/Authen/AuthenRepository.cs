using PlayerDuo.Database;
using PlayerDuo.Database.Entities;
using PlayerDuo.DTOs.Authen;
using PlayerDuo.Services.Storage;
using PlayerDuo.Services.Token;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PlayerDuo.Repositories.Authen
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly MyDbContext _context ;
        private readonly ITokenService _tokenService;
        private readonly IStorageService _storageService;

        public AuthenRepository(MyDbContext context, ITokenService tokenService, IStorageService storageService)
        {
            _context = context;
            _tokenService = tokenService;
            _storageService = storageService;
        }

        public async Task<AuthenVm> AdminLogin(LoginRequest request)
        {
            if(!request.Username.Equals("admin"))
            {
                return null;
            }
            var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                        .Where(u => u.Username.Equals(request.Username))
                                        .FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            // check password
            // algorithm for password encoding, with key specified by user's key
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            // compare 2 arrays of byte: passwordHash from request vs PasswordHash from Db
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            var token = _tokenService.CreateToken(user, user.UserRoles.Select(x => x.Role.RoleName).ToList());

            return new AuthenVm()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                AvatarUrl = !String.IsNullOrEmpty(user.AvatarUrl) ? user.AvatarUrl : String.Empty,
                ProviderId = user.ProviderId != null ? user.ProviderId : 0,
                Token = token
            };
        }

        public async Task<AuthenVm> Login(LoginRequest request)
        {
            var user = await _context.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                                        .Where(u => u.Username.Equals(request.Username))
                                        .FirstOrDefaultAsync();

            if(user == null)
            {
                return null;
            }

            // check password
            // algorithm for password encoding, with key specified by user's key
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            // compare 2 arrays of byte: passwordHash from request vs PasswordHash from Db
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            var token = _tokenService.CreateToken(user, user.UserRoles.Select(x => x.Role.RoleName).ToList());

            return new AuthenVm()
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                AvatarUrl = !String.IsNullOrEmpty(user.AvatarUrl) ? user.AvatarUrl : String.Empty,
                ProviderId = user.ProviderId != null ? user.ProviderId : 0,
                Token = token
            };
        }

        public async Task<AuthenVm> Register(RegisterRequest request)
        {
            var existedUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == request.Username);
            if (existedUser != null)
            {
                return null;
            }

            if(request.Password != request.ConfirmPassword)
            {
                return null;
            }

            // algorithm for password encoding
            using var hmac = new HMACSHA512();
            // new user info
            var newUser = new User()
            {
                Username = request.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                AvatarUrl = "/storage/blank_avatar.png",
                IsEnabled = true
            };
            // assign 'Tourist' role to new user
            var userRoles = new List<UserRole>();
            userRoles.Add(
                new UserRole { RoleId = 3 }
            );
            newUser.UserRoles = userRoles;


            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // get list roles of new user
            var roles = _context.UserRoles.Where(x => x.UserId == newUser.Id).Select(x => x.Role.RoleName).ToList();

            var token = _tokenService.CreateToken(newUser, roles);

            return new AuthenVm()
            {
                Id = newUser.Id,
                Username = newUser.Username,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Phone = newUser.Phone,
                Email = newUser.Email,
                AvatarUrl = "/storage/blank_avatar.png",
                ProviderId = 0,
                Token = token
            };
        }
        public async Task<int?> ChangePassword(int userId, ChangePasswordRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.CurrentPassword));
            // compare two bytes arrays
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i])
                {
                    return null;
                }
            }

            // save new password
            using var new_hmac = new HMACSHA512();
            user.PasswordHash = new_hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
            user.PasswordSalt = new_hmac.Key;          

            return await _context.SaveChangesAsync();
        }
    }
}
