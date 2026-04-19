using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.UpdateBulkNtStatus
{
    public class UpdateBulkNtStatusCommand : IRequest<CommonResultResponseDto<string>>
    {
        public List<int> NtIds { get; set; } = new();
        public List<int> StudentIds { get; set; } = new();
    }
}
