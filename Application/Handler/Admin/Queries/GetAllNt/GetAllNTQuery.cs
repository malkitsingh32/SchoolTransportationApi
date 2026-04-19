using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllNt
{
    public class GetAllNTQuery : IRequest<CommonResultResponseDto<IList<GetAllNTResponseDto>>>
    {
    }
}
