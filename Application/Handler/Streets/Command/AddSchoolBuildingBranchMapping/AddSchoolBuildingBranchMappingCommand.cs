using DTO.Request.Street;
using DTO.Response;
using MediatR;

namespace Application.Handler.Streets.Command.AddSchoolBuildingBranchMapping
{
    public class AddSchoolBuildingBranchMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public AddSchoolBuildingBranchMappingCommand()
        {
            schoolList = new List<AddSchoolReq>();
        }
        public List<AddSchoolReq> schoolList { get; set; }
    }
}
