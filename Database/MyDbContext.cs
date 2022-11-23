using Microsoft.EntityFrameworkCore;
using PlayerDuo.Database.Configurations;
using PlayerDuo.Database.Entities;
using PlayerDuo.Database.Extensions;

namespace PlayerDuo.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configuration for entity
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ReportTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new ImageReportConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            // Data seeding
            //modelBuilder.Seed();

        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<ReportType>? ReportTypes { get; set; }
        public DbSet<Skill>? Skills { get; set; }
        public DbSet<Report>? Reports { get; set; }
        public DbSet<ImageReport>? ImageReports { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Message>? Messages { get; set; }
        public DbSet<TradeHistory>? TradeHistories { get; set; }

    }
}
