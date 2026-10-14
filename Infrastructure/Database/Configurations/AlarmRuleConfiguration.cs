using Domain.Enities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class AlarmRuleConfiguration : IEntityTypeConfiguration<AlarmRule>
{
    public void Configure(EntityTypeBuilder<AlarmRule> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SensorType).HasConversion<string>();

        builder.Property(x => x.Operator).HasConversion<string>();
    }
}
