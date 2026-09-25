using Application.Dto;
using MediatR;

namespace Application.Features.Queries.GetAllTemperature
{
    public record GetAllTemperatureQuery : IRequest<List<SensorDTO>>;
}
