using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Request.Street;
using DTO.Response;
using DTO.Response.Streets;
using Helper;
using Helper.Constant;
using Newtonsoft.Json;

namespace Infrastructure.Implementation.Services
{
    public class StreetsService : IStreetsService
    {
        private readonly IStreetsRepository _streetsRepository;
        public StreetsService(IStreetsRepository streetsRepository)
        {
            _streetsRepository = streetsRepository;
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateStreets(AddUpdateStreetsDto addUpdateStreetsDto)
        {
            var studentID = await _streetsRepository.AddUpdateStreets(addUpdateStreetsDto);
            if (studentID > 0 && studentID != addUpdateStreetsDto.ID)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, studentID);
            }
            else if (studentID < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, studentID);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteStreets(int id)
        {
            var streetId = await _streetsRepository.DeleteStreets(id);
            if (streetId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, streetId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> GetStreets(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            var (streets, total) = await _streetsRepository.GetStreets(filterModel, commonRequest, getSort, routeId);
            return CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStreetsResponseDto>(streets, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetStreetListResponseDto>>> GetStreetList()
        {
            var areaList = await _streetsRepository.GetStreetList();
            return CommonResultResponseDto<IList<GetStreetListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, areaList);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>> GetStreetsByRouteAndArea(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            var (streets, total) = await _streetsRepository.GetStreetsByRouteAndArea(filterModel, commonRequest, getSort, routeId);
            return CommonResultResponseDto<PaginatedList<GetStreetsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStreetsResponseDto>(streets, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> UpdateStreetRouteMapping(UpdateStreetRouteMappingDto updateStreetRouteMappingDto)
        {
            var studentID = await _streetsRepository.UpdateStreetRouteMapping(JsonConvert.SerializeObject(updateStreetRouteMappingDto.StreetList));
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, studentID);
        }

        public async Task<CommonResultResponseDto<string>> AddSchoolBuildingBranchMapping(AddSchoolBuildingBranchMappingDto addSchoolBuildingBranchMappingDto)
        {
            var school = await _streetsRepository.AddSchoolBuildingBranchMapping(JsonConvert.SerializeObject(addSchoolBuildingBranchMappingDto.schoolList));
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, school);
        }
    }
}
