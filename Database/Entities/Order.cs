namespace PlayerDuo.Database.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int? SkillId { get; set; }
        public int? UserOrderId { get; set; }
        public bool IsAccepted { get; set; }
        public int? Quality { get; set; }
        public double? Price { get; set; }
        public double? Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }


        // navigation props
        // 1 role - n user roles
        public User? User { get; set; }
        // 1 role - n user roles
        public Skill? Skill { get; set; }
    }
}
