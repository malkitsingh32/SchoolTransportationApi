using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateTrackingTime
{
    public class UpdateTrackingTimeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int TrackingTimeId { get; set; }
        public int? TrackingStartTime { get; set; }
        public int? TrackingEndTime { get; set; }
    }
}
