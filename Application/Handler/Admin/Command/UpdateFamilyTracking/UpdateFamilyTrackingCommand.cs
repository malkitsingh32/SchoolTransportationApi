using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateFamilyTracking
{
    public class UpdateFamilyTrackingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsTracking { get; set; }
    }
}
