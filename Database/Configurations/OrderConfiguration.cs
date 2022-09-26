using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Database.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.IsAccepted).HasDefaultValue(false);
            builder.Property(x => x.Quality).IsRequired();
            builder.Property(x => x.Price).IsRequired();


            // 1-1: order - skill
            builder.HasOne(order => order.Skill)
                .WithOne(skill => skill.Order)
                .HasForeignKey<Order>(order => order.SkillId);

            // 1-n: user - orders
            builder.HasOne(order => order.User)
                    .WithMany(user => user.Orders)
                    .HasForeignKey(order => order.UserOrderId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}