using Application.Dto;
using MediatR;

namespace Application.Features.Sensor.Queries.GetAllTemperature
{
    public record GetAllTemperatureQuery : IRequest<List<SensorDTO>>;
}
