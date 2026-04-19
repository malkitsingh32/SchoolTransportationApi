using DTO.Response;
using DTO.Response.SystemValues;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDriverTypeList
{
    public class GetDriverTypeListQuery : IRequest<CommonResultResponseDto<IList<GetDriverTypeResponseDto>>>
    {
    }
}
