namespace PlayerDuo.DTOs.Messages
{
    public class MessageDTO
    {
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
    }
}
