namespace PlayerDuo.Database.Entities
{
    public class Skill
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public string? AudioUrl { get; set; }
        public double Price { get; set; }  // giá cho 1 trận
        public bool IsEnabled { get; set; } 
        public string? ImageDetailUrl { get; set; }

        public User? User { get; set; }
        public Category? Category { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
