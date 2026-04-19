using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Students.Queries.ExportStudentsList;
using Domain.Entities;
using DTO.Request.Drivers;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Bus;
using DTO.Response.Driver;
using DTO.Response.SystemValues;

namespace Application.Abstraction.Services
{
    public interface IDriversService
    {
        Task<CommonResultResponseDto<IList<GetDriversListResponseDto>>> GetDriversList();
        Task<CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>> GetDrivers(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId );
        Task<CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>> GetDriversBalanceHistory(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId );
        Task<CommonResultResponseDto<CheckDriverHasBusResponseDto>> AddUpdateDriversDetails(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto);
        Task<CommonResultResponseDto<string>> DeductReservedAccountBalance(DeductReservedAccountBalanceRequestDto deductReservedAccountBalanceRequestDto);
        Task<CommonResultResponseDto<string>> DeleteDrivers(int id,bool IsFromRoute,int? RouteId);
        Task<CommonResultResponseDto<IList<GetDriverTypeResponseDto>>> GetDriverTypeList();
        Task<CommonResultResponseDto<string>> ResetDriverPassword(ResetDriverPasswordDto resetDriverPasswordDto);
        Task<CommonResultResponseDto<string>> SendOtpOnEmail(SendOtpOnEmailRequestDto sendOtpOnEmailRequestDto);
        Task<CommonResultResponseDto<Drivers>> CheckOTP(CheckOTPRequestDto checkOTPRequestDto);
        Task<CommonResultResponseDto<string>> AssignRouteToDriver(AssignRouteToDriverRequestDto assignRouteToDriverRequestDto);
        Task<CommonResultResponseDto<string>> DeleteBusAndDriverRoute(DeleteBusAndDriverRouteRequestDto deleteBusAndDriverRouteRequestDto);
        Task<CommonResultResponseDto<string>> SendLinkToResetDriverPassword(SendLinkToResetDriverPasswordDto sendLinkToResetDriverPasswordDto);
        Task<ExportFileResult> ExportDriversList();


    }
}
