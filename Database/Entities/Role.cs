namespace PlayerDuo.Database.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }

        // navigation props
        // 1 role - n user roles
        public List<UserRole>? UserRoles { get; set; }
    }
}
