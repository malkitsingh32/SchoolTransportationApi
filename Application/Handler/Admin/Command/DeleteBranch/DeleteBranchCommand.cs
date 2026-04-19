

using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteBranch
{
    public class DeleteBranchCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
