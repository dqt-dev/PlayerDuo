using PlayerDuo.Database;
using PlayerDuo.Database.Entities;
using PlayerDuo.DTOs.Orders;
using PlayerDuo.Services.Storage;
using PlayerDuo.Services.Token;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using PlayerDuo.Utilities;

namespace PlayerDuo.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyDbContext _context;
        private readonly ITokenService _tokenService;

        public OrderRepository(MyDbContext context, ITokenService tokenService, IStorageService storageService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ApiResult<string>> CancelOrder(int userId, int orderId)
        {
            // check order is match with user
            var query = (from u in _context.Users
                         join s in _context.Skills on u.Id equals s.UserId
                         join o in _context.Orders on s.Id equals o.SkillId
                         where (s.UserId == userId && o.Id == orderId)
                         select new{}).FirstOrDefault();
            if (query == null) return new ApiResult<string>(false, Message: "Order don't match with UserId!"); // return false if order doesn't match user id

            var order = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (order == null) return new ApiResult<string>(false, Message: "Order is not exist!");
            if (DateTime.Now > ((DateTime)order.CreatedAt).AddMinutes(5))
            {
                order.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new ApiResult<string>(false, "Mã xác nhận quá hạn");
            }
            order.Status = 4; // update status of order is canceled
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Successfully cancel order!");
        }


        public async Task<ApiResult<string>> ConfirmOrder(int userId, int orderId)
        {
            // check order is match with user
            var query = (from u in _context.Users
                         join s in _context.Skills on u.Id equals s.UserId
                         join o in _context.Orders on s.Id equals o.SkillId
                         where (s.UserId == userId && o.Id == orderId)
                         select new{}).FirstOrDefault();
            if (query == null) return new ApiResult<string>(false, Message: "Order don't match with UserId!"); // return false if order doesn't match user id

            var order = await _context.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
            if (order == null) return new ApiResult<string>(false, Message: "Order is not exist!");
            if (DateTime.Now > ((DateTime)order.CreatedAt).AddMinutes(5))
            {
                order.IsDeleted = true; 
                await _context.SaveChangesAsync();
                return new ApiResult<string>(false, "Đơn hàng đã quá hạn");
            }   
            order.Status = 2; // update status of order is starting
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Successfully confirm order!");
        }

        public async Task<ApiResult<string>> CreateOrder(int userId, CreateOrderRequest request)
        {
            if (request == null) return new ApiResult<string>(false, Message: "Input is required!");
            // kiểm tra skill này có tồn tại hay không?
            var skill = await _context.Skills.Where(x => x.Id == request.SkillId).FirstOrDefaultAsync();
            // trả ra lỗi không tồn tại
            if (skill == null) return new ApiResult<string>(false, Message: "SkillId is not exist!");
            if (skill.UserId == userId) return new ApiResult<string>(false, Message: "Không được đặt chính bản thân!");
            var order = new Order()
            {
                OrderedUserId = userId,
                SkillId = request.SkillId,
                Price = skill.Price,
                Quality = request.Quality,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new ApiResult<string>(true, Message: "Successfully created!");
        }

        public async Task<ApiResult<OrderVm>> GetOrderById(int orderId, int userId)
        {

            // check order is match with user
            var query = await (from u in _context.Users
                               join o in _context.Orders on u.Id equals o.OrderedUserId
                               join s in _context.Skills on o.SkillId equals s.Id
                               join c in _context.Categories on s.CategoryId equals c.Id
                               where o.Id == orderId && (o.OrderedUserId == userId || s.UserId == userId) && o.IsDeleted == false
                               select new OrderVm()
                               {
                                   PlayerName = _context.Users.Where(u => u.Id == s.UserId).Select(u => u.NickName).FirstOrDefault(),
                                   OrderedUserName = _context.Users.Where(u => u.Id == o.OrderedUserId).Select(u => u.NickName).FirstOrDefault(),
                                   AvatarUserUrl = _context.Users.Where(u => u.Id == o.OrderedUserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                                   AvatarPlayerUrl = _context.Users.Where(u => u.Id == s.UserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                                   CategoryName = c.CategoryName,
                                   Price = s.Price,
                                   Quality = o.Quality,
                                   Status = o.Status,
                                   TotalPrice = s.Price * o.Quality,
                                   OrderDate = o.CreatedAt
                               }).FirstOrDefaultAsync();

            if (query == null) return new ApiResult<OrderVm>(false, Message: "Order này không tồn tại hoặc bạn không có quyền truy cập"); // return false if order doesn't match user id
            return new ApiResult<OrderVm>(true, ResultObj: query);
        }

        public async Task<ApiResult<List<OrderVm>>> GetMyOrders(int userId, int? status)
        {

            // Get list order filter by user id
            List<OrderVm> orders = new List<OrderVm>();
            var query = from u in _context.Users
                        join o in _context.Orders on u.Id equals o.OrderedUserId
                        join s in _context.Skills on o.SkillId equals s.Id
                        join c in _context.Categories on s.CategoryId equals c.Id
                        where u.Id == userId && o.IsDeleted == false
                        select new { u, o, s, c };
            if (query == null) return new ApiResult<List<OrderVm>>(false, Message: "Có lỗi xảy ra!");

            var list_records = await query.OrderByDescending(x => x.c.Id).ToListAsync();

            if (status == null) // get all orders
            {
                foreach (var record in list_records)
                {
                    orders.Add(new OrderVm()
                    {
                        PlayerName = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.NickName).FirstOrDefault(),
                        AvatarPlayerUrl = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                        CategoryName = record.c.CategoryName,
                        Price = record.s.Price,
                        Quality = record.o.Quality,
                        Status = record.o.Status,
                        TotalPrice = record.s.Price * record.o.Quality,
                        OrderDate = record.o.CreatedAt,
                    });
                }
                return new ApiResult<List<OrderVm>>(true, ResultObj: orders);
            }
            else
            {
                list_records = list_records.Where(x => x.o.Status == status).OrderByDescending(x => x.c.Id).ToList();
                foreach (var record in list_records)
                {
                    orders.Add(new OrderVm()
                    {
                        PlayerName = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.NickName).FirstOrDefault(),
                        AvatarPlayerUrl = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                        CategoryName = record.c.CategoryName,
                        Price = record.s.Price,
                        Quality = record.o.Quality,
                        Status = record.o.Status,
                        TotalPrice = record.s.Price * record.o.Quality,
                        OrderDate = record.o.CreatedAt
                    });
                }
                return new ApiResult<List<OrderVm>>(true, ResultObj: orders);
            }
        }

        public async Task<ApiResult<List<OrderVm>>> GetOrders(int userId, int? status)   // other user order me
        {
            // Get list order filter by user id
            List<OrderVm> orders = new List<OrderVm>();
            var query = from u in _context.Users
                        join o in _context.Orders on u.Id equals o.OrderedUserId
                        join s in _context.Skills on o.SkillId equals s.Id
                        join c in _context.Categories on s.CategoryId equals c.Id
                        where s.UserId == userId && o.IsDeleted == false
                        select new { u, o, s, c };
            if (query == null) return new ApiResult<List<OrderVm>>(false, Message: "Có lỗi xảy ra!");

            var list_records = await query.OrderByDescending(x => x.c.Id).ToListAsync();

            if (status == null) // get all orders
            {
                foreach (var record in list_records)
                {
                    orders.Add(new OrderVm()
                    {
                        OrderId = record.o.Id,
                        OrderedUserName = _context.Users.Where(u => u.Id == record.o.OrderedUserId).Select(u => u.NickName).FirstOrDefault(),
                        AvatarUserUrl = _context.Users.Where(u => u.Id == record.o.OrderedUserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                        CategoryName = record.c.CategoryName,
                        Price = record.s.Price,
                        Quality = record.o.Quality,
                        Status = record.o.Status,
                        TotalPrice = record.s.Price * record.o.Quality,
                        OrderDate = record.o.CreatedAt
                    });
                }
                return new ApiResult<List<OrderVm>>(true, ResultObj: orders);
            }
            else
            {
                list_records = list_records.Where(x => x.o.Status == status).OrderByDescending(x => x.c.Id).ToList();
                foreach (var record in list_records)
                {
                    orders.Add(new OrderVm()
                    {
                        OrderId = record.o.Id,
                        PlayerName = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.NickName).FirstOrDefault(),
                        AvatarPlayerUrl = _context.Users.Where(u => u.Id == record.s.UserId).Select(u => u.AvatarUrl).FirstOrDefault(),
                        CategoryName = record.c.CategoryName,
                        Price = record.s.Price,
                        Quality = record.o.Quality,
                        Status = record.o.Status,
                        TotalPrice = record.s.Price * record.o.Quality,
                        OrderDate = record.o.CreatedAt
                    });
                }
                return new ApiResult<List<OrderVm>>(true, ResultObj: orders);
            }
        }

        public async Task<ApiResult<List<ReviewVM>>> GetReviewBySkillId(int skillId)
        {
            // Get list order filter by user id
            List<ReviewVM> reviews = new List<ReviewVM>();
            var query = from u in _context.Users
                        join o in _context.Orders on u.Id equals o.OrderedUserId
                        join s in _context.Skills on o.SkillId equals s.Id
                        where s.Id == skillId && o.Rating != null
                        select new { u, o, s };
            if (query == null) return new ApiResult<List<ReviewVM>>(false, Message: "Có lỗi xảy ra!");
            foreach (var item in query)
            {
                reviews.Add(new ReviewVM
                {
                    ReviewId = item.o.Id,
                    NickName = item.u.NickName,
                    AvatarUrl = item.u.AvatarUrl,
                    Comment = item.o.Comment,
                    CreatedAt = item.o.CreatedAt,
                    Rating = item.o.Rating
                });
            }
            return new ApiResult<List<ReviewVM>>(true, ResultObj: reviews);

        }
    }
}
