namespace PlayerDuo.DTOs.Orders
{
    public class OrderVm
    {
        public int OrderId { get; set; }
        public string? PlayerName { get; set; }
        public string? OrderedUserName { get; set; }
        public string? AvatarUserUrl { get; set; }
        public string? AvatarPlayerUrl { get; set; }
        public string? CategoryName { get; set; }
        public double? Price { get; set; }
        public int? Quality { get; set; }
        public int? Status { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}