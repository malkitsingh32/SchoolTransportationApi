using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDeductionAmount
{
    public class AddDeductionAmountCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
    }
}
