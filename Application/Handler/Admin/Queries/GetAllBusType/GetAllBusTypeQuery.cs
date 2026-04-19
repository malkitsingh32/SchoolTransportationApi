using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllBusType
{
    public class GetAllBusTypeQuery : IRequest<CommonResultResponseDto<IList<GetAllBusTypeResponseDto>>>
    {

    }
}
