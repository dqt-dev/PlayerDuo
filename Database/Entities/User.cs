namespace PlayerDuo.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public string? NickName { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AudioUrl { get; set; }
        public bool IsEnabled { get; set; } // enable or disable user 
        public bool isPlayer { get; set; } // check user have role Player?
        public bool Status { get; set; }  // set status online/ offline 
        public int? Coin { get; set; }

        // navigation props
        // 1 user - n user roles
        public List<UserRole>? UserRoles { get; set; }
        // 1 user - n skills
        public List<Skill>? Skills { get; set; }

        // 1 user - n orders
        public List<Order>? Orders { get; set; }

        // 1 user - n orders
        public List<Report>? Reports { get; set; }

        // 1 user - n orders
        public List<TradeHistory>? TradeHistories { get; set; }
    }
}
