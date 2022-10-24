using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Database.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Content).IsRequired();

            // 1-1: Report - ReportType
            builder.HasOne(report => report.ReportType)
                .WithOne(reportType => reportType.Report)
                .HasForeignKey<Report>(report => report.ReportTypeId);

            // // 1-n: User - Reports
            //     builder.HasOne(report => report.User)
            //         .WithMany(user => user.Reports)
            //         .HasForeignKey(report => report.ReportedUserId);

            // // 1-n: User - Reports
            //     builder.HasOne(report => report.User)
            //         .WithMany(user => user.Reports)
            //         .HasForeignKey(report => report.CreatedUserId);
        }
    }
}