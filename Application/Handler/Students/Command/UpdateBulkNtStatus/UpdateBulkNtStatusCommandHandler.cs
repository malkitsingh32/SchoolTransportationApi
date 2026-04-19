using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.UpdateBulkNtStatus
{
    public class UpdateBulkNtStatusCommandHandler : IRequestHandler<UpdateBulkNtStatusCommand, CommonResultResponseDto<string>>
    {
        private readonly IStudentsService _studentsService;
        public UpdateBulkNtStatusCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<string>> Handle(UpdateBulkNtStatusCommand updateBulkNtStatusCommand, CancellationToken cancellationToken)
        {
            return await _studentsService.UpdateBulkNtStatus(updateBulkNtStatusCommand.Adapt<UpdateBulkNtStatusRequestDto>());
        }
    }
}
