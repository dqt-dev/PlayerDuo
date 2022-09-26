namespace PlayerDuo.Database.Entities
{
    public class ReportType
    {
        public int Id { get; set; }
        public int? Name { get; set; }

        // navigation props
        public Report? Report { get; set; }
    }
}
