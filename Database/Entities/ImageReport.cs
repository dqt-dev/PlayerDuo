namespace PlayerDuo.Database.Entities
{
    public class ImageReport
    {
        public int Id { get; set; }
        public int? ReportId { get; set; }
        public string? ImageUrl { get; set; }

        public Report? Report { get; set; }
    }
}