using Application.Dto;
using MediatR;

namespace Application.Features.Queries.GetLatestSensorReadings;

public record GetLatestSensorReadingQuery : IRequest<List<SensorDTO>>;
