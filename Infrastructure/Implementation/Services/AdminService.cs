using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Admin.Command.ImportKml;
using Application.Handler.Students.Queries.ExportStudentsList;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DTO.Request.Admin;
using DTO.Response;
using DTO.Response.Admin;
using DTO.Response.SystemValues;
using DTO.Response.User;
using Helper;
using Helper.Constant;
using Infrastructure.Implementation.Common;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Data;
using System.Xml.Linq;

namespace Infrastructure.Implementation.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IHostingEnvironment _environment;
        public AdminService(IAdminRepository adminRepository, IHostingEnvironment environment)
        {
            _adminRepository = adminRepository;
            _environment = environment;
        }

        public async Task<CommonResultResponseDto<List<Permissions>>> GetPermissionsByRoleId(int roleId)
        {
            var permissions = await _adminRepository.GetPermissionsByRoleId(roleId);
            return CommonResultResponseDto<List<Permissions>>.Success(new string[] { ActionStatusHelper.Success }, permissions);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>> GetStreetAndAreaMapped(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetStreetAndAreaMapped(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetStreetAndAreaMappedResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStreetAndAreaMappedResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>> GetAreas(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetAreas(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetAreasResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetAreasResponseDto>(areas, total), 0);
        }


        public async Task<CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>> GetAllSeasonFolder()
        {
            var season = await _adminRepository.GetAllSeasonFolder();
            return CommonResultResponseDto<IList<GetSeasonFolderResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, season);
        }

        public async Task<CommonResultResponseDto<IList<GetAllRolesResponseDto>>> GetRoles()
        {
            var roles = await _adminRepository.GetRoles();
            return CommonResultResponseDto<IList<GetAllRolesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, roles);
        }
        public async Task<CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>> GetPredefinedSMSMessages()
        {
            var messages = await _adminRepository.GetPredefinedSMSMessages();
            return CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, messages);
        }

        public async Task<CommonResultResponseDto<string>> UpdatePermissionByRoleId(int permissionId, int roleId, string permissionType, bool canView, bool canEdit)
        {
            var perissionId = await _adminRepository.UpdatePermissionByRoleId(permissionId, roleId, permissionType, canView, canEdit);
            if (perissionId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, perissionId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>> GetSchoolYears(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetSchoolYears(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetSchoolYearsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSchoolYearsResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>> GetGender(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetGender(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetGenderResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetGenderResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>> GetDistrict(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetDistrict(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetDistrictResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetDistrictResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>> GetSchools(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetSchools(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetSchoolsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSchoolsResponseDto>(areas, total), 0);
        }
        public async Task<CommonResultResponseDto<string>> AddRole(string roleName)
        {
            var roleId = await _adminRepository.AddRole(roleName);
            if (roleId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, roleId);
            }
            else if (roleId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Role already exists" }, null);
            }
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>> GetBuilding(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetBuilding(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetBuildingResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBuildingResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetCcResponseDto>>> GetCc(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetCc(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetCcResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetCcResponseDto>(areas, total), 0);

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>> GetChargeStructure(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetChargeStructure(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetChargeStructureResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetChargeStructureResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetNTResponseDto>>> GetNT(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (areas, total) = await _adminRepository.GetNT(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetNTResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetNTResponseDto>(areas, total), 0);

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>> GetBusType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (busType, total) = await _adminRepository.GetBusType(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetBusTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBusTypeResponseDto>(busType, total), 0);

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>> GetDriverType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (driverType, total) = await _adminRepository.GetDriverType(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetDriverTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetDriverTypeResponseDto>(driverType, total), 0);

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>> GetDecductionAmount(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (amount, total) = await _adminRepository.GetDecductionAmount(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetDecductionAmountResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetDecductionAmountResponseDto>(amount, total), 0);

        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>> GetSearchLocation(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (currentLocation, total) = await _adminRepository.GetSearchLocation(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetSearchLocationResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSearchLocationResponseDto>(currentLocation, total), 0);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>> GetSeasonFolder(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (currentLocation, total) = await _adminRepository.GetSeasonFolder(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetSeasonFolderResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSeasonFolderResponseDto>(currentLocation, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> AddStreetsAreaMapping(int Id, string StreetName, int AreaId, int userId, int districtId)
        {
            var streetsAreaMappingId = await _adminRepository.AddStreetsAreaMapping(Id, StreetName, AreaId, userId, districtId);
            if (streetsAreaMappingId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, streetsAreaMappingId);
            }
            else if (streetsAreaMappingId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "StreetsAreaMapping already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddAreas(int id, string areaName, int userId, string shortName)
        {
            var areaId = await _adminRepository.AddAreas(id, areaName, userId, shortName);
            if (areaId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, areaId);
            }
            else if (areaId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "StreetsAreaMapping already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddSchoolYears(int id, int schoolYear, int numberOfStudents, int userId)
        {
            var SchoolYearId = await _adminRepository.AddSchoolYears(id, schoolYear, numberOfStudents, userId);
            if (SchoolYearId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, SchoolYearId);
            }
            else if (SchoolYearId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddGender(int id, string gender, int userId)
        {
            var genderId = await _adminRepository.AddGender(id, gender, userId);
            if (genderId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, genderId);
            }
            else if (genderId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddDistrict(int id, string districtName, int userId)
        {
            var districtId = await _adminRepository.AddDistrict(id, districtName, userId);
            if (districtId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, districtId);
            }
            else if (districtId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddSchool(int id, string schoolName, string legalName, int userId)
        {
            var schoolId = await _adminRepository.AddSchool(id, schoolName, legalName, userId);
            if (schoolId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, schoolId);
            }
            else if (schoolId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddBuilding(int id, string address, int schoolId, int userId, string buildingName)
        {
            var buildingId = await _adminRepository.AddBuilding(id, address, schoolId, userId, buildingName);
            if (buildingId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, buildingId);
            }
            else if (buildingId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddCC(int id, int cardnoxId, int familyId, int userId)
        {
            var cCId = await _adminRepository.AddCC(id, cardnoxId, familyId, userId);
            if (cCId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, cCId);
            }
            else if (cCId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddChargeStructure(int id, int DistrictId, int? NtId, bool IsFunded, int userId, decimal price, int? SchoolId)
        {
            var chargeStructureId = await _adminRepository.AddChargeStructure(id, DistrictId, NtId, IsFunded, userId, price, SchoolId);

            if (chargeStructureId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, chargeStructureId);
            }
            else if (chargeStructureId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Charge structure already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddNT(int id, string nTName, int userId)
        {
            var nTId = await _adminRepository.AddNT(id, nTName, userId);
            if (nTId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, nTId);
            }
            else if (nTId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "SchoolYear already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddBusType(AddBusTypeRequestDto addBusTypeRequestDto)
        {
            var busTypeId = await _adminRepository.AddBusType(addBusTypeRequestDto);
            if (busTypeId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busTypeId);
            }
            else if (busTypeId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Bus Type already exists" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> UpdateFamilyTracking(UpdateFamilyTrackingRequestDto updateFamilyTrackingRequestDto)
        {
            var familyId = await _adminRepository.UpdateFamilyTracking(updateFamilyTrackingRequestDto);
            if (familyId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, familyId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> UpdateProfile(UpdateProfileRequestDto UpdateProfileRequestDto)
        {
            if (!string.IsNullOrWhiteSpace(UpdateProfileRequestDto.Password))
            {
                CreatePasswordHash(UpdateProfileRequestDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                UpdateProfileRequestDto.PasswordHash = passwordHash;
                UpdateProfileRequestDto.PasswordSalt = passwordSalt;
            }

            var UpdateProfileid = await _adminRepository.UpdateProfile(UpdateProfileRequestDto);
            if (UpdateProfileid > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, UpdateProfileid);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            }

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<CommonResultResponseDto<string>> AddDriverType(int id, string driverTypeName, decimal payRate, int userId)
        {
            var driverTypeId = await _adminRepository.AddDriverType(id, driverTypeName, payRate, userId);
            if (driverTypeId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, driverTypeId);
            }
            else if (driverTypeId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Driver Type already exists" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> AddDeductionAmount(int id, decimal amount, int userId)
        {
            var amountId = await _adminRepository.AddDeductionAmount(id, amount, userId);
            if (amountId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, amountId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> AddSearchLocation(int id, string currntLocation, string currntLocationLongLat, int userId)
        {
            var amountId = await _adminRepository.AddSearchLocation(id, currntLocation, currntLocationLongLat, userId);
            if (amountId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, amountId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> AddUpdateBranch(AddUpdateBranchRequestDto addUpdateBranchRequestDto)
        {
            var branchId = await _adminRepository.AddUpdateBranch(addUpdateBranchRequestDto);
            if (branchId > 0)
            {
                if (addUpdateBranchRequestDto.Id > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, branchId);
                }
                else
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, branchId);
                }
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> UpdateChargeStructure(int id, bool isFunded)
        {
            var userId = await _adminRepository.UpdateChargeStructure(id, isFunded);

            if (userId > 0)
            {
                return CommonResultResponseDto<string>.Success(
                    new string[] { ActionStatusConstant.Updated },
                    null,
                    userId
                );
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(
                    new string[] { "Something went wrong" },
                    null
                );
            }
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateGrade(AddUpdateGradeRequestDto addUpdateGradeRequestDto)
        {
            var gradeId = await _adminRepository.AddUpdateGrade(addUpdateGradeRequestDto);
            if (gradeId > 0)
            {
                if (addUpdateGradeRequestDto.Id > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, gradeId);
                }
                else
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, gradeId);
                }
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddSeasonFolder(AddSeasonFolderRequestDto addSeasonFolderRequestDto)
        {
            var seasonId = await _adminRepository.AddSeasonFolder(addSeasonFolderRequestDto);
            if (seasonId > 0)
            {
                if (addSeasonFolderRequestDto.Id > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, seasonId);
                }
                else
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, seasonId);
                }
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> GetAreaList()
        {
            var areaList = await _adminRepository.GetAreaList();
            return CommonResultResponseDto<IList<GetAreaListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, areaList);
        }

        public async Task<CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>> GetAllDistricts()
        {
            var districts = await _adminRepository.GetAllDistricts();
            return CommonResultResponseDto<IList<GetAllDistrictsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, districts);
        }

        public async Task<CommonResultResponseDto<IList<GetAllNTResponseDto>>> GetAllNT()
        {
            var districts = await _adminRepository.GetAllNT();
            return CommonResultResponseDto<IList<GetAllNTResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, districts);
        }

        public async Task<CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>> GetAllSchools()
        {
            var schools = await _adminRepository.GetAllSchools();
            return CommonResultResponseDto<IList<GetAllSchoolsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, schools);
        }

        public async Task<CommonResultResponseDto<string>> DeleteStreetAreaMapping(int id)
        {
            var streetAreaMappingId = await _adminRepository.DeleteStreetAreaMapping(id);
            if (streetAreaMappingId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, streetAreaMappingId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteArea(int id, bool isDelete)
        {
            var isAreaFree = await _adminRepository.CheckStudentAreaRelation(id);

            if (isAreaFree == 0 || isDelete)
            {
                var deletedAreaId = await _adminRepository.DeleteArea(id);
                if (deletedAreaId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new[] { ActionStatusConstant.Deleted }, null, deletedAreaId);
                }
                else
                {
                    return CommonResultResponseDto<string>.Failure(new[] { "Something went wrong" }, null);
                }
            }
            else if (isAreaFree == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }

            // If deleteArea is false and the area is free, do nothing
            return CommonResultResponseDto<string>.Failure(new[] { "Delete flag is not set" }, null);
        }


        public async Task<CommonResultResponseDto<string>> DeleteSchoolYears(int id)
        {
            var schoolYearsId = await _adminRepository.DeleteSchoolYears(id);
            if (schoolYearsId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, schoolYearsId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteGender(int id, bool isDelete)
        {
            var gender = await _adminRepository.CheckGenderBeforeDelete(id);
            if (gender == 0 || isDelete)
            {
                var genderId = await _adminRepository.DeleteGender(id);
                if (genderId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, genderId);
                }
                else if (genderId == 0)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "This Gender is in use. It can not be deleted" }, null);
                }
            }
            else if (gender == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteDistrict(int id, bool isDelete)
        {
            var isAreaFree = await _adminRepository.CheckDistrictBeforeDelete(id);
            if (isAreaFree == 0 || isDelete)
            {
                var districtId = await _adminRepository.DeleteDistrict(id);
                if (districtId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, districtId);
                }
                else if (districtId == 0)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "This District is in use. It can not be deleted" }, null);
                }
            }
            else if (isAreaFree == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteSchools(int id, bool isDelete)
        {
            var school = await _adminRepository.CheckSchoolBeforeDelete(id);
            if (school == 0 || isDelete)
            {
                var schoolId = await _adminRepository.DeleteSchools(id);
                if (schoolId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, schoolId);
                }
                else if (schoolId == 0)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "This school is in use. It can not be deleted" }, null);
                }
            }
            else if (school == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteBuilding(int id, bool isDelete)
        {
            var building = await _adminRepository.CheckBuildingBeforeDelete(id);
            if (building == 0 || isDelete)
            {
                var buildingId = await _adminRepository.DeleteBuilding(id);
                if (buildingId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, buildingId);
                }
                else if (buildingId == 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { "This building is in use. It can not be deleted." }, null, buildingId);
                }
            }
            else if (building == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteCC(int id)
        {
            var ccId = await _adminRepository.DeleteCC(id);
            if (ccId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, ccId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteChargeStructure(int id)
        {
            var chargeStructureId = await _adminRepository.DeleteChargeStructure(id);
            if (chargeStructureId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, chargeStructureId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteNt(int id, bool isDelete)
        {
            var nt = await _adminRepository.CheckNTBeforeDelete(id);
            if (nt == 0 || isDelete)
            {
                var ntId = await _adminRepository.DeleteNt(id);
                if (ntId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, ntId);
                }
                else if (ntId == 0)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "This NT is in use. It can not be deleted" }, null);
                }
            }
            else if (nt == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteBusType(int id, bool isDelete)
        {
            var routeType = await _adminRepository.CheckRouteTypeBeforeDelete(id);
            if (routeType == 0 || isDelete)
            {

                var busTypeId = await _adminRepository.DeleteBusType(id);
                if (busTypeId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, busTypeId);
                }
            }
            else if (routeType == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteDriverType(int id, bool isDelete)
        {
            var driverType = await _adminRepository.CheckDriverTypeBeforeDelete(id);
            if (driverType == 0 || isDelete)
            {
                var DriverTypeId = await _adminRepository.DeleteDriverType(id);
                if (DriverTypeId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, DriverTypeId);
                }
            }
            else if (driverType == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }
        public async Task<CommonResultResponseDto<string>> DeleteBranch(int id, bool isDelete)
        {
            var branch = await _adminRepository.CheckBranchBeforeDelete(id);
            if (branch == 0 || isDelete)
            {
                var branchId = await _adminRepository.DeleteBranch(id);
                if (branchId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, branchId);
                }
            }
            else if (branch == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteGrade(DeleteGradeRequestDto deleteGradeRequestDto)
        {
            var grade = await _adminRepository.CheckGradeBeforeDelete(deleteGradeRequestDto);
            if (grade == 0 || deleteGradeRequestDto.IsDelete)
            {
                var gradeId = await _adminRepository.DeleteGrade(deleteGradeRequestDto);
                if (gradeId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, gradeId);
                }
            }
            else if (grade == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<string>> DeleteSeasonFolder(DeleteSeasonFolderRequestDto deleteSeasonFolderRequestDto)
        {
            var chargeStructureId = await _adminRepository.CheckSeasonFolderBeforeDelete(deleteSeasonFolderRequestDto);
            if (chargeStructureId == 0 || deleteSeasonFolderRequestDto.IsDelete)
            {
                var gradeId = await _adminRepository.DeleteSeasonFolder(deleteSeasonFolderRequestDto);
                if (gradeId > 0)
                {
                    return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, gradeId);
                }
            }
            else if (chargeStructureId == 1)
            {
                return CommonResultResponseDto<string>.Failure(new[] { "true" }, null);
            }
            return CommonResultResponseDto<string>.Failure(new string[] { "Delete flag is not set" }, null);
        }

        public async Task<CommonResultResponseDto<IList<GetAllGendersResponseDto>>> GetAllGenders()
        {
            var genders = await _adminRepository.GetAllGenders();
            return CommonResultResponseDto<IList<GetAllGendersResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, genders);
        }

        public async Task<CommonResultResponseDto<IList<GetBuildingListResponseDto>>> GetBuildingList()
        {
            var building = await _adminRepository.GetBuildingList();
            return CommonResultResponseDto<IList<GetBuildingListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, building);
        }

        public async Task<CommonResultResponseDto<IList<GetStatesResponseDto>>> GetStates(int id)
        {
            var states = await _adminRepository.GetStates(id);
            return CommonResultResponseDto<IList<GetStatesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, states);
        }

        public async Task<CommonResultResponseDto<IList<GetCitiesResponseDto>>> GetCities(int id)
        {
            var states = await _adminRepository.GetCities(id);
            return CommonResultResponseDto<IList<GetCitiesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, states);
        }
        public async Task<CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>> GetRouteTypeRequiredRules(int? routeTypeId)
        {
            var states = await _adminRepository.GetRouteTypeRequiredRules(routeTypeId);
            return CommonResultResponseDto<IList<GetRouteTypeRequiredRulesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, states);
        }

        public async Task<CommonResultResponseDto<IList<GetAllBusTypeResponseDto>>> GetAllBusType()
        {
            var busType = await _adminRepository.GetAllBusType();
            return CommonResultResponseDto<IList<GetAllBusTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, busType);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>> GetBranch(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (branchType, total) = await _adminRepository.GetBranch(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetBranchResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetBranchResponseDto>(branchType, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> AddLogs(string message, string from, string messageType)
        {
            var logId = await _adminRepository.AddLog(message, from, messageType);
            if (logId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, logId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>> GetAllGrade(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (gradeType, total) = await _adminRepository.GetAllGrade(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetAllGradeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetAllGradeResponseDto>(gradeType, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>> GetAllFamilyDetail(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (family, total) = await _adminRepository.GetAllFamilyDetail(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetAllFamilyDetailResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetAllFamilyDetailResponseDto>(family, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> UpdateFamilyDetail(UpdateFamilyDetailRequestDto updateFamilyDetailRequestDto)
        {
            var detail = await _adminRepository.UpdateFamilyDetail(updateFamilyDetailRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusHelper.Success }, detail);
        }

        public async Task<CommonResultResponseDto<string>> ImportKml(ImportKmlCommand importKmlCommand)
        {
            string writePath = string.Empty;

            Guid kmlId = Guid.NewGuid();

            writePath = FileUploadHelper.SaveUploadedFile(importKmlCommand.File, _environment, "areaKml", kmlId);
            var fullPath = _environment.WebRootPath + writePath;
            try
            {
                XElement newKml;
                try
                {
                    newKml = XElement.Load(fullPath);
                }
                catch (Exception ex)
                {
                    return CommonResultResponseDto<string>.Failure(new string[] { "Error loading new KML file: " + ex.Message });
                }
                bool definePolygonCenter = true;
                switch (importKmlCommand.KmlType)
                {
                    case "area":
                        var isAreaExist = await _adminRepository.GetAllAreas();
                        List<AreasKMLRequestDto> areasKMLRequestDtos = new List<AreasKMLRequestDto>();
                        var zones = ParseKml(fullPath);
                        foreach (var zone in zones)
                        {
                            if (!isAreaExist.Any(w => w.AreaName == zone.Name))
                            {
                                var areasDto = new AreasKMLRequestDto
                                {
                                    AreaName = zone.Name,
                                    UserId = importKmlCommand.UserId

                                };
                                areasKMLRequestDtos.Add(areasDto);
                            }
                        }
                        await _adminRepository.ImportAreasKml(JsonConvert.SerializeObject(areasKMLRequestDtos));

                        break;
                }
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusHelper.Success }, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Polygon> ParseKml(string filePath, bool definePolygonCenter = false)
        {
            XDocument kmlDoc = XDocument.Load(filePath);
            XNamespace ns = "http://www.opengis.net/kml/2.2";

            var polygons = new List<Polygon>();

            var placemarks = kmlDoc.Descendants(ns + "Placemark").ToList();
            if (!placemarks.Any())
            {
                throw new InvalidOperationException("No 'Placemark' elements found in the KML file.");
            }

            foreach (var placemark in placemarks)
            {
                var nameElement = placemark.Element(ns + "name")
                    ?? throw new InvalidOperationException("A 'Placemark' element is missing the 'name' element.");

                var polygonElement = placemark.Descendants(ns + "Polygon").FirstOrDefault()
                    ?? throw new InvalidOperationException($"The 'Placemark' named '{nameElement.Value}' is missing a 'Polygon' element.");

                var coordinatesElement = polygonElement.Descendants(ns + "coordinates").FirstOrDefault();
                if (coordinatesElement == null || string.IsNullOrWhiteSpace(coordinatesElement.Value))
                {
                    throw new InvalidOperationException($"The 'Polygon' element for '{nameElement.Value}' is missing the 'coordinates' element or it is empty.");
                }

                // Calculate center point
                decimal? centerLat = null, centerLon = null;
                if (definePolygonCenter)
                {
                    (centerLat, centerLon) = ParseCoordinates(coordinatesElement.Value);
                }

                // Add polygon data
                polygons.Add(new Polygon
                {
                    Name = nameElement.Value,
                    CenterLatitude = centerLat,
                    CenterLongitude = centerLon
                });
            }

            return polygons;
        }

        private (decimal? centerLat, decimal? centerLon) ParseCoordinates(string coordinatesData)
        {
            var coordinates = coordinatesData.Trim()
                .Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(coord =>
                {
                    var parts = coord.Split(',');
                    if (parts.Length < 2) throw new FormatException("Invalid coordinate format.");
                    return new { Lon = decimal.Parse(parts[0]), Lat = decimal.Parse(parts[1]) };
                }).ToList();

            if (coordinates.Count < 3)
            {
                return (null, null); // At least 3 points are required for the polygon
            }

            // Calculating center with Shoelace(Double Area Formula)
            decimal sumX = 0, sumY = 0, area = 0;

            for (int i = 0; i < coordinates.Count - 1; i++)
            {
                var x0 = coordinates[i].Lon;
                var y0 = coordinates[i].Lat;
                var x1 = coordinates[i + 1].Lon;
                var y1 = coordinates[i + 1].Lat;

                decimal step = (x0 * y1 - x1 * y0);
                area += step;
                sumX += (x0 + x1) * step;
                sumY += (y0 + y1) * step;
            }

            area /= 2;
            if (area == 0) return (null, null); // If the area is zero, the centroid cannot be calculated

            decimal centerLon = sumX / (6 * area);
            decimal centerLat = sumY / (6 * area);

            return (centerLat, centerLon);
        }

        public async Task<CommonResultResponseDto<string>> UpdateGradeMapping(UpdateGradeMappingDto updateGradeMappingDto)
        {
            var grade = await _adminRepository.UpdateGradeMapping(JsonConvert.SerializeObject(updateGradeMappingDto.GradeList));
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, grade);
        }

        public async Task<CommonResultResponseDto<string>> UpdateBulkGrade(UpdateBulkGradeDto updateBulkGradeDto)
        {
            var detail = await _adminRepository.UpdateBulkGrade(updateBulkGradeDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusHelper.Success }, detail);
        }

        public async Task<CommonResultResponseDto<string>> SetRouteTypeRequiredRules(RouteTypeRequiredRulesDto routeTypeRequiredRulesDto)
        {
            var detail = await _adminRepository.SetRouteTypeRequiredRules(routeTypeRequiredRulesDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusHelper.Success }, detail);
        }

        public async Task<CommonResultResponseDto<GetTrackingTimeQueryResponseDto>> GetTrackingTime()
        {
            var trackingTime = await _adminRepository.GetTrackingTime();
            return CommonResultResponseDto<GetTrackingTimeQueryResponseDto>.Success(new string[] { ActionStatusHelper.Success }, trackingTime);
        }

        public async Task<CommonResultResponseDto<string>> UpdateTrackingTime(UpdateTrackingTimeRequestDto updateTrackingTimeRequestDto)
        {
            var amountId = await _adminRepository.UpdateTrackingTime(updateTrackingTimeRequestDto);
            if (amountId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, amountId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddRunType(AddRunTypeRequestDto addRunTypeRequestDto)
        {
            var runTypeId = await _adminRepository.AddRunType(addRunTypeRequestDto);
            if (runTypeId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, runTypeId);
            }
            else if (runTypeId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Run Type already exists" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>> GetRunType(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (runType, total) = await _adminRepository.GetRunType(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetRunTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetRunTypeResponseDto>(runType, total), 0);
        }

        public async Task<CommonResultResponseDto<string>> DeleteRunType(DeleteRunTypeRequestDto deleteRunTypeRequestDto)
        {
            var runTypeId = await _adminRepository.DeleteRunType(deleteRunTypeRequestDto);
            if (runTypeId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, runTypeId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>> GetAllRunType()
        {
            var busType = await _adminRepository.GetAllRunType();
            return CommonResultResponseDto<IList<GetAllRunTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, busType);
        }

        public async Task<ExportFileResult> ExportFamilyList()
        {
            var familyList = await _adminRepository.ExportFamilyList();

            DataTable table = new DataTable();

            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("FatherFirstName", typeof(string));
            table.Columns.Add("MotherFirstName", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("HomeNumber", typeof(string));
            table.Columns.Add("FatherCell", typeof(string));
            table.Columns.Add("MotherCell", typeof(string));
            table.Columns.Add("State", typeof(string));
            table.Columns.Add("City", typeof(string));
            table.Columns.Add("District", typeof(string));
            table.Columns.Add("Area", typeof(string));
            table.Columns.Add("Zipcode", typeof(int));
            table.Columns.Add("StreetNumber", typeof(int));
            table.Columns.Add("IsTracking", typeof(bool));

            foreach (var item in familyList)
            {
                table.Rows.Add(
                    item.Id,
                    item.LastName,
                    item.FatherFirstName,
                    item.MotherFirstName,
                    item.Address ?? string.Empty,
                    item.HomeNumber ?? string.Empty,
                    item.FatherCell ?? string.Empty,
                    item.MotherCell ?? string.Empty,
                    item.StateName ?? string.Empty,
                    item.CityName ?? string.Empty,
                    item.DistrictName ?? string.Empty,
                    item.AreaName ?? string.Empty,
                    item.Zipcode,
                    item.StreetNumber,
                    item.IsTracking
                );
            }

            int totalCount = familyList.Count;

            string fileName = $"Family({totalCount}).xlsx";

            byte[] fileBytes = ExportToExcel(table);

            return new ExportFileResult
            {
                FileBytes = fileBytes,
                FileName = fileName
            };
        }

        public byte[] ExportToExcel(DataTable table)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
                {
                    // Create WorkbookPart and WorksheetPart
                    WorkbookPart workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    SheetData sheetData = new SheetData();
                    worksheetPart.Worksheet = new Worksheet(sheetData);

                    // Create Sheets and append to Workbook
                    Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                    Sheet sheet = new Sheet()
                    {
                        Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                        SheetId = 1,
                        Name = "Students"
                    };
                    sheets.Append(sheet);

                    // Add header row
                    Row headerRow = new Row();
                    foreach (DataColumn column in table.Columns)
                    {
                        Cell headerCell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue(column.ColumnName)
                        };
                        headerRow.AppendChild(headerCell);
                    }
                    sheetData.AppendChild(headerRow);

                    // Add data rows
                    foreach (DataRow dtRow in table.Rows)
                    {
                        Row newRow = new Row();
                        foreach (var cellData in dtRow.ItemArray)
                        {
                            Cell cell = new Cell
                            {
                                DataType = CellValues.String,
                                CellValue = new CellValue(cellData?.ToString() ?? string.Empty)
                            };
                            newRow.AppendChild(cell);
                        }
                        sheetData.AppendChild(newRow);
                    }

                    workbookPart.Workbook.Save();
                }

                return memoryStream.ToArray();
            }
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>> GetMessages(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (messages, total) = await _adminRepository.GetMessages(filterModel, commonRequest, getSort);

            return CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>
                .Success(
                    new string[] { ActionStatusHelper.Success },
                    new PaginatedList<GetMessageResponseDto>(messages, total),
                    0
                );
        }

        public async Task<CommonResultResponseDto<string>> AddMessage(AddMessageRequestDto request)
        {
            await _adminRepository.AddMessage(request);

            return CommonResultResponseDto<string>.Success(
                new string[] { ActionStatusHelper.Success },
                "Message added successfully"
            );
        }

        public async Task<CommonResultResponseDto<string>> DeleteMessage(DeleteMessageRequestDto request)
        {
            await _adminRepository.DeleteMessage(request);

            return CommonResultResponseDto<string>.Success(
                new string[] { ActionStatusHelper.Success },
                "Message deleted successfully"
            );
        }
        public async Task<List<GetDaysResponseDto>> GetDay()
        {
            return await _adminRepository.GetDay();
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetDaysResponseDto>>> GetDays(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (days, total) = await _adminRepository.GetDays(filterModel, commonRequest, getSort);

            return CommonResultResponseDto<
            PaginatedList<GetDaysResponseDto>>.Success(

            new string[] { ActionStatusHelper.Success },

            new PaginatedList<GetDaysResponseDto>(days, total),
            0
            );

        }
        public async Task<CommonResultResponseDto<int>> UpdateDayStatus(UpdateDayStatusRequestDto request)
        {
            var result = await _adminRepository.UpdateDayStatus(request.Id, request.IsActive);

            return CommonResultResponseDto<int>.Success(
                new string[] { ActionStatusHelper.Success },
                result,
                0
            );
        }
        public async Task<IList<RouteTypeDayDto>> GetRouteTypeDays(int routeTypeId)
        {
            return await _adminRepository.GetRouteTypeDays(routeTypeId);
        }
        public async Task<CommonResultResponseDto<string>> UpdateRouteTypeExclusivePay(UpdateRouteTypeExclusivePayRequestDto request)
        {
            var result = await _adminRepository.UpdateRouteTypeExclusivePay(request.Id, request.ExclusivelyPay);

            if (result == 1)
            {
                return CommonResultResponseDto<string>.Success(
                    new string[] { "Updated Successfully" }, null, result);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(
                    new string[] { "RouteType not found" }, null);
            }
        }
        public async Task<CommonResultResponseDto<PaginatedList<PredefinedColorDto>>> GetPredefinedColors(  string filterModel, ServerRowsRequest commonRequest,  string getSort)
        {
            var (colors, total) = await _adminRepository.GetPredefinedColors(filterModel, commonRequest, getSort);

            return CommonResultResponseDto<PaginatedList<PredefinedColorDto>>.Success(
                new string[] { ActionStatusHelper.Success },
                new PaginatedList<PredefinedColorDto>(colors, total),
                0
            );
        }
        public async Task<CommonResultResponseDto<string>> AddUpdatePredefinedColor(AddUpdatePredefinedColorDto dto)
        {
            var id = await _adminRepository.AddUpdatePredefinedColor(dto);

            if (id > 0)
            {
                if (dto.Id > 0){
                    return CommonResultResponseDto<string>.Success(
                        new string[] { ActionStatusConstant.Updated },
                        null,
                        id
                    );
                }
                else {
                    return CommonResultResponseDto<string>.Success(
                        new string[] { ActionStatusConstant.Created },
                        null,
                        id
                    );
                }
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(
                    new string[] { "Something went wrong" },
                    null
                );
            }
        }
        public async Task<CommonResultResponseDto<string>> DeletePredefinedColor(int id)
        {
            var result = await _adminRepository.DeletePredefinedColor(id);

            if (result > 0){
                return CommonResultResponseDto<string>.Success(
                    new string[] { ActionStatusConstant.Deleted },
                    null,
                    result
                );
            }
            else{  return CommonResultResponseDto<string>.Failure(
                    new string[] { "Something went wrong" },
                    null
                );
            }
        }
        public async Task<CommonResultResponseDto<IList<PredefinedColorDto>>> GetPredefinedColorsDropdown()
        {
            var data = await _adminRepository.GetPredefinedColorsDropdown();

            return CommonResultResponseDto<IList<PredefinedColorDto>>.Success(
                new string[] { ActionStatusHelper.Success },
                data
            );
        }
        public async Task<CommonResultResponseDto<string>> UpdateBusCharge(UpdateBusChargeRequestDto updateBusChargeRequestDto)
        {
            var rowsAffected = await _adminRepository.UpdateBusCharge(updateBusChargeRequestDto);

            if (rowsAffected > 0)
            {
                return CommonResultResponseDto<string>.Success(
                    new string[] { ActionStatusConstant.Updated },
                    null,
                    rowsAffected
                );
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(
                    new string[] { "Something went wrong" },
                    null
                );
            }
        }
        public async Task<CommonResultResponseDto<GetBusChargeQueryResponseDto>> GetBusCharge()
        {
            var busCharge = await _adminRepository.GetBusCharge();

            return CommonResultResponseDto<GetBusChargeQueryResponseDto>.Success(
                new string[] { ActionStatusHelper.Success },
                busCharge
            );
        }
    }
}
