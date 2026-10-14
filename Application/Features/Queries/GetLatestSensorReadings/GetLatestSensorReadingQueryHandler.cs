using Application.Dto;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Application.Features.Queries.GetLatestSensorReadings;

public class GetLatestSensorReadingQueryHandler : IRequestHandler<GetLatestSensorReadingQuery, List<SensorDTO>>
{
    private readonly IAppDbContext _appDbContext;

    public GetLatestSensorReadingQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<SensorDTO>> Handle(GetLatestSensorReadingQuery request, CancellationToken cancellationToken)
    {
        var output = await _appDbContext.Sensors
                .OrderByDescending(x => x.Time)
                .Take(2)
                .OrderBy(x => x.Time)
                .Select(x => new SensorDTO
                {
                  Type = x.Type.ToString(),
                  Value = x.Value,
                  Time = x.Time
                }).ToListAsync();

        return output;
    }
}
