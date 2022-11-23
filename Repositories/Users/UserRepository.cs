using PlayerDuo.Database;
using PlayerDuo.Database.Entities;
using PlayerDuo.DTOs.Common;
using PlayerDuo.DTOs.Users;
using PlayerDuo.Services.Storage;
using PlayerDuo.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PlayerDuo.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;
        private readonly IStorageService _storageService;

        public UserRepository(MyDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<UserVm> GetUserById(int userId)
        {
            if (!_context.Users.Any(x => (x.Id == userId)))
            {
                return null;
            }
            var user = await _context.Users.Where(x => x.Id == userId).AsNoTracking()
                                        .Select(x => new UserVm()
                                        {
                                            Id = x.Id,
                                            Username = x.Username,
                                            NickName = x.NickName,
                                            Description = x.Description,
                                            Phone = x.Phone,
                                            Email = x.Email,
                                            AvatarUrl = !String.IsNullOrEmpty(x.AvatarUrl) ? x.AvatarUrl : String.Empty,
                                            Status = x.Status,
                                            IsEnabled = x.IsEnabled,
                                            Coin = x.Coin,
                                        }).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool?> UpdateUser(int userId, UpdateUserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                return null;
            }
            user.NickName = request.NickName;
            user.Phone = request.Phone;
            
            // save new avatar 
            if(request.Avatar != null)
            {
                user.AvatarUrl = await _storageService.SaveImage(request.Avatar);
            }

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        // change email 
        public async Task<bool?> CheckEmail(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email.ToLower()));
            if(user == null)
            {
                return null;
            }
            // check password
            // algorithm for password encoding, with key specified by user's key
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            // compare 2 arrays of byte: passwordHash from request vs PasswordHash from Db
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.PasswordHash[i])
                {
                    return false;
                }
            }

            return true;
        }

        // wish list functions
        // public async Task<int> AddToWishList(int userId, int tourId)
        // {
        //     if(_context.WishItems.Any(x => x.TourId == tourId && x.UserId == userId))
        //     {
        //         return 0;
        //     }
        //     _context.WishItems.Add(new WishItem() { UserId = userId, TourId = tourId });

        //     return await _context.SaveChangesAsync();
        // }

        // public async Task<int> RemoveFromWishList(int userId, int tourId)
        // {
        //     var NOT_FOUND_ERROR = -1;

        //     var wishItem = await _context.WishItems.FirstOrDefaultAsync(x => x.UserId == userId && x.TourId == tourId);
        //     if (wishItem == null)
        //     {
        //         return NOT_FOUND_ERROR;
        //     }
        //     _context.WishItems.Remove(wishItem);

        //     return await _context.SaveChangesAsync();
        // }

        // public async Task<List<WishItemVm>> GetWishList(int userId)
        // {
        //     var wishList = await _context.WishItems.Where(x => x.UserId == userId).AsNoTracking()
        //                             .OrderByDescending(x => x.Date)
        //                             .Select(x => new WishItemVm()
        //                             {
        //                                 Id = x.Id,
        //                                 TourId = x.Tour.Id,
        //                                 TourName = x.Tour.TourName,
        //                                 ThumbnailPath = (x.Tour.TourImages.Count() > 0) ? x.Tour.TourImages[0].Url : String.Empty,
        //                                 ProviderId = x.Tour.ProviderId,
        //                                 ProviderName = x.Tour.Provider.ProviderName,
        //                                 ProviderAvatar = x.Tour.Provider.AvatarUrl
        //                             })
        //                             .ToListAsync();

        //     return wishList;
        // }

        public async Task<PagedResult<UserVm>> GetUsers(GetUsersManageRequest request)
        {
            var query = _context.Users.Where(x => x.Id > 1).AsNoTracking();

            // filter by Id
            if (request.UserId != 0)
            {
                query = query.Where(x => x.Id == request.UserId);
            }
            // filter by enable/disable
            if (request.IsEnabled != null)
            {
                query = query.Where(x => (x.IsEnabled == request.IsEnabled));
            }
            // filter by username
            if (!string.IsNullOrEmpty(request.Username))
            {
                query = query.Where(x => x.Username == request.Username);
            }
            // filter by keyword user info
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var phoneRegex = new Regex(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");
                // by phone
                if (phoneRegex.IsMatch(request.Keyword))
                {
                    query = query.Where(x => (x.Phone.Contains(request.Keyword)));
                }
                // by email
                if (EmailValid.IsValid(request.Keyword))
                {
                    query = query.Where(x => (x.Email.Contains(request.Keyword)));
                }
                // by name
                if (!EmailValid.IsValid(request.Keyword) && !phoneRegex.IsMatch(request.Keyword))
                {
                    query = query.Where(x => x.NickName.Contains(request.Keyword));
                }
            }

            // order by newest
            query = query.OrderByDescending(x => x.Id);
            // paging
            int totalCount = query.Count();
            int totalPages = ((totalCount - 1) / request.PerPage) + 1;
            query = query.Skip((request.Page - 1) * request.PerPage).Take(request.PerPage);

            var users = await query.Select(x => new UserVm()
            {
                Id = x.Id,
                Username = x.Username,
                NickName = x.NickName,
                Phone = x.Phone,
                Email = x.Email,
                AvatarUrl = !String.IsNullOrEmpty(x.AvatarUrl) ? x.AvatarUrl : String.Empty,
                IsEnabled = x.IsEnabled,
                Coin = x.Coin,
            }).ToListAsync();

            return new PagedResult<UserVm>()
            {
                TotalCount = totalCount,
                TotalPage = totalPages,
                Items = users
            };
        }

        public async Task<int> DisableUser(int userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return 0;
            }
            // disable user
            user.IsEnabled = false;
            // disable associated provider 
            // if(user.ProviderId != null)
            // {
            //     var provider = await _context.Providers.Where(x => x.Id == user.ProviderId).FirstOrDefaultAsync();
            //     provider.IsEnabled = false;
            // }
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<int> EnableUser(int userId)
        {
            var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                return 0;
            }
            // enable
            user.IsEnabled = true;
            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool?> CheckUserEnabled(int userId)
        {
            if (!_context.Users.Any(x => x.Id == userId))
            {
                return null;
            }

            return await _context.Users.Where(x => x.Id == userId).AsNoTracking().Select(x => x.IsEnabled).FirstOrDefaultAsync();
        }

        public async Task<bool?> UpdateStatus(int userId, bool status)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                return null;
            }
            user.Status = status;

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool?> Payment(int userId, int coin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            

            // save new avatar 
            if (coin > 0)
            {
                user.Coin = user.Coin + coin;
            }

            var tradeHistory = new TradeHistory()
            {
                UserId = userId,
                Coin = coin,
                CreatedAt = DateTime.Now,
                Type = 1
            };

            _context.TradeHistories.Add(tradeHistory);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<TradeHistory>> GetTradeHistories(int userId)
        {

            var query = await _context.TradeHistories.ToListAsync();
            List<TradeHistory> result = query.Select(x => new TradeHistory()
            {
               UserId = x.UserId,
               Coin = x.Coin,
               Type = x.Type,
               CreatedAt = x.CreatedAt
            }
            ).ToList();
            return result;
        }
    }
}
