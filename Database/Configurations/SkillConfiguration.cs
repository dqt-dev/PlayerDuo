using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlayerDuo.Database.Entities;

namespace PlayerDuo.Database.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.IsEnabled).HasDefaultValue(false);
            builder.Property(x => x.AudioUrl).IsRequired();
            builder.Property(x => x.ImageDetailUrl).IsRequired();



            // 1-n: user - user skills
            builder.HasOne(skill => skill.User)
                    .WithMany(user => user.Skills)
                    .HasForeignKey(skill => skill.UserId);

            // n-1: skill - category
            builder.HasOne(skill => skill.Category)
                .WithMany(category => category.Skills)
                .HasForeignKey(skill => skill.CategoryId);
        }
    }
}
    