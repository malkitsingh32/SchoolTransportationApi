using DTO.Request.Admin;
using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateGradeMapping
{
    public class UpdateGradeMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public UpdateGradeMappingCommand()
        {
            GradeList = new List<GradeReq>();
        }
        public List<GradeReq> GradeList { get; set; }
    }
}
