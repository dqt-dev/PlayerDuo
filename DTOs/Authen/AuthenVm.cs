namespace PlayerDuo.DTOs.Authen
{
    public class AuthenVm
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public int? ProviderId { get; set; }
        public string? Token { get; set; }
    }
}
