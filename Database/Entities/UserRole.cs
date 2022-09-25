namespace PlayerDuo.Database.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        // navigation props
        // 1 user - n user roles
        public User? User { get; set; }
        // 1 role - n user roles
        public Role? Role { get; set; }
    }
}
