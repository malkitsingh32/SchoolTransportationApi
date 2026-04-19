using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSeasonFolder
{
    public class AddSeasonFolderCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string SeasonName { get; set; }
        public bool IsDefault { get; set; }
        public int UserId { get; set; }

    }
}
