namespace PlayerDuo.DTOs.Skills
{
    public class SkillVm
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }
        public string? PlayerName { get; set; }
        public bool Status { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? AudioUrl { get; set; }
        public int? Total { get; set; }
        public double? Rating { get; set; }
        public double Price { get; set; }  // giá cho 1 trận
        public bool IsEnabled { get; set; } 
    }
}