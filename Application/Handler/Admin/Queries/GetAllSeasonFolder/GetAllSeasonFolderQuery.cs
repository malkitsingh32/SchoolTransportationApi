using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllSeasonFolder
{
    public class GetAllSeasonFolderQuery : IRequest<CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>>
    {
    }
}
