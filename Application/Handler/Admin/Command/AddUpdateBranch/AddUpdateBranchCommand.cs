

using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddUpdateBranch
{
    public  class AddUpdateBranchCommand : IRequest<CommonResultResponseDto<string>>
    {
        public string BranchName { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalCell { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BuildingSys { get; set; }
        public int Gender { get; set; }
        public List<int> Grade { get; set; }

    }
}
