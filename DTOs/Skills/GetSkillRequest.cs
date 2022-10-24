namespace PlayerDuo.DTOs.Skills
{
    public class GetSkillRequest
    {
        public int? UserId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}