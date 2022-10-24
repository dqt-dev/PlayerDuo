namespace PlayerDuo.Database.Entities
{
    public class ReportType
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        // navigation props
        public Report? Report { get; set; }
    }
}
