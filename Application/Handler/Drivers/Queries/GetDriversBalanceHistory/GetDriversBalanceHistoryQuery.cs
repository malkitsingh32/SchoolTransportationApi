using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Driver;
using MediatR;

namespace Application.Handler.Drivers.Queries.GetDriversBalanceHistory
{
    public class GetDriversBalanceHistoryQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int DriverId { get; set; }
    }
}
