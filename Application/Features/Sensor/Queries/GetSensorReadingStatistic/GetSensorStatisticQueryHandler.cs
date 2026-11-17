using Application.Dto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Sensor.Queries.GetSensorReadingStatistic
{
    public class GetSensorStatisticQueryHandler : IRequestHandler<GetSensorStatisticQuery, List<MeasurementsStatisticsDTO>>
    {
        private readonly IAppDbContext _iotContext;

        public GetSensorStatisticQueryHandler(IAppDbContext iotContext)
        {
            _iotContext = iotContext;
        }

        public async Task<List<MeasurementsStatisticsDTO>> Handle(GetSensorStatisticQuery request, CancellationToken cancellationToken)
        {
            var response = await _iotContext.Sensors
                .AsNoTracking()
                .GroupBy(x=> x.Type)
                .Select(x => new MeasurementsStatisticsDTO 
                {
                    SensorType = x.Key.ToString(),
                    Average = Math.Round(x.Average(x => x.Value), 2),
                    Maximal = x.Max(x => x.Value),
                    Minimal = x.Min(x => x.Value)
                }).ToListAsync();

            return response;

        }
    }
}
