namespace PlayerDuo.DTOs.Users
{
    public class UserChatVm
    {
        public string? Id { get; set; }
        public int? UserId { get; set; }
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
        public string? LastestMessage { get; set; }
        public DateTime? LastestTime { get; set; }
    }
}
