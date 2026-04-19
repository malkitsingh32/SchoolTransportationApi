using DTO.Request.Students;
using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.UpdateBusStopIndex
{
    public class UpdateBusStopIndexCommand : IRequest<CommonResultResponseDto<string>>
    {
        public UpdateBusStopIndexCommand()
        {
            BusStopList = new List<BusStopReq>();
        }
        public List<BusStopReq> BusStopList { get; set; }
    }
}
