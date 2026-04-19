using DTO.Response;
using DTO.Response.Admin;
using MediatR;

namespace Application.Handler.Admin.Queries.GetAllDistricts
{
    public class GetAllDistrictsQuery : IRequest<CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>>
    {

    }
}
