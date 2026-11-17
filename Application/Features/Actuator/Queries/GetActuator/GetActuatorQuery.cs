using MediatR;
using Shared.Respons.Actuator;
using Shared.ResultService;

namespace Application.Features.Actuator.Queries.GetActuator;

public record GetActuatorQuery : IRequest<Result<List<ActuatorResponse>>>;

