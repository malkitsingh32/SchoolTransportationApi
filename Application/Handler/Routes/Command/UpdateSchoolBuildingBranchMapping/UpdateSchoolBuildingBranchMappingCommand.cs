using DTO.Request.Routes;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateSchoolBuildingBranchMapping
{
    public class UpdateSchoolBuildingBranchMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public UpdateSchoolBuildingBranchMappingCommand()
        {
            SchoolBuildingBranchList = new List<SchoolBuildingBranchReq>();
            RouteIdList = new List<RouteIdReq>();
        }
        public List<SchoolBuildingBranchReq> SchoolBuildingBranchList { get; set; }
        public List<RouteIdReq> RouteIdList { get; set; }
    }
}
