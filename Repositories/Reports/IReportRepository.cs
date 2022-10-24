    using PlayerDuo.DTOs.Reports;
    namespace PlayerDuo.Repositories.Report
    {
        public interface IReportRepository
        {
            Task<int> CreateReport(CreateReportVm request);
            // Task<IList<ReportVm>> CreateReport(CreateReportVm request);
        }
    }