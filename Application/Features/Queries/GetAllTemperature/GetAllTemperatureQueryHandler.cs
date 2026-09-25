using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Dto;
using Domain.Enums;
using Application.Interfaces;

namespace Application.Features.Queries.GetAllTemperature
{
    public class GetAllTemperatureQueryHandler : IRequestHandler<GetAllTemperatureQuery, List<SensorDTO>>
    {
        private readonly IAppDbContext _iotContext;

        public GetAllTemperatureQueryHandler(IAppDbContext iotContext)
        {
            _iotContext = iotContext;
        }

        public async Task<List<SensorDTO>> Handle(GetAllTemperatureQuery request, CancellationToken cancellationToken)
        {
            var list = await _iotContext.Sensors.Where(x => x.Type == SensorType.Temperature)
               .Select(x => new SensorDTO
               {
                   Type = x.Type.ToString(),
                   Value = x.Value,
                   Time = x.Time.ToLocalTime()
               })
              .ToListAsync();

            return list;
        }
    }
}
