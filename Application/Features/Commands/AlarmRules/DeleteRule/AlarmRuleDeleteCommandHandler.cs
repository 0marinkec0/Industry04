using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.ResultService;

namespace Application.Features.Commands.AlarmRules.DeleteRule
{
    public class AlarmRuleDeleteCommandHandler : IRequestHandler<AlarmRuleDeleteCommand, Result>
    {
        private readonly IAppDbContext _iotContext;

        public AlarmRuleDeleteCommandHandler(IAppDbContext iotContext)
        {
            _iotContext = iotContext;
        }

        public async Task<Result> Handle(AlarmRuleDeleteCommand request, CancellationToken cancellationToken)
        {
            var rule = await _iotContext.AlarmRules.FirstOrDefaultAsync(x => x.Id == request.ruleId);

            if (rule is not null)
            {
                _iotContext.AlarmRules.Remove(rule);

                await _iotContext.SaveChangesAsync();

                return Result.Success();
            }

            return Result.Failure(new Error(code: "Alarm.Rules", message: "Alarm rule was not found!"));
        }
    }
}
