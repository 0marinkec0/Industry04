using Application.Interfaces;
using Domain.Enities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Seeder;

public class ActuatorSeeder
{
    private readonly IAppDbContext _appDbContext;

    public ActuatorSeeder(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _appDbContext.Actuators.AnyAsync(cancellationToken))
            return;

        var actuators = new List<Actuator>
        {
            new()
            {
                Id = 1,
                ByteId = 0x01,
                Name = "LED 1",
                Mode = Mode.Manual
            },
            new()
            {
                Id = 2,
                ByteId = 0x02,
                Name = "LED 2",
                Mode = Mode.Manual
            }
        };

        _appDbContext.Actuators.AddRange(actuators);

        await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}
