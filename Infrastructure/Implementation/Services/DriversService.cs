using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using Application.ExternalAPI;
using Application.Handler.Students.Queries.ExportStudentsList;
using Application.Settings;
using Common.Helper;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Entities;
using DTO.Request.Drivers;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Bus;
using DTO.Response.Driver;
using DTO.Response.SystemValues;
using Helper;
using Helper.Constant;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Implementation.Services
{
    public class DriversService : IDriversService
    {
        private readonly IDriversRepository _driversRepository;
        private readonly Mailsetting _mailSetting;
        private readonly ISendGridEmail _sendGridEmail;
        private readonly IUserRepository _userRepository;

        public DriversService(IDriversRepository driversRepository, IOptions<Mailsetting> mailSetting, ISendGridEmail sendGridEmail, IUserRepository userRepository)
        {
            _driversRepository = driversRepository;
            _mailSetting = mailSetting.Value;
            _sendGridEmail = sendGridEmail;
            _userRepository = userRepository;
        }

        public async Task<CommonResultResponseDto<CheckDriverHasBusResponseDto>> AddUpdateDriversDetails(AddUpdateDriversDetailsRequestDto addUpdateDriversDetailsRequestDto)
        {
            var busDetailsId = await _driversRepository.AddUpdateDriversDetails(addUpdateDriversDetailsRequestDto);
            if (busDetailsId > 0 && busDetailsId != addUpdateDriversDetailsRequestDto.DriverID)
            {
                var response = CommonResultResponseDto<CheckDriverHasBusResponseDto>.Success(new string[] { ActionStatusConstant.Created }, null, busDetailsId);
                _ = Task.Run(async () =>
                {
                    string resetLink = $"{ConstantVariables.SETPASSWORDURL}?driverId={busDetailsId}";
                    await SendEmailsAsyncForDriver(addUpdateDriversDetailsRequestDto.Email, resetLink);

                });
                return response;
            }
            else if (busDetailsId < 0)
            {
                return CommonResultResponseDto<CheckDriverHasBusResponseDto>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<CheckDriverHasBusResponseDto>.Success(new string[] { ActionStatusConstant.Updated }, null, busDetailsId);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeductReservedAccountBalance(DeductReservedAccountBalanceRequestDto deductReservedAccountBalanceRequestDto)
        {
            var balanceId = await _driversRepository.DeductReservedAccountBalance(deductReservedAccountBalanceRequestDto);
            if (balanceId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, balanceId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteDrivers(int id, bool IsFromRoute, int? RouteId)
        {
            var driversId = await _driversRepository.DeleteDrivers(id, IsFromRoute, RouteId);
            if (driversId == "1")
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, 0);
            }
            else if (!string.IsNullOrEmpty(driversId))
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "This driver cannot be deleted because it is assigned to one or more routes " + driversId }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>> GetDrivers(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            var (areas, total) = await _driversRepository.GetDrivers(filterModel, commonRequest, getSort, routeId);
            return CommonResultResponseDto<PaginatedList<GetDriversResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetDriversResponseDto>(areas, total), 0);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>> GetDriversBalanceHistory(string filterModel, ServerRowsRequest commonRequest, string getSort, int driverId)
        {
            var (reservedBalance, total) = await _driversRepository.GetDriversBalanceHistory(filterModel, commonRequest, getSort, driverId);
            return CommonResultResponseDto<PaginatedList<GetDriversBalanceHistoryResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetDriversBalanceHistoryResponseDto>(reservedBalance, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetDriversListResponseDto>>> GetDriversList()
        {
            var areaList = await _driversRepository.GetDriversList();
            return CommonResultResponseDto<IList<GetDriversListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, areaList);
        }

        public async Task<CommonResultResponseDto<IList<GetDriverTypeResponseDto>>> GetDriverTypeList()
        {
            var driverType = await _driversRepository.GetDriverTypeList();
            return CommonResultResponseDto<IList<GetDriverTypeResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, driverType);
        }

        public async Task<CommonResultResponseDto<string>> ResetDriverPassword(ResetDriverPasswordDto resetDriverPasswordDto)
        {
            var driver = await _driversRepository.GetDriverByEmail(resetDriverPasswordDto.Email, resetDriverPasswordDto.DriverId);
            var user = await _userRepository.GetUserByEmail(resetDriverPasswordDto.Email);
            if (driver == null && user == null)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Enter Valid Email" }, null);
            }

            if (string.IsNullOrWhiteSpace(resetDriverPasswordDto.Password))
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Password cannot be empty" }, null);
            }

            EncryptionHelper.CreatePasswordHash(resetDriverPasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            if (driver != null)
            {
                driver.PasswordHash = passwordHash;
                driver.PasswordSalt = passwordSalt;
                await _driversRepository.ResetDriverPassword(driver, resetDriverPasswordDto);
            }
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _driversRepository.ResetUserPassword(user, resetDriverPasswordDto);
            }
            return CommonResultResponseDto<string>.Success(new[] { ActionStatusHelper.Updated }, null);
        }

        public async Task<CommonResultResponseDto<string>> SendOtpOnEmail(SendOtpOnEmailRequestDto sendOtpOnEmailRequestDto)
        {
            var driver = await _driversRepository.GetDriverByEmail(sendOtpOnEmailRequestDto.Email, 0);
            var user = await _userRepository.GetUserByEmail(sendOtpOnEmailRequestDto.Email);

            if (driver == null && user == null)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Enter Valid Email" }, null);
            }

            int OTPCode = new Random().Next(100000, 999999);
            if (driver != null && driver.DriverID != 0)
            {
                Drivers dr = driver;
                dr.OtpCode = OTPCode;
                dr.UpdatedDate = DateTime.Now;
                await _driversRepository.SendOtpOnEmail(dr);
            }

            if (user != null && user.UserId != 0)
            {
                Domain.Entities.Users us = user;
                us.OtpCode = OTPCode;
                us.UpdatedDate = DateTime.Now;
                await _userRepository.SendOtpOnEmail(us);
            }

            string body = "Your reset password otp code is: " + OTPCode.ToString().Substring(0, 3) + "-" + OTPCode.ToString().Substring(3, 3);
            if (driver != null || user != null)
            {
                await SendEmailsAsync(sendOtpOnEmailRequestDto.Email, body);
            }
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusHelper.Success }, null);

        }

        public async Task<bool> SendEmailsAsync(string email, string body)
        {
            try
            {
                return await _sendGridEmail.SendMail(
                        to: email,
                        subject: "School Transportation System",
                        body: body,
                        fromName: _mailSetting.DisplayName
                );


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> SendEmailsAsyncForDriver(string email, string resetLink)
        {
            try
            {
                string emailBodyPath = Path.Combine("wwwroot", "EmailTemplates", "GeneratePassword.html");
                string body = await File.ReadAllTextAsync(emailBodyPath);

                return await _sendGridEmail.SendMail(
                    to: email,
                    subject: "Generate Your Password",
                    body: body.Replace("{{ResetLink}}", resetLink),
                    fromName: _mailSetting.DisplayName
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }


        public async Task<CommonResultResponseDto<Drivers>> CheckOTP(CheckOTPRequestDto checkOTPRequestDto)
        {
            var checkOTP = await _driversRepository.CheckOTP(checkOTPRequestDto);
            var checkUserOTP = await _userRepository.CheckUserOTP(checkOTPRequestDto);

            if (checkOTP != null || checkUserOTP != null)
            {
                return CommonResultResponseDto<Drivers>.Success(new string[] { ActionStatusHelper.Success }, checkOTP);
            }
            return CommonResultResponseDto<Drivers>.Failure(new string[] { "Could not find a valid OTP" }, checkOTP);
        }

        public async Task<CommonResultResponseDto<string>> AssignRouteToDriver(AssignRouteToDriverRequestDto assignRouteToDriverRequestDto)
        {
            var assignRoute = await _driversRepository.AssignRouteToDriver(assignRouteToDriverRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, assignRoute);

        }

        public async Task<CommonResultResponseDto<string>> DeleteBusAndDriverRoute(DeleteBusAndDriverRouteRequestDto deleteBusAndDriverRouteRequestDto)
        {
            var driversId = await _driversRepository.DeleteBusAndDriverRoute(deleteBusAndDriverRouteRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, driversId);

        }

        public Task<CommonResultResponseDto<string>> SendLinkToResetDriverPassword(SendLinkToResetDriverPasswordDto dto)
        {
            _ = Task.Run(async () =>
            {
                try
                {
                    string resetLink = $"{ConstantVariables.SETPASSWORDURL}?driverId={dto.DriverId}";
                    await SendEmailsAsyncForDriver(dto.Email, resetLink);
                }
                catch (Exception ex)
                {
                    // Log the error so it's not swallowed
                    Console.WriteLine(ex);
                }
            });
            return Task.FromResult(CommonResultResponseDto<string>.Success(new[] { "Reset link sent successfully." }, null));
        }

        public async Task<ExportFileResult> ExportDriversList()
        {
            var driversList = await _driversRepository.ExportDriversList();

            DataTable table = new DataTable();

            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("PhoneNumber", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("BusName", typeof(string));
            table.Columns.Add("TotalRoute", typeof(int));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("UserName", typeof(string));
            table.Columns.Add("DriverType", typeof(string));

            foreach (var item in driversList)
            {
                table.Rows.Add(
                    item.FirstName,
                    item.LastName,
                    item.PhoneNumber,
                    item.Status,
                    item.BusName ?? string.Empty,
                    item.TotalRoute,
                    item.Address ?? string.Empty,
                    item.Email,
                    item.UserName,
                    item.DriverType
                );
            }

            int totalCount = driversList.Count;

            string fileName = $"Drivers({totalCount}).xlsx";

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
                        Name = "Drivers"
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

    }
}
