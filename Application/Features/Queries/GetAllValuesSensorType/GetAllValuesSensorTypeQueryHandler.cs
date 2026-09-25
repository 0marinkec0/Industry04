using Application.Dto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GetAllValuesSensorType
{
    public class GetAllValuesSensorTypeQueryHandler : IRequestHandler<GetAllValuesSensorTypeQuery, List<SensorDTO>>
    {
        private readonly IAppDbContext _iotContext;

        public GetAllValuesSensorTypeQueryHandler(IAppDbContext iotContext)
        {
            _iotContext = iotContext;
        }

        public async Task<List<SensorDTO>> Handle(GetAllValuesSensorTypeQuery request, CancellationToken cancellationToken)
        {
            var response = await _iotContext.Sensors.Where(x => x.Type == request.type).Select(n => new SensorDTO
            {
                Type = n.Type.ToString(),
                Value = n.Value,
                Time = n.Time
            }).ToListAsync();

            return response;
        }
    }
}
