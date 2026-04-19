using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.UpdateBusStopIndex
{
    public class UpdateBusStopIndexCommandHandler : IRequestHandler<UpdateBusStopIndexCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public UpdateBusStopIndexCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<string>> Handle(UpdateBusStopIndexCommand updateBusStopIndexCommand, CancellationToken cancellationToken)
        {
            return await _studentsService.UpdateBusStopIndex(updateBusStopIndexCommand.Adapt<UpdateBusStopIndexDto>());
        }
    }
}
