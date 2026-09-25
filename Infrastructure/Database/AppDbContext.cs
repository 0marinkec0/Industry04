using Application.Interfaces;
using Domain.Enities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IAppDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Sensor> Sensors => Set<Sensor>();
        public DbSet<AlarmRule> AlarmRules => Set<AlarmRule>();
        public DbSet<AlarmEvent> AlarmEvents => Set<AlarmEvent>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            IdentityConfiguration(modelBuilder);

            modelBuilder.Entity<Sensor>()
            .Property(x => x.Type)
            .HasConversion<string>();

            modelBuilder.Entity<AlarmRule>().HasKey(x=>x.Id);

            modelBuilder.Entity<AlarmRule>()
                .Property(x => x.SensorType).HasConversion<string>();
           
            modelBuilder.Entity<AlarmRule>()
            .Property(x => x.Operator).HasConversion<string>();

            modelBuilder.Entity<AlarmEvent>().HasKey(x => x.AlarmId);
        }

        async Task IAppDbContext.SaveChangesAsync(CancellationToken cancellationToken)
        {
            await base.SaveChangesAsync(cancellationToken);
        }

        private static void IdentityConfiguration(ModelBuilder builder)
        {
            builder.Entity<User>().Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<User>().Property(u => u.UserName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityRole<int>>().ToTable("Roles");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<int>>().ToTable("UserTokens");
        }
    }
}
