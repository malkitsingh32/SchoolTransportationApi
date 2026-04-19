using DTO.Response;
using MediatR;

namespace Application.Handler.CardKnoxPayment.Command.DeleteCardknoxPayment
{
    public class DeleteCardknoxPaymentCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}
