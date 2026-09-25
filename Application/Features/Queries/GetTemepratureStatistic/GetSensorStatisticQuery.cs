using Application.Dto;
using MediatR;

namespace Application.Features.Queries.GetTemepratureStatistic
{
    public class GetSensorStatisticQuery : IRequest<List<MeasurementsStatisticsDTO>>;

}
