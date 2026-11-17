using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class AlarmEventConfiguration : IEntityTypeConfiguration<AlarmEvent>
{
    public void Configure(EntityTypeBuilder<AlarmEvent> builder)
    {
        builder.HasKey(x => x.AlarmId);
    }
}
