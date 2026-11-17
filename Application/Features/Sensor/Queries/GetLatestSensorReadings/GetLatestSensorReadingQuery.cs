using Application.Dto;
using MediatR;

namespace Application.Features.Sensor.Queries.GetLatestSensorReadings;

public record GetLatestSensorReadingQuery : IRequest<List<SensorDTO>>;
