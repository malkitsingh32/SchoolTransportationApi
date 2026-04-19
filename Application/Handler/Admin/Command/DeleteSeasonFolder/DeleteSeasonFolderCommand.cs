using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSeasonFolder
{
    public class DeleteSeasonFolderCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }

    }
}
