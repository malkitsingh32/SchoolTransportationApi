using Application.Common.Dtos;
using DTO.Request.Street;
using DTO.Response.Streets;

namespace Application.Abstraction.Repositories
{
    public interface IStreetsRepository
    {
        Task<(List<GetStreetsResponseDto>, int)> GetStreets(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId);
        Task<int> AddUpdateStreets(AddUpdateStreetsDto addUpdateStreetsDto);
        Task<int> DeleteStreets(int id);
        Task<IList<GetStreetListResponseDto>> GetStreetList();
        Task<(List<GetStreetsResponseDto>, int)> GetStreetsByRouteAndArea(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId);
        Task<int> UpdateStreetRouteMapping(string streetJson);
        Task<int> AddSchoolBuildingBranchMapping(string schoolJson);
    }
}
