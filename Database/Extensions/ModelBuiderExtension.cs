using PlayerDuo.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PlayerDuo.Database.Extensions
{
    public static class ModelBuiderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // data seeding for Users
            using var hmac = new HMACSHA512();
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Admin123!")),
                    PasswordSalt = hmac.Key,
                    NickName = "admin",
                    Phone = "0921231220",
                    Email = "tuandang29042000@gmail.com",
                    AvatarUrl = null,
                    IsEnabled = true
                },
                new User
                {
                    Id = 2,
                    Username = "quoctuan",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Quoctuan123!")),
                    PasswordSalt = hmac.Key,
                    NickName = "boi_cudon",
                    Phone = "0921231220",
                    Email = "tuandang29042000@gmail.com",
                    AvatarUrl = "/storage/tuan.jpg",
                    IsEnabled = true
                },
                new User
                {
                    Id = 3,
                    Username = "quocdat",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Quocdat123!")),
                    PasswordSalt = hmac.Key,
                    NickName = "sadboiz",
                    Phone = "0905553859",
                    Email = "ngoluuquocdat@gmail.com",
                    AvatarUrl = "/storage/dat.jpg",
                    IsEnabled = true
                },
                new User
                {
                    Id = 4,
                    Username = "quangbao",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Quangbao123!")),
                    PasswordSalt = hmac.Key,
                    NickName = "mt-15",
                    Phone = "0905553859",
                    Email = "quanbao203@gmail.com",
                    AvatarUrl = "/storage/bao.jpg",
                    IsEnabled = true
                },
                new User
                {
                    Id = 5,
                    Username = "congtai",
                    PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Congtai123!")),
                    PasswordSalt = hmac.Key,
                    NickName = "gaylord",
                    Phone = "0905553859",
                    Email = "brad@gmail.com",
                    AvatarUrl = "/storage/tai.jpg",
                    IsEnabled = true
                }
            );
            // data seeding for Roles
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    RoleName = "Admin"
                },
                new Role
                {
                    Id = 2,
                    RoleName = "Player"
                },
                new Role
                {
                    Id = 3,
                    RoleName = "User"
                }
            );
            // data seeding for UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = 1, RoleId = 1 },
                new UserRole { UserId = 1, RoleId = 2 },
                new UserRole { UserId = 1, RoleId = 3 },
                new UserRole { UserId = 2, RoleId = 2 },
                new UserRole { UserId = 2, RoleId = 3 },
                new UserRole { UserId = 3, RoleId = 2 },
                new UserRole { UserId = 3, RoleId = 3 },
                new UserRole { UserId = 4, RoleId = 3 },
                new UserRole { UserId = 5, RoleId = 3 }
            );

            // data seeding for Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Liên Quân Mobile", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 2, CategoryName = "Liên Minh Huyền Thoại", ImageUrl = "/storage/LOL.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 3, CategoryName = "PUBG Mobile", ImageUrl = "/storage/PUBG.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 4, CategoryName = "Free Fire", ImageUrl = "/storage/freefire.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 5, CategoryName = "Liên Minh: Tốc Chiến", ImageUrl = "/storage/LOL_Mobile.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 6, CategoryName = "Mobile Legends", ImageUrl = "/storage/MBLG.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 7, CategoryName = "Play Together", ImageUrl = "/storage/playtogether.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 8, CategoryName = "AU Mobile", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 9, CategoryName = "AU Mobile", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 10, CategoryName = "Đấu Trường Chân Lý", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 11, CategoryName = "Valorant", ImageUrl = "/storage/valorant.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 12, CategoryName = "Minecraft", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" },
                new Category { Id = 13, CategoryName = "CSGO", ImageUrl = "/storage/AOV.png", ImageSmallUrl = "/storage/AOV.png" }
            );

            // data seeding for ReportType
            modelBuilder.Entity<ReportType>().HasData(
                new ReportType { Id = 1, Name = "Quấy rối tình dục" },
                new ReportType { Id = 2, Name = "Ngôn ngữ thù ghét hoặc bắt nạt" },
                new ReportType { Id = 3, Name = "Lừa đảo hoặc gian lận" },
                new ReportType { Id = 4, Name = "Quảng cáo hoặc spam" },
                new ReportType { Id = 5, Name = "Bạo lực và chia rẽ" },
                new ReportType { Id = 6, Name = "Giá sai" },
                new ReportType { Id = 7, Name = "Dịch vụ không khớp với mô tả" },
                new ReportType { Id = 8, Name = "Tuổi hoặc giới tính giả" },
                new ReportType { Id = 9, Name = "Chất lượng dịch vụ kém" },
                new ReportType { Id = 10, Name = "Chuyển đơn hàng cho người khác" },
                new ReportType { Id = 11, Name = "Nhận đơn hàng vượt quá công suất" },
                new ReportType { Id = 12, Name = "Khác" }
            );

            // data seeding for Skill
            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 1,
                    UserId = 2,
                    CategoryId = 1,
                    AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                    Description = "Chiến tướng là chuyện nhỏ",
                    Price = 100,
                    IsEnabled = true, 
                    ImageDetailUrl = ""
                },
                new Skill
                {
                    Id = 2,
                    UserId = 2,
                    CategoryId = 2,
                    AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                    Description = "Tầu hài là chính",
                    Price = 100,
                    IsEnabled = false,
                    ImageDetailUrl = ""
                },
                new Skill
                {
                    Id = 3,
                    UserId = 3,
                    CategoryId = 2,
                    AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                    Description = "Cùng nhau leo rank nào.",
                    Price = 100,
                    IsEnabled = true,
                    ImageDetailUrl = ""
                },
                new Skill
                {
                    Id = 4,
                    UserId = 3,
                    CategoryId = 11,
                    AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                    Description = "Valorant thì nạp mua skin xịn",
                    Price = 100,
                    IsEnabled = true,
                    ImageDetailUrl = ""
                }
            );

            // data seeding for Orders
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, OrderedUserId = 2, SkillId = 3, Price = 100, Status = 1, CreatedAt = DateTime.Now, Quality = 2 }
            );

        }
    }
}
