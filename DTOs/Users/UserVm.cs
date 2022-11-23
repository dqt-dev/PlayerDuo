namespace PlayerDuo.DTOs.Users
{
    public class UserVm
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? NickName { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public int? ProviderId { get; set; }
        public bool? Status { get; set; }
        public bool IsEnabled { get; set; }
        public int? Coin { get; set; }
    }
}
