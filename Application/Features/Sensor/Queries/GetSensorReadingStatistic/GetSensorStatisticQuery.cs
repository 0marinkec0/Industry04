using Application.Dto;
using MediatR;

namespace Application.Features.Sensor.Queries.GetSensorReadingStatistic
{
    public class GetSensorStatisticQuery : IRequest<List<MeasurementsStatisticsDTO>>;

}
