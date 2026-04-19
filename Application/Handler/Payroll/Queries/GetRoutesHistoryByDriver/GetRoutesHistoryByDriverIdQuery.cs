using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Payroll;
using MediatR;

namespace Application.Handler.Payroll.Queries.GetRoutesHistoryByDriver
{
    public class GetRoutesHistoryByDriverIdQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetRoutesHistoryByDriverResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
        public int DriverId { get; set; }
        public DateTime Date { get; set; }
    }
}
