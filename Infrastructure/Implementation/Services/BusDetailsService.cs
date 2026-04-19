using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.BusDetails;
using DTO.Response;
using DTO.Response.Bus;
using DTO.Response.BusStatus;
using Helper;
using Helper.Constant;

namespace Infrastructure.Implementation.Services
{
    public class BusDetailsService : IBusDetailsService
    {
        private readonly IBusDetailsRepository _busDetailsRepository;
        public BusDetailsService(IBusDetailsRepository busDetailsRepository)
        {
            _busDetailsRepository = busDetailsRepository;
        }

        public async Task<CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>> AddUpdateBusDetails(AddUpdateBusDetailsRequestDto addUpdateBusDetailsRequestDto)

        {
            if (addUpdateBusDetailsRequestDto.IsOverWrite != true && addUpdateBusDetailsRequestDto.DefaultDriver != null)
            {
                var bus = await _busDetailsRepository.CheckDriverHasBus(addUpdateBusDetailsRequestDto);
                if (bus.Count > 0)
                {
                    return CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>.Success(new string[] { ActionStatusConstant.Updated }, bus);
                }
            }

            if (addUpdateBusDetailsRequestDto.IsTempOverWrite != true && addUpdateBusDetailsRequestDto.TempDefaultDriver != null)
            {
                var bus = await _busDetailsRepository.CheckDriverHasTempBus(addUpdateBusDetailsRequestDto);
                if (bus.Count > 0)
                {
                    return CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>.Success(new string[] { ActionStatusConstant.Updated }, bus);
                }
            }
            var busDetailsId = await _busDetailsRepository.AddUpdateBusDetails(addUpdateBusDetailsRequestDto);
            if (busDetailsId > 0 && busDetailsId != addUpdateBusDetailsRequestDto.BusDetailID)
            {
                return CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>.Success(new string[] { ActionStatusConstant.Created }, null, busDetailsId);
            }
            else if (busDetailsId < 0)
            {
                return CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<IList<CheckDriverHasBusResponseDto>>.Success(new string[] { ActionStatusConstant.Updated }, null, busDetailsId);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteBusDetails(int id)
        {
            var busDetailsId = await _busDetailsRepository.DeleteBusDetails(id);
            if (busDetailsId == "1")
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, 0);
            }
            else if (!string.IsNullOrEmpty(busDetailsId))
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "This bus cannot be deleted because it is assigned to one or more routes " + busDetailsId }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<IList<GetBusesResponseDto>>> GetAllBusDetails()
        {
            var busDetails = await _busDetailsRepository.GetAllBusDetails();
            return CommonResultResponseDto<IList<GetBusesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, busDetails);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>> GetBuses(string filterModel, ServerRowsRequest commonRequest, string getSort, int routId)
        {
            var (buses, total) = await _busDetailsRepository.GetBuses(filterModel, commonRequest, getSort, routId);
            return CommonResultResponseDto<PaginatedList<GetBusesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBusesResponseDto>(buses, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetBusStatusResponseDto>>> GetBusStatus()
        {
            var busStatus = await _busDetailsRepository.GetBusStatus();
            return CommonResultResponseDto<IList<GetBusStatusResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, busStatus);
        }

        public async Task<CommonResultResponseDto<IList<BusPosition>>> GetRecentBusPositions(string busName, int routeId)
        {
            var busStatus = await _busDetailsRepository.GetRecentBusPositions(busName, routeId);
            return CommonResultResponseDto<IList<BusPosition>>.Success(new string[] { ActionStatusHelper.Success }, busStatus);
        }
    }
}
