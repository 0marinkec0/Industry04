using Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Sensor> Sensors { get;}
        DbSet<AlarmRule> AlarmRules { get;}
        DbSet<AlarmEvent> AlarmEvents { get; }

        Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
