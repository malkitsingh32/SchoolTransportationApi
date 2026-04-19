using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllRunType
{
    public class GetAllRunTypeQuery : IRequest<CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>>
    {
    }
}
