namespace PlayerDuo.DTOs.Orders
{
    public class ReviewVM
    {
        public int? ReviewId { get; set; }
        public string? NickName { get; set; }
        public string? AvatarUrl { get; set; }
        public double? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
