using MediatR;
using Shared.Respons.Actuator;
using Shared.ResultService;

namespace Application.Features.Queries.Actuator;

public record GetActuatorQuery : IRequest<Result<List<ActuatorResponse>>>;

