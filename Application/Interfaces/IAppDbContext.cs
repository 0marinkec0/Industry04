using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces;

public interface IAppDbContext
{
    DbSet<Sensor> Sensors { get;}
    DbSet<AlarmRule> AlarmRules { get;}
    DbSet<AlarmEvent> AlarmEvents { get; }
    DbSet<Actuator> Actuators { get;}
    DbSet<Schedule> Schedules { get;}
    Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
}
