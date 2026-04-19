using Application.Common.Dtos;
using Domain.Entities;
using DTO.Request.Drivers;
using DTO.Request.Routes;
using DTO.Response.Bus;
using DTO.Response.Driver;
using DTO.Response.SystemValues;

namespace Application.Abstraction.Repositories
{
    public interface IDriversRepository
    {
        Task<IList<GetDriversListResponseDto>> GetDriversList();

        Task<(List<GetDriversResponseDto>, int)> GetDrivers(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId);
        Task<(List<GetDriversBalanceHistoryResponseDto>, int)> GetDriversBalanceHistory(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId);

        Task<int> AddUpdateDriversDetails(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto);
        Task<CheckDriverHasBusResponseDto> CheckBusHasDriver(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto);
        Task<int> DeductReservedAccountBalance(DeductReservedAccountBalanceRequestDto deductReservedAccountBalanceRequestDto);
        Task<string> DeleteDrivers(int id, bool IsFromRoute, int? RouteId);
        Task<IList<GetDriversResponseDto>> GetDriversByBusId(string? id);
        Task<IList<GetDriverTypeResponseDto>> GetDriverTypeList();
        Task<Drivers> GetDriverByEmail(string email, int driverId);
        Task<string> ResetDriverPassword(Drivers drivers, ResetDriverPasswordDto resetDriverPasswordDto);
        Task<string> SendOtpOnEmail(Drivers drivers);
        Task<Drivers> CheckOTP(CheckOTPRequestDto checkOTPRequestDto);
        Task<string> ResetUserPassword(Users users, ResetDriverPasswordDto resetDriverPasswordDto);
        Task<int> UpdateDriversDetail(Users users, byte[]? passwordHash, byte[]? passwordSalt);
        Task<int> IsExistEmail(Users users);
        Task<int> AssignRouteToDriver(AssignRouteToDriverRequestDto assignRouteToDriverRequestDto);
        Task<int> DeleteBusAndDriverRoute(DeleteBusAndDriverRouteRequestDto deleteBusAndDriverRouteRequestDto);
        Task<IList<ExportDriversListDto>> ExportDriversList();


    }
}
