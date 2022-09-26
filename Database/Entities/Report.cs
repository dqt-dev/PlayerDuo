namespace PlayerDuo.Database.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int? ReportTypeId { get; set; }
        public string? Content { get; set; }
        public int? CreatedUserId { get; set; }
        public int? ReportedUserId { get; set; }

        // navigation props
        // 1 report - 1 user
        public User? User { get; set; }
        // 1 report - 1 reportType
        public ReportType? ReportType { get; set; }
        // 1 report - n image report
        public List<ImageReport>? ImageReports { get; set; }
    }
}
