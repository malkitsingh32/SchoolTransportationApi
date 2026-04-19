using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Street;
using DTO.Response;
using DTO.Response.Streets;

namespace Application.Abstraction.Services
{
    public interface IStreetsService
    {
        Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> GetStreets(string filterModel, ServerRowsRequest commonRequest, string getSort, int RouteId);
        Task<CommonResultResponseDto<string>> AddUpdateStreets(AddUpdateStreetsDto addUpdateStreetsDto);

        Task<CommonResultResponseDto<string>> DeleteStreets(int id);
        Task<CommonResultResponseDto<IList<GetStreetListResponseDto>>> GetStreetList();
        Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> GetStreetsByRouteAndArea(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId);
        Task<CommonResultResponseDto<string>> UpdateStreetRouteMapping(UpdateStreetRouteMappingDto updateStreetRouteMappingDto);
        Task<CommonResultResponseDto<string>> AddSchoolBuildingBranchMapping(AddSchoolBuildingBranchMappingDto addSchoolBuildingBranchMappingDto);
    }
}
