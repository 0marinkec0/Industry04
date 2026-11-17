using Application.Features.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Respons.Actuator;
using Shared.ResultService;

namespace Application.Features.Actuator.Queries.GetActuator;

public class GetActuatorQueryHandler : IRequestHandler<GetActuatorQuery, Result<List<ActuatorResponse>>>
{
    private readonly IAppDbContext _appDbContext;

    public GetActuatorQueryHandler(IAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Result<List<ActuatorResponse>>> Handle(GetActuatorQuery request, CancellationToken cancellationToken)
    {
        var actuator = await _appDbContext.Actuators.Select(x => new ActuatorResponse
        {
            Id = x.Id,
            Name = x.Name,
            Mode = x.Mode.ToString()
        }).ToListAsync();

        if (actuator is null)
            return Result<List<ActuatorResponse>>.Failure(ActuatorError.ActuatorNotFound);

        return Result<List<ActuatorResponse>>.Success(actuator);
    }
}
