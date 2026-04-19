using Application.Abstraction.Services;
using DTO.Request.Payments;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Payments.Command.RecordPayment
{
    public class RecodePaymentQueryHandler : IRequestHandler<RecodePaymentCommand, CommonResultResponseDto<string>>
    {
        private readonly IPaymentsService _paymentsService;
        public RecodePaymentQueryHandler(IPaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(RecodePaymentCommand recodePaymentCommand, CancellationToken cancellationToken)
        {
            var user = await _paymentsService.RecodePayment(recodePaymentCommand.Adapt<RecodePaymentRequestDto>());
            return await Task.FromResult(user);
        }
    }
}
