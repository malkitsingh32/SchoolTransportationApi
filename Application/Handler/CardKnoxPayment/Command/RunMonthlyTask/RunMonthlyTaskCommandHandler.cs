using Application.Abstraction.Services;
using DTO.Response;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.RunMonthlyTask
{
    public class RunMonthlyTaskCommandHandler : IRequestHandler<RunMonthlyTaskCommand, CommonResultResponseDto<string>>
    {
        private readonly ICardknoxMonthlyTaskService _cardknoxMonthlyTaskService;

        public RunMonthlyTaskCommandHandler(ICardknoxMonthlyTaskService cardknoxMonthlyTaskService)
        {
            _cardknoxMonthlyTaskService = cardknoxMonthlyTaskService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(RunMonthlyTaskCommand request, CancellationToken cancellationToken)
        {
            await _cardknoxMonthlyTaskService.RunMonthlyTask(cancellationToken);
            return CommonResultResponseDto<string>.Success(new[] { "Monthly task completed successfully" }, null, 1);
        }
    }
}
