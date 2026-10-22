using Application.Interfaces;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ActuatorService : IActuatorService
{
    private readonly IAppDbContext _appDbContext;
    private readonly IMqttService _mqttService;

    public ActuatorService(IAppDbContext appDbContext, IMqttService mqttService)
    {
        _appDbContext = appDbContext;
        _mqttService = mqttService;
    }

    public async Task AutomaticModeActuatorControl()
    {
        var currentDay = DateTime.Today.DayOfWeek;

        var currentTime = TimeOnly.FromDateTime(DateTime.Now);

        byte[] payload;

        var actuatorsInAutoMode = await _appDbContext.Actuators
            .Where(x => x.Mode == Mode.Automatic)
            .Include(x => x.Schedules
                .Where(s => s.DayOfWeek == currentDay))
            .ToListAsync();

        if (actuatorsInAutoMode.Count == 0)
            return;

        foreach (var actuator in actuatorsInAutoMode)
        {
            foreach (var schedule in actuator.Schedules)
            {
                if (currentTime >= schedule.StartTime && currentTime <= schedule.EndTime)
                {
                    payload = new byte[] { actuator.ByteId, (byte)ActuatorCommand.ON };

                    await _mqttService.ConnectAsync();
                    await _mqttService.PublishByteAsync("actuator/room-1/led", payload);
                }
                else
                {
                    payload = new byte[] { actuator.ByteId, (byte)ActuatorCommand.OFF };

                    await _mqttService.ConnectAsync();
                    await _mqttService.PublishByteAsync("actuator/room-1/led", payload);
                }
            }
        }
    }
}
