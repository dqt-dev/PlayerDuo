using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Database.Configurations
{
    public class TradeHistoryConfiguration : IEntityTypeConfiguration<TradeHistory>
    {
        public void Configure(EntityTypeBuilder<TradeHistory> builder)
        {
            builder.ToTable("TradeHistories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Coin).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            // 1-n: user - trade history
            builder.HasOne(trade => trade.User)
                    .WithMany(user => user.TradeHistories)
                    .HasForeignKey(trade => trade.UserId);
        }
    }
}
