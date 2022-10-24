using PlayerDuo.Database.Entities;
namespace PlayerDuo.DTOs.Reports
{
    public class CreateReportVm
    {
        public string? ReportTypeName { get; set; }
        public string? Content { get; set; }
        public List<ImageReport> ? ImageReports { get; set; }
        public string? CreatedUser { get; set; }
        public string? ReportedUser { get; set; }
    }
}