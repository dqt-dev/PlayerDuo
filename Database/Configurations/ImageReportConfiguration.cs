using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Database.Configurations
{
    public class ImageReportConfiguration : IEntityTypeConfiguration<ImageReport>
    {
        public void Configure(EntityTypeBuilder<ImageReport> builder)
        {
            builder.ToTable("ImageReports");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.ImageUrl).IsRequired();

            // 1-n: report - image reports
                builder.HasOne(imageReport => imageReport.Report)
                    .WithMany(report => report.ImageReports)
                    .HasForeignKey(imageReport => imageReport.ReportId);            

        }
    }
}