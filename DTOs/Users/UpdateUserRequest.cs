namespace PlayerDuo.DTOs.Users
{
    public class UpdateUserRequest
    {
        public string? NickName { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Avatar { get; set; }
    }
}
