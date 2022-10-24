namespace PlayerDuo.DTOs.Users
{
    public class GetUsersManageRequest
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public string? Keyword { get; set; }     // name, phone or email
        public bool? IsEnabled { get; set; }
        public int Page { get; set; } = 1;
        public int PerPage { get; set; } = 4;
    }
}
