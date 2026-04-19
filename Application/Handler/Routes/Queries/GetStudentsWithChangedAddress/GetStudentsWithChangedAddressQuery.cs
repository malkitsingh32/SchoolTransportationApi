using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Routes;
using MediatR;

namespace Application.Handler.Routes.Queries.GetStudentsWithChangedAddress
{
    public class GetStudentsWithChangedAddressQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public string? RouteTypeIds { get; set; }
        public int? GenderId { get; set; }
    }
}
