namespace PlayerDuo.Database.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public int? ReportTypeId { get; set; }
        public string? Content { get; set; }
        public int? CreatedUserId { get; set; }
        public int? ReportedUserId { get; set; }
        public bool? IsChecked { get; set; }

        // navigation props
        public ReportType? ReportType { get; set; }
        // 1 report - n image report
        public List<ImageReport>? ImageReports { get; set; }
    }
}
