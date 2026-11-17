using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ActuatorConfiguration : IEntityTypeConfiguration<Actuator>
{
    public void Configure(EntityTypeBuilder<Actuator> builder)
    {
        builder.HasMany(a => a.Schedules)
            .WithOne(s => s.Actuator)
            .HasForeignKey(s => s.ActuatorId);
    }
}
