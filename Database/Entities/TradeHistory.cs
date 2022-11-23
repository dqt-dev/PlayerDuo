namespace PlayerDuo.Database.Entities
{
    public class TradeHistory
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? Coin { get; set; }
        public int? Type { get; set; }
        public DateTime? CreatedAt { get; set; }

        // 1 user - n order roles
        public User? User { get; set; }
    }
}
