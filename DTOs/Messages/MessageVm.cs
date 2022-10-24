namespace PlayerDuo.DTOs.Messages
{
    public class MessageVm
    {
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime DateTime { get; set; }
    }
}
