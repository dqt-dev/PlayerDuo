namespace PlayerDuo.DTOs.Skills
{
    public class CreateSkillRequest
    {
        public int? CategoryId { get; set; }
        public string? Description { get; set; }
        public IFormFile? AudioUrl { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public double Price { get; set; }  // giá cho 1 trận
    }
}