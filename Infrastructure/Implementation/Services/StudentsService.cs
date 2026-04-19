using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Helpers;
using Application.Common.Response;
using Application.Handler.Students.Queries.ExportStudentsList;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using DTO.Request.Routes;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using Helper;
using Helper.Constant;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.Xml;

namespace Infrastructure.Implementation.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IPdfBuilderService _iPdfBuilder;
        private readonly Utility _utility;
        public StudentsService(IStudentsRepository studentsRepository, Utility utility, IPdfBuilderService iPdfBuilder)
        {
            _studentsRepository = studentsRepository;
            _utility = utility;
            _iPdfBuilder = iPdfBuilder;
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateStudents(AddUpdateStudentsDto addUpdateStudentsDto)
        {
            var studentID = await _studentsRepository.AddUpdateStudents(addUpdateStudentsDto);
            if (addUpdateStudentsDto.StudentID != 0)
            {
                var familyId = await _studentsRepository.MergeFamilyByAutoFillFamilyId(addUpdateStudentsDto);
            }

            if (studentID > 0 && studentID != addUpdateStudentsDto.StudentID)
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

        public async Task<CommonResultResponseDto<string>> DeleteStudent(int id, bool IsFromRoute, int type)
        {
            var studentId = await _studentsRepository.DeleteStudent(id, IsFromRoute, type);
            if (studentId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, studentId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string ntId, string dob, string? district, string schoolStudentId, string schoolId, string grade, string? gender)
        {
            var (students, total) = await _studentsRepository.GetStudents(filterModel, commonRequest, routId, getSort, streetId, familyId, ntId, dob, district, schoolStudentId, schoolId, grade, gender);
            return CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsResponseDto>(students, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetStudentsForBulkRoute(string filterModel, ServerRowsRequest commonRequest, string getSort, string area, string school, string grade, int? gender, string building, string branch, string street, int userId, string uniqueId, int? routeId)
        {
            var (students, total) = await _studentsRepository.GetStudentsForBulkRoute(filterModel, commonRequest, getSort, area, school, grade, gender, building, branch, street, userId, uniqueId, routeId);
            return CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsResponseDto>(students, total), 0);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>> GetUnassignedStudents(string filterModel, ServerRowsRequest commonRequest, int routId, string getSort, int streetId, int familyId, string? routeTypeId, int? genderId)
        {
            var (students, total) = await _studentsRepository.GetUnassignedStudents(filterModel, commonRequest, routId, getSort, streetId, familyId, routeTypeId, genderId);
            return CommonResultResponseDto<PaginatedList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsResponseDto>(students, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsList()
        {
            var students = await _studentsRepository.GetStudentsList();
            return CommonResultResponseDto<IList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, students);
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsWithBusChangeList()
        {
            var students = await _studentsRepository.GetStudentsWithBusChangeList();
            return CommonResultResponseDto<IList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, students);
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentsByPhoneNumber(string phoneNumber)
        {
            var students = await _studentsRepository.GetStudentsByPhoneNumber(phoneNumber);
            return CommonResultResponseDto<IList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, students);
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetStudentRouteInfo(int id)
        {
            var students = await _studentsRepository.GetStudentRouteInfo(id);
            return CommonResultResponseDto<IList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, students);
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> GetRouteInfo(int id, string? phoneNumber , bool? isTeacher)
        {
            var students = await _studentsRepository.GetRouteInfo(id, phoneNumber, isTeacher );
            return CommonResultResponseDto<IList<GetStudentsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, students);
        }

        public async Task<CommonResultResponseDto<bool>> GetRecordsWithin30Minutes()
        {
            var students = await _studentsRepository.GetRecordsWithin30Minutes();
            return CommonResultResponseDto<bool>.Success(new string[] { ActionStatusHelper.Success }, students);
        }

        public async Task<CommonResultResponseDto<IList<GetFamilyListResponseDto>>> GetFamilyList()
        {
            var family = await _studentsRepository.GetFamilyList();
            return CommonResultResponseDto<IList<GetFamilyListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, family);
        }

        public async Task<CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>> GetFamilyDetails(GetFamilyDetailsRequestDto getBooksDetailRequestDto)
        {
            var family = await _studentsRepository.GetFamilyDetails(getBooksDetailRequestDto);
            return CommonResultResponseDto<IList<GetFamilyDetailsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, family);
        }
        public async Task<CommonResultResponseDto<IList<GetAllBranchResponseDto>>> GetAllBranch()
        {
            var branch = await _studentsRepository.GetAllBranch();
            return CommonResultResponseDto<IList<GetAllBranchResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, branch);
        }

        public async Task<CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>> GetBranchByBuildingId(GetBranchByBuildingIdRequestDto getBranchByBuildingIdRequestDto)
        {
            var branch = await _studentsRepository.GetBranchByBuildingId(getBranchByBuildingIdRequestDto);
            return CommonResultResponseDto<IList<GetBranchByBuildingIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, branch);
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateBulkStudentsRouteId(AddUpdateBulkStudentsRouteIdDto addUpdateBulkStudentsRouteIdDto)
        {
            string overrideXml = null;

            if (addUpdateBulkStudentsRouteIdDto.OverrideStudentList != null)
            {
                overrideXml = ConvertOverrideStudentToXML(addUpdateBulkStudentsRouteIdDto.OverrideStudentList);
            }
            var studentID = await _studentsRepository.AddUpdateBulkStudentsRouteId(addUpdateBulkStudentsRouteIdDto, overrideXml);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, studentID);

        }

        public async Task<CommonResultResponseDto<string>> AssignRouteToStudent(AssignRouteToStudentRequestDto assignRouteToStudentRequestDto)
        {
            var studentID = await _studentsRepository.AssignRouteToStudent(assignRouteToStudentRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, studentID);

        }

        public async Task<CommonResultResponseDto<string>> UpdateStudentsIndex(UpdateStudentsIndexRquestDto updateStudentsIndexRquestDto)
        {
            var studentID = await _studentsRepository.UpdateStudentsIndex(updateStudentsIndexRquestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, studentID);

        }

        public async Task<CommonResultResponseDto<IList<GetGradeListResponseDto>>> GetGradeList()
        {
            var grade = await _studentsRepository.GetGradeList();
            return CommonResultResponseDto<IList<GetGradeListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, grade);
        }
        public async Task<CommonResultResponseDto<IList<SearchLocationResult>>> SearchLocation(SearchLocationRequestDto searchLocationRequestDto)
        {
            var addresses = await _studentsRepository.SearchLocation(searchLocationRequestDto);
            List<SearchLocationResult> searchLocationResults = new List<SearchLocationResult>();
            foreach (var prediction in addresses)
            {
                SearchLocationResult result = new SearchLocationResult
                {
                    IsMap = false,
                    Address = (prediction?.StreetNumber ?? "") + " " + (prediction?.Address ?? ""),
                    StreetNumberMask = (prediction?.StreetNumber ?? ""),
                    AddressMask = (prediction?.Address ?? ""),
                    Cross = null,
                    GooglePlaceId = null,
                    Latitude = prediction.Latitude,
                    Longitude = prediction.Longitude
                };
                searchLocationResults.Add(result);
            }
            SearchAutocompleteResult searchAutocompleteResult = await _utility.SearchAddress(searchLocationRequestDto.Filter, searchLocationRequestDto.SearchCount);
            if (searchAutocompleteResult != null)
            {
                List<Prediction> sortedPredictions = _utility.SortPredictions(searchAutocompleteResult.predictions);

                foreach (Prediction prediction in sortedPredictions)
                {
                    // Extract street number and route from AddressComponents
                    var streetNumberComp = prediction.AddressComponents
                        ?.FirstOrDefault(comp => comp.types.Contains("street_number"));

                    var routeComp = prediction.AddressComponents
                        ?.FirstOrDefault(comp => comp.types.Contains("route"));

                    string streetNumber = streetNumberComp?.long_name ?? string.Empty;
                    string streetName = routeComp?.long_name ?? string.Empty;

                    SearchLocationResult result = new SearchLocationResult
                    {
                        IsMap = true,
                        Address = prediction.description,
                        Cross = prediction.structured_formatting?.main_text,
                        GooglePlaceId = prediction.place_id,
                        Latitude = prediction.Latitude,
                        Longitude = prediction.Longitude,
                        StreetNumberMask = streetNumber,
                        AddressMask = streetName,
                    };
                    searchLocationResults.Add(result);
                }
            }
            return CommonResultResponseDto<IList<SearchLocationResult>>.Success(new string[] { ActionStatusHelper.Success }, searchLocationResults);
        }

        public async Task<ExportFileResult> ExportStudentsList(ExportStudentsListQuery exportStudentsListQuery)
        {
            var newApplicationList = await _studentsRepository.ExportStudentsList(exportStudentsListQuery);


            DataTable table = new DataTable();

            //table.Columns.Add("StudentID", typeof(int));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("StudentLegalName", typeof(string));
            table.Columns.Add("GenderName", typeof(string)); // use GenderName instead of Gender ID
            table.Columns.Add("FatherFirstName", typeof(string));
            table.Columns.Add("MotherFirstName", typeof(string));
            table.Columns.Add("DOB", typeof(string));
            table.Columns.Add("Grade", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("StateName", typeof(string));
            table.Columns.Add("CityName", typeof(string));
            table.Columns.Add("Zipcode", typeof(string));
            table.Columns.Add("FatherCell", typeof(string));
            table.Columns.Add("MotherCell", typeof(string));
            table.Columns.Add("FamilyThirdCell", typeof(string));
            table.Columns.Add("HomeNumber", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("MWFamilyID", typeof(string));
            table.Columns.Add("MWStudentID", typeof(string));
            table.Columns.Add("SchoolFamilyID", typeof(string));
            table.Columns.Add("SchoolStudentID", typeof(string));
            table.Columns.Add("BuildingName", typeof(string));
            table.Columns.Add("AreaName", typeof(string));
            table.Columns.Add("DistrictName", typeof(string));
            table.Columns.Add("NtName", typeof(string));
            table.Columns.Add("RouteName", typeof(string));
            table.Columns.Add("BranchName", typeof(string));

            foreach (var item in newApplicationList)
            {
                table.Rows.Add(
                    //item.StudentID,
                    item.FirstName,
                    item.LastName,
                    item.StudentLegalName,
                    item.GenderName, // gender name from joined Gender table
                    item.FatherFirstName,
                    item.MotherFirstName,
                    item.DOB.ToString("yyyy-MM-dd") ?? string.Empty,
                    item.Grade,
                    item.Address ?? string.Empty,
                    item.Unit,
                    item.StateName,
                    item.CityName,
                    item.Zipcode,
                    item.FatherCell,
                    item.MotherCell,
                    item.FamilyThirdCell,
                    item.HomeNumber,
                    item.Email,
                    item.MWFamilyID,
                    item.MWStudentID,
                    item.SchoolFamilyID,
                    item.SchoolStudentID,
                    item.BuildingName,
                    item.AreaName,
                    item.DistrictName,
                    item.NtName,
                    item.RouteName,
                    item.BranchName
                );
            }

            int totalCount = newApplicationList.Count;
            string fileName = $"Students({totalCount}).xlsx";
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

        public async Task<CommonResultResponseDto<string>> UpdateBusStopIndex(UpdateBusStopIndexDto updateBusStopIndexDto)
        {
            var busStop = await _studentsRepository.UpdateBusStopIndex(JsonConvert.SerializeObject(updateBusStopIndexDto.BusStopList));
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busStop);
        }

        public async Task<CommonResultResponseDto<string>> UpdateStudentRouteNote(UpdateStudentRouteNoteDto updateStudentRouteNoteDto)
        {
            var busStop = await _studentsRepository.UpdateStudentRouteNote(updateStudentRouteNoteDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busStop);
        }

        public async Task<CommonResultResponseDto<string>> UpdateBulkNtStatus(UpdateBulkNtStatusRequestDto updateBulkNtStatusRequestDto)
        {
            var nt = await _studentsRepository.UpdateBulkNtStatus(updateBulkNtStatusRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, nt);
        }

        public async Task<CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>> UpdateFundedDetailsFromExcel(UpdateFundedDetailsFromExcelRequestDto updateFundedDetailsFromExcelRequestDto)
        {
            if (updateFundedDetailsFromExcelRequestDto.File == null || updateFundedDetailsFromExcelRequestDto.File.Length == 0)
            {
                return CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>.Failure(
                    new string[] { "Excel file is required." }, null);
            }

            if (updateFundedDetailsFromExcelRequestDto.Month < 1 || updateFundedDetailsFromExcelRequestDto.Month > 12)
            {
                return CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>.Failure(
                    new string[] { "Month must be between 1 and 12." }, null);
            }

            if (updateFundedDetailsFromExcelRequestDto.DistrictId <= 0)
            {
                return CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>.Failure(
                    new string[] { "DistrictId is required." }, null);
            }

            var firstDayOfSelectedMonth = new DateTime(DateTime.Now.Year, updateFundedDetailsFromExcelRequestDto.Month, 1);

            var fundedRows = ReadFundedRowsFromExcel(updateFundedDetailsFromExcelRequestDto.File);

            if (!fundedRows.Any())
            {
                return CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>.Failure(
                    new string[] { "No valid rows found in the uploaded file." }, null);
            }

            string fundedDetailsJson = JsonConvert.SerializeObject(fundedRows);

            var result = await _studentsRepository.UpdateFundedDetailsFromExcel(
                updateFundedDetailsFromExcelRequestDto.DistrictId,
                firstDayOfSelectedMonth,
                fundedDetailsJson
            );

            var resultList = result.ToList();
            var responseData = new UpdateFundedDetailsFromExcelResultDto
            {
                Rows = resultList,
                FileBytes = GenerateFundedDetailsExcel(resultList),
                FileName = $"FundedDetails({resultList.Count}).xlsx"
            };

            return CommonResultResponseDto<UpdateFundedDetailsFromExcelResultDto>.Success(
                new string[] { ActionStatusConstant.Updated },
                responseData,
                resultList.Count
            );
        }

        private byte[] GenerateFundedDetailsExcel(IList<UpdateFundedDetailsFromExcelResponseDto> data)
        {
            DataTable table = new DataTable();
            table.Columns.Add("SchoolDistrictId", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("UpdateStatus", typeof(string));
            table.Columns.Add("MWStudentID", typeof(string));
            table.Columns.Add("Isfunded", typeof(string));
            table.Columns.Add("FundStartDate", typeof(string));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("StudentLegalName", typeof(string));
            table.Columns.Add("FatherFirstName", typeof(string));
            table.Columns.Add("MotherFirstName", typeof(string));
            table.Columns.Add("DOB", typeof(string));
            table.Columns.Add("Grade", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("DistrictId", typeof(string));
            table.Columns.Add("StateName", typeof(string));
            table.Columns.Add("CityName", typeof(string));
            table.Columns.Add("FatherCell", typeof(string));
            table.Columns.Add("MotherCell", typeof(string));
            table.Columns.Add("FamilyThirdCell", typeof(string));
            table.Columns.Add("HomeNumber", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("MWFamilyID", typeof(string));
            table.Columns.Add("SchoolFamilyID", typeof(string));
            table.Columns.Add("SchoolStudentID", typeof(string));
            table.Columns.Add("BuildingName", typeof(string));
            table.Columns.Add("AreaName", typeof(string));
            table.Columns.Add("DistrictName", typeof(string));
            table.Columns.Add("Unit", typeof(string));

            foreach (var item in data)
            {
                table.Rows.Add(
                    item.SchoolDistrictId ?? string.Empty,
                    item.Status.HasValue ? item.Status.Value.ToString() : string.Empty,
                    item.UpdateStatus ?? string.Empty,
                    item.MWStudentID ?? string.Empty,
                    item.Isfunded.HasValue ? item.Isfunded.Value.ToString() : string.Empty,
                    item.FundStartDate.HasValue ? item.FundStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty,
                    item.FirstName ?? string.Empty,
                    item.LastName ?? string.Empty,
                    item.StudentLegalName ?? string.Empty,
                    item.FatherFirstName ?? string.Empty,
                    item.MotherFirstName ?? string.Empty,
                    item.DOB.HasValue ? item.DOB.Value.ToString("yyyy-MM-dd") : string.Empty,
                    item.Grade ?? string.Empty,
                    item.Address ?? string.Empty,
                    item.DistrictId.HasValue ? item.DistrictId.Value.ToString() : string.Empty,
                    item.StateName ?? string.Empty,
                    item.CityName ?? string.Empty,
                    item.FatherCell ?? string.Empty,
                    item.MotherCell ?? string.Empty,
                    item.FamilyThirdCell ?? string.Empty,
                    item.HomeNumber ?? string.Empty,
                    item.Email ?? string.Empty,
                    item.MWFamilyID ?? string.Empty,
                    item.SchoolFamilyID ?? string.Empty,
                    item.SchoolStudentID ?? string.Empty,
                    item.BuildingName ?? string.Empty,
                    item.AreaName ?? string.Empty,
                    item.DistrictName ?? string.Empty,
                    item.Unit ?? string.Empty
                );
            }

            return ExportToExcel(table);
        }
        public async Task<CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>> GetGradeBranchList()
        {
            var gradeBranch = await _studentsRepository.GetGradeBranchList();
            return CommonResultResponseDto<IList<GetGradeBranchListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, gradeBranch);
        }

        public async Task<CommonResultResponseDto<ImportStudentsResult>> ImportBulkStudents(ImportBulkStudentsRequestDto importBulkStudentsRequestDto)
        {
            var studentID = await _studentsRepository.ImportBulkStudents(importBulkStudentsRequestDto);
            return CommonResultResponseDto<ImportStudentsResult>.Success(new string[] { ActionStatusConstant.Updated }, studentID);
        }

        public async Task<CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>> AssignChangeAddressStudent(AssignChangeAddressStudentRequestDto assignChangeAddressStudentRequestDto)
        {
            var studentID = await _studentsRepository.AssignChangeAddressStudent(assignChangeAddressStudentRequestDto);
            return CommonResultResponseDto<IList<AssignChangeAddressStudentResponseDto>>.Success(new string[] { ActionStatusConstant.Updated }, studentID, 0);
        }
        public async Task<CommonResultResponseDto<IList<GetFatherCellsListResponseDto>>> GetFatherCellsByRouteId(string routeId)
        {
            var fatherCells = await _studentsRepository.GetFatherCellsByRouteId(routeId);
            return CommonResultResponseDto<IList<GetFatherCellsListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, fatherCells);
        }

        public async Task<Byte[]> DownloadStudentApplicationForm(int studentId)
        {
            var response = await _studentsRepository.GetStudentsById(studentId);
            return await _iPdfBuilder.GeneratePrintStudentPdf(response);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>> GetStudentsByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            var (students, total) = await _studentsRepository.GetStudentsByFamilyId(filterModel, commonRequest, getSort, familyId);
            return CommonResultResponseDto<PaginatedList<GetStudentsByFamilyIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsByFamilyIdResponseDto>(students, total), 0);
         }
        public async Task<CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>> GetFamilyChargesByFamilyId(string filterModel, ServerRowsRequest commonRequest, string getSort, string familyId)
        {
            var (students, total) = await _studentsRepository.GetFamilyChargesByFamilyId(filterModel, commonRequest, getSort, familyId);
            return CommonResultResponseDto<PaginatedList<GetFamilyChargesByFamilyIdResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetFamilyChargesByFamilyIdResponseDto>(students, total), 0);
        }
        public async Task<CommonResultResponseDto<string>> AddFamilyCharge(AddFamilyChargeDto dto)
        {
            var result = await _studentsRepository.AddFamilyCharge(dto);

            if (result > 0)
            {
                return CommonResultResponseDto<string>.Success(
                    new string[] { ActionStatusHelper.Success },
                    "Charge added successfully",
                    0
                );
            }

            return CommonResultResponseDto<string>.Failure(
                new string[] { "Failed to add charge" }
            );
        }
        public async Task<List<ChargeDropdownDto>> GetFamilyChargesForDropdown(int familyId)
        {
            return await _studentsRepository.GetFamilyChargesForDropdown(familyId);
        }
        public async Task<CommonResultResponseDto<IList<TeacherPhoneResponseDto>>> GetTeacherByPhoneNumber(string phoneNumber)
        {
            var teachers = await _studentsRepository.GetTeacherByPhoneNumber(phoneNumber);
            return CommonResultResponseDto<IList<TeacherPhoneResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, teachers);
        }

        private static List<FundedExcelRow> ReadFundedRowsFromExcel(IFormFile file)
        {
            using MemoryStream memoryStream = new();
            file.CopyTo(memoryStream);
            memoryStream.Position = 0;

            using SpreadsheetDocument document = SpreadsheetDocument.Open(memoryStream, false);
            WorkbookPart workbookPart = document.WorkbookPart;
            var sheet = workbookPart.Workbook.Sheets.Elements<Sheet>().FirstOrDefault();
            if (sheet == null)
            {
                return new List<FundedExcelRow>();
            }

            WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
            var rows = worksheetPart.Worksheet.Descendants<Row>().ToList();
            if (!rows.Any())
            {
                return new List<FundedExcelRow>();
            }

            var headerCells = rows.First().Elements<Cell>().ToList();
            int schoolDistrictIdColumnIndex = -1;
            int statusColumnIndex = -1;
            for (int i = 0; i < headerCells.Count; i++)
            {
                var headerValue = GetCellValue(document, headerCells[i]).Trim();
                var normalizedHeader = NormalizeHeader(headerValue);
                if (normalizedHeader == "schooldistrictid" || normalizedHeader == "schooldistrict")
                {
                    schoolDistrictIdColumnIndex = i;
                }

                if (normalizedHeader == "status")
                {
                    statusColumnIndex = i;
                }
            }

            if (schoolDistrictIdColumnIndex < 0 || statusColumnIndex < 0)
            {
                return new List<FundedExcelRow>();
            }

            var result = new List<FundedExcelRow>();
            for (int i = 1; i < rows.Count; i++)
            {
                var row = rows[i];
                var cells = row.Elements<Cell>().ToList();
                if (!cells.Any())
                {
                    continue;
                }

                string schoolDistrictId = GetCellValue(document, cells.ElementAtOrDefault(schoolDistrictIdColumnIndex)).Trim();
                string statusText = GetCellValue(document, cells.ElementAtOrDefault(statusColumnIndex)).Trim();

                if (string.IsNullOrWhiteSpace(schoolDistrictId))
                {
                    continue;
                }

                if (!TryParseStatus(statusText, out bool status))
                {
                    continue;
                }

                result.Add(new FundedExcelRow
                {
                    SchoolDistrictId = schoolDistrictId,
                    Status = status
                });
            }

            return result
                .GroupBy(x => x.SchoolDistrictId, StringComparer.OrdinalIgnoreCase)
                .Select(x => x.Last())
                .ToList();
        }

        private static string NormalizeHeader(string value)
        {
            return new string(value.Where(char.IsLetterOrDigit).ToArray()).ToLowerInvariant();
        }

        private static bool TryParseStatus(string value, out bool status)
        {
            if (bool.TryParse(value, out status))
            {
                return true;
            }

            switch (value.Trim().ToLowerInvariant())
            {
                case "1":
                case "yes":
                case "y":
                    status = true;
                    return true;
                case "0":
                case "no":
                case "n":
                    status = false;
                    return true;
                default:
                    status = false;
                    return false;
            }
        }

        private static string GetCellValue(SpreadsheetDocument document, Cell? cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }

            var cellValue = cell.CellValue?.InnerText ?? string.Empty;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                if (!int.TryParse(cellValue, out var sharedStringIndex))
                {
                    return string.Empty;
                }

                return document.WorkbookPart.SharedStringTablePart?.SharedStringTable?
                           .ElementAt(sharedStringIndex).InnerText ?? string.Empty;
            }

            return cellValue;
        }

        private sealed class FundedExcelRow
        {
            public string SchoolDistrictId { get; set; } = string.Empty;
            public bool Status { get; set; }
        }

        private static string ConvertOverrideStudentToXML(List<OverrideStudentDto> overrideStudents)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in overrideStudents)
            {
                XmlNode node = xmlDocument.CreateElement("OverrideStudent");

                XmlAttribute attribute = xmlDocument.CreateAttribute("RouteID");
                attribute.Value = item.RouteID != null ? item.RouteID.ToString() : "";
                node.Attributes.Append(attribute);

                attribute = xmlDocument.CreateAttribute("StudentId");
                attribute.Value = item.StudentId != null ? item.StudentId.ToString() : "";
                node.Attributes.Append(attribute);

                rootNode.AppendChild(node);
            }

            return xmlDocument.OuterXml;
        }

       
    }

}
