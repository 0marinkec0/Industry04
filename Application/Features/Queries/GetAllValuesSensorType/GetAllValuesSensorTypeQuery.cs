using Application.Dto;
using Domain.Enums;
using MediatR;

namespace Application.Features.Queries.GetAllValuesSensorType
{
    public record GetAllValuesSensorTypeQuery(SensorType type) : IRequest<List<SensorDTO>>;
}
