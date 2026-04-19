using DTO.Request.Street;
using DTO.Response;
using MediatR;

namespace Application.Handler.Streets.Command.UpdateStreetRouteMapping
{
    public class UpdateStreetRouteMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public UpdateStreetRouteMappingCommand()
        {
            StreetList = new List<StreetReq>();
        }
        public List<StreetReq> StreetList { get; set; }
    }
}
