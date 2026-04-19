using DTO.Response;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.RunMonthlyTask
{
    public class RunMonthlyTaskCommand : IRequest<CommonResultResponseDto<string>>
    {
    }
}
