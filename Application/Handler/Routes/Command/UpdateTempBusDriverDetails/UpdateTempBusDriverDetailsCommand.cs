using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateTempBusDriverDetails
{
    public class UpdateTempBusDriverDetailsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int DriverID { get; set; }
        public int? TempBus { get; set; }
        public string? RunType { get; set; }
        public DateTime? TempBusStartTime { get; set; }
        public DateTime? TempBusEndTime { get; set; }
    }
}
