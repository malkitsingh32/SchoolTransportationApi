using Application.Abstraction.Services;
using DTO.Request.Charges;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Charges.Command.AddUpdateCharges
{
    internal class AddUpdateChargesCommandHandler : IRequestHandler<AddUpdateChargesCommand, CommonResultResponseDto<string>>
    {
        private readonly IChargesService _chargessService;
        public AddUpdateChargesCommandHandler(IChargesService chargessService)
        {
            _chargessService = chargessService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(AddUpdateChargesCommand addUpdateChargesCommand, CancellationToken cancellationToken)
        {
            var user = await _chargessService.AddUpdateCharges(addUpdateChargesCommand.Adapt<AddUpdateChargesRequestDto>());
            return await Task.FromResult(user);
        }
    }
}
