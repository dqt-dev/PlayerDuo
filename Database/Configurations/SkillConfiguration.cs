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
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.IsAcception).HasDefaultValue(false);


            // 1-n: user - user skills
                builder.HasOne(userSkill => userSkill.User)
                    .WithMany(user => user.Skills)
                    .HasForeignKey(userSkill => userSkill.UserId);

            // 1-1: skill - category
            builder.HasOne(skill => skill.Category)
                .WithOne(category => category.Skill)
                .HasForeignKey<Skill>(skill => skill.CategoryId);
        }
    }
}
    