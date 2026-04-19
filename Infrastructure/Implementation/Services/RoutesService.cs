using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using Application.Common.Dtos;
using Application.Common.Response;
using Application.Handler.Routes.Queries.GetRoutes;
using Application.Handler.Routes.Queries.GetRoutesLists;
using DTO.Request.Routes;
using DTO.Response;
using DTO.Response.Admin;
using DTO.Response.Routes;
using DTO.Response.Streets;
using Helper;
using Helper.Constant;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System.Xml;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace Infrastructure.Implementation.Services
{
    public class RoutesService : IRoutesService
    {

        private readonly IRoutesRepository _routesRepository;
        public RoutesService(IRoutesRepository routesRepository)
        {
            _routesRepository = routesRepository;
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateRoutesDetails(AddUpdateRoutesDetailsRequestDto addUpdateRoutesDetailsRequestDto)
        {
            var busDetailsId = await _routesRepository.AddUpdateRoutesDetails(addUpdateRoutesDetailsRequestDto);
            if (busDetailsId > 0 && busDetailsId != addUpdateRoutesDetailsRequestDto.RouteID)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busDetailsId);
            }
            else if (busDetailsId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, busDetailsId);
            }
        }

        public async Task<CommonResultResponseDto<string>> AddUpdateBulkRoutes(UpdateBulkRoutesRequestDto updateBulkRoutesRequestDto)
        {
            var routeGroupID = await _routesRepository.AddUpdateBulkRoutes(updateBulkRoutesRequestDto);
            if (routeGroupID == 1)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, 1);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> UpdateStop(UpdateStopDto updateStopDto)
        {
            var routeGroupID = await _routesRepository.UpdateStop(updateStopDto);
            if (routeGroupID == 1)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, 1);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> DeleteRoutes(int id, int? deleteAll, DateTime? routeDate)
        {
            var routesId = await _routesRepository.DeleteRoutes(id, deleteAll, routeDate);
            if (routesId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, routesId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> UndoRoutes(int id, DateTime? routeDate)
        {
            var routesId = await _routesRepository.UndoRoutes(id, routeDate);
            if (routesId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, routesId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }
        public async Task<CommonResultResponseDto<string>> DeleteStudentFromRoute(int studentId, int routeId)
        {
            var routesId = await _routesRepository.DeleteStudentFromRoute(studentId, routeId);
            if (routesId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Deleted }, null, routesId);
            }
            else
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutes(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto)
        {

            var (areas, total) = await _routesRepository.GetRoutes(filterModel, commonRequest, getSort, getRoutesRequestDto);
            return CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetRoutesResponseDto>(areas, total), 0);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutesByTabs(string filterModel, ServerRowsRequest commonRequest, string getSort, GetRoutesRequestDto getRoutesRequestDto)
        {

            var (areas, total) = await _routesRepository.GetRoutesByTabs(filterModel, commonRequest, getSort, getRoutesRequestDto);
            return CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetRoutesResponseDto>(areas, total), 0);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>> GetRoutesDetailsByTypeId(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeTypeId)
        {

            var (areas, total) = await _routesRepository.GetRoutesDetailsByTypeId(filterModel, commonRequest, getSort, routeTypeId);
            return CommonResultResponseDto<PaginatedList<GetRoutesResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetRoutesResponseDto>(areas, total), 0);
        }
        public async Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> GetRoutesList()
        {
            var building = await _routesRepository.GetRoutesList();
            return CommonResultResponseDto<IList<GetRoutesListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, building);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>> GetHistoryByTab(string filterModel, ServerRowsRequest commonRequest, string getSort, string tab, int id)
        {
            var (history, total) = await _routesRepository.GetHistoryByTab(filterModel, commonRequest, getSort, tab, id);
            return CommonResultResponseDto<PaginatedList<GetHistoryByTabResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetHistoryByTabResponseDto>(history, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>> GetSchoolBuildingBranchDetails()
        {
            var schoolBuilding = await _routesRepository.GetSchoolBuildingBranchDetails();
            return CommonResultResponseDto<IList<GetSchoolBuildingBranchDetailsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, schoolBuilding);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>> GetSchoolBuildingBranchList(string filterModel, ServerRowsRequest commonRequest, string getSort)
        {
            var (branch, total) = await _routesRepository.GetSchoolBuildingBranchList(filterModel, commonRequest, getSort);
            return CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSchoolBuildingBranchListResponseDto>(branch, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<string>>> AddBulkRoutesDetails(AddBulkRoutesDetailsRequestDto addBulkRoutesDetailsRequest)
        {
            var studentBusXml = ConvertStudentBusToXML(addBulkRoutesDetailsRequest.Students);

            string overrideXml = null;

            if (addBulkRoutesDetailsRequest.OverrideStudentList != null)
            {
                overrideXml = ConvertOverrideStudentToXML(addBulkRoutesDetailsRequest.OverrideStudentList);
            }

            var insertedRoutes = await _routesRepository.AddBulkRoutesDetails(addBulkRoutesDetailsRequest, studentBusXml, overrideXml);

            if (insertedRoutes != null && insertedRoutes.Count > 0)
            {
                var routeIds = insertedRoutes.Select(x => x.RouteID).ToList();
                return CommonResultResponseDto<IList<string>>.Success(new[] { ActionStatusConstant.Created }, routeIds);
            }
            else
            {
                return CommonResultResponseDto<IList<string>>.Failure(new[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<IList<GetRoutesListsResponseDto>>> GetRoutesLists(GetRoutesListsRequestDto getRoutesListsRequestDto)
        {
            var route = await _routesRepository.GetRoutesLists(getRoutesListsRequestDto);
            return CommonResultResponseDto<IList<GetRoutesListsResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, route);
        }


        private static string ConvertStudentBusToXML(List<StudentBusDto> studentBusData)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in studentBusData)
            {
                Guid commonId = Guid.NewGuid();
                XmlNode medicationsNode = xmlDocument.CreateElement("StudentBusData");
                XmlAttribute attribute = xmlDocument.CreateAttribute("StudentId");
                attribute.Value = item.StudentId != null ? item.StudentId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("BusId");
                attribute.Value = item.BusID != null ? item.BusID.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("RowNumber");
                attribute.Value = item.RowNumber != null ? item.RowNumber.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("BusStopLatLong");
                attribute.Value = item.BusStopLatLong != null ? item.BusStopLatLong.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("StreetNumber");
                attribute.Value = item.StreetNumber != null ? item.StreetNumber.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Address");
                attribute.Value = item.Address != null ? item.Address.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

            }
            return xmlDocument.OuterXml;
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

        public async Task<CommonResultResponseDto<string>> UpdateSchoolBuildingBranchMapping(UpdateSchoolBuildingBranchMappingDto updateSchoolBuildingBranchMappingDto)
        {
            string schoolBuildingBranchJson = JsonConvert.SerializeObject(updateSchoolBuildingBranchMappingDto.SchoolBuildingBranchList);
            string routeIdsJson = JsonConvert.SerializeObject(updateSchoolBuildingBranchMappingDto.RouteIdList);

            var result = await _routesRepository.UpdateSchoolBuildingBranchMapping(schoolBuildingBranchJson, routeIdsJson);

            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, result);
        }

        public async Task<CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>> GetSchoolBuildingBranchByRouteId(string filterModel, ServerRowsRequest commonRequest, string getSort, int routeId)
        {
            var (school, total) = await _routesRepository.GetSchoolBuildingBranchByRouteId(filterModel, commonRequest, getSort, routeId);
            return CommonResultResponseDto<PaginatedList<GetSchoolBuildingBranchResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetSchoolBuildingBranchResponseDto>(school, total), 0);
        }

        public async Task<CommonResultResponseDto<IList<GetAddressResponseDto>>> GetAddress()
        {
            var address = await _routesRepository.GetAddress();
            return CommonResultResponseDto<IList<GetAddressResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, address);
        }

        public async Task<CommonResultResponseDto<byte[]>> DownloadPrintOrder(DownloadPrintForRoutesRequestDto downloadPrintForRoutesRequestDto)
        {
            var address = await _routesRepository.DownloadPrintOrder(downloadPrintForRoutesRequestDto);
            var studentDetails = await _routesRepository.GetStudentByRouteId(downloadPrintForRoutesRequestDto);
            var news = GenerateRoutePdf(address, studentDetails);
            return CommonResultResponseDto<byte[]>.Success(new string[] { ActionStatusHelper.Success }, news);
        }

        public async Task<CommonResultResponseDto<IList<GetAreaListResponseDto>>> GetAreasBySchoolAndGrade(GetAreasBySchoolAndGradeRequestDto getAreasBySchoolAndGradeRequestDto)
        {
            var address = await _routesRepository.GetAreasBySchoolAndGrade(getAreasBySchoolAndGradeRequestDto);
            return CommonResultResponseDto<IList<GetAreaListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, address);
        }
        public async Task<CommonResultResponseDto<IList<GetRoutesListResponseDto>>> GetFilteredRoutesForBusChange(GetFilteredRoutesForBusChangeRequestDto getFilteredRoutesForBusChangeRequestDto)
        {
            var address = await _routesRepository.GetFilteredRoutesForBusChange(getFilteredRoutesForBusChangeRequestDto);
            return CommonResultResponseDto<IList<GetRoutesListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, address);
        }


        public async Task<CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>> GetRoutesDetailList()
        {
            var detail = await _routesRepository.GetRoutesDetailList();
            return CommonResultResponseDto<IList<GetRoutesDetailListResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, detail);
        }

        public async Task<CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>> CheckRouteTypeStudent(CheckRouteTypeStudentRequestDto checkRouteTypeStudentRequestDto)


        {
            var studentBusXml = ConvertStudentBusToXML(checkRouteTypeStudentRequestDto.Students);
            var checkRouteTypeAssignToStudent = await _routesRepository.CheckRouteTypeStudent(checkRouteTypeStudentRequestDto, studentBusXml);

            return CommonResultResponseDto<IList<RouteTypeStudentResponseDto>>.Success(new[] { ActionStatusConstant.Created }, checkRouteTypeAssignToStudent);

        }


        public async Task<CommonResultResponseDto<string>> UpdateBulkRoutesDetails(UpdateBulkRoutesDetailsRequestDto updateBulkRoutesDetailsRequestDto)
        {
            var bulkRouteId = await _routesRepository.UpdateBulkRoutesDetails(updateBulkRoutesDetailsRequestDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, bulkRouteId);
        }
        public async Task<CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>> GetStudentsWithChangedAddress(string filterModel, ServerRowsRequest commonRequest, string getSort, string? routeTypeIds, int? genderId)
        {
            var (address, total) = await _routesRepository.GetStudentsWithChangedAddress(filterModel, commonRequest, getSort, routeTypeIds, genderId);
            return CommonResultResponseDto<PaginatedList<GetStudentsWithChangedAddressResponseDto>>.Success(new string[] { ActionStatusHelper.Success }, new PaginatedList<GetStudentsWithChangedAddressResponseDto>(address, total), 0);

        }

        public async Task<CommonResultResponseDto<string>> UpdateTempBusDriverDetails(UpdateTempBusDriverDetailsDto updateTempBusDriverDetailsDto)
        {
            var routeGroupID = await _routesRepository.UpdateTempBusDriverDetails(updateTempBusDriverDetailsDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, routeGroupID);
        }

        public async Task<CommonResultResponseDto<string>> UpdateTodayDriver(UpdateTodayDriverDto updateTodayDriverDto)
        {
            var driver = await _routesRepository.UpdateTodayDriver(updateTodayDriverDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, driver);
        }
        public async Task<CommonResultResponseDto<string>> UpdateRouteGroup(UpdateRouteGroupDto updateRouteGroupDto)
        {
            var routeGroup = await _routesRepository.UpdateRouteGroup(updateRouteGroupDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, routeGroup);
        }

        public async Task<CommonResultResponseDto<string>> UpdateDelayRoute(UpdateDelayRouteDto updateDelayRouteDto)
        {
            var delayRoute = await _routesRepository.UpdateDelayRoute(updateDelayRouteDto);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, delayRoute);
        }
        public byte[] GenerateRoutePdf(IList<DownloadPrintOrderResponseDto> routeData, IList<GetRoutesResponseDto> studentDetails)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 29f, 29f, 29f, 29f);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Title
                Font titleFont = FontFactory.GetFont("Inter", 18, Font.BOLD, new BaseColor(0, 0, 0));
                var isDraft = studentDetails.FirstOrDefault()?.IsDraftRoute == true;
                string suffix = isDraft ? " (Draft)" : "";

                Paragraph title = new Paragraph("Routes Details" + suffix, titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 25f
                };
                document.Add(title);

                PdfPTable boxTable = new PdfPTable(1);
                boxTable.WidthPercentage = 100;

                PdfPCell boxCell = new PdfPCell();
                boxCell.Border = Rectangle.NO_BORDER;
                boxCell.Padding = 16f;
                boxCell.BackgroundColor = new BaseColor(247, 247, 248);
                boxCell.CellEvent = new RoundedBorderCellEvent(12);
                boxCell.UseVariableBorders = false;

                PdfPTable infoTable = new PdfPTable(4);
                infoTable.WidthPercentage = 100;
                infoTable.SpacingAfter = 20f;
                infoTable.SetWidths(new float[] { 1f, 1f, 1f, 1f });
                infoTable.DefaultCell.Border = Rectangle.NO_BORDER;

                infoTable.AddCell(CreateLabelTopCell("School Name"));
                infoTable.AddCell(CreateLabelTopCell("Grade"));
                infoTable.AddCell(CreateLabelTopCell("Time"));
                infoTable.AddCell(CreateLabelTopCell("Bus Name"));

                infoTable.AddCell(CreateValueBottomCell(studentDetails.FirstOrDefault()?.SchoolName ?? ""));
                infoTable.AddCell(CreateValueBottomCell(studentDetails.FirstOrDefault()?.Grade ?? ""));
                var timeSpan = studentDetails.FirstOrDefault()?.Time;
                string timeString = timeSpan.HasValue ? DateTime.Today.Add(timeSpan.Value).ToString("hh:mm tt") : "";
                infoTable.AddCell(CreateValueBottomCell(timeString));
                infoTable.AddCell(CreateValueBottomCell(studentDetails.FirstOrDefault()?.BusName ?? ""));

                // Row 2
                infoTable.AddCell(CreateLabelTopCell("Bus Driver Name"));
                infoTable.AddCell(CreateLabelTopCell("Driver Phone Number"));
                infoTable.AddCell(CreateLabelTopCell("Total Student"));
                infoTable.AddCell(CreateLabelTopCell("Dropoff Buildings"));

                infoTable.AddCell(CreateValueBottomCell(studentDetails.FirstOrDefault()?.TodayDriverFullName ?? ""));
                infoTable.AddCell(CreateValueBottomCell(studentDetails.FirstOrDefault()?.PhoneNumber ?? ""));
                infoTable.AddCell(CreateValueBottomCell((studentDetails.FirstOrDefault()?.TotalStudents ?? 0).ToString()));
                var dropoffBuildings = string.Join(", ",
                    routeData.Where(r => !string.IsNullOrEmpty(r.BuildingAddress))
                             .Select(r => r.BuildingAddress.Trim())
                             .Distinct()
                             .OrderBy(x => x));
                infoTable.AddCell(CreateValueBottomCell(dropoffBuildings));

                boxCell.AddElement(infoTable);

                Paragraph driverNoteLabel = new Paragraph("Driver Notes:",
                    FontFactory.GetFont("Inter", 10, Font.NORMAL, new BaseColor(147, 148, 157)))
                {
                    SpacingAfter = 6f
                };
                boxCell.AddElement(driverNoteLabel);

                string driverNote = studentDetails.FirstOrDefault()?.DriverNotes;
                if (string.IsNullOrWhiteSpace(driverNote))
                    driverNote = "There are no notes for this route.";

                PdfPCell noteValueCell = new PdfPCell(new Phrase(driverNote,
                    FontFactory.GetFont("Inter", 10, Font.NORMAL, new BaseColor(66, 66, 74))))
                {
                    BorderColor = new BaseColor(238, 238, 240),
                    BorderWidth = 1f,
                    PaddingLeft = 12f,
                    PaddingRight = 12f,
                    PaddingTop = 9f,
                    PaddingBottom = 9f,
                    BackgroundColor = BaseColor.WHITE,
                    MinimumHeight = 90f,
                    VerticalAlignment = Element.ALIGN_TOP
                };
                noteValueCell.UseVariableBorders = false;
                noteValueCell.CellEvent = new RoundedBorderCellEvent(8);


                PdfPTable noteTable = new PdfPTable(1) { WidthPercentage = 100 };
                noteTable.DefaultCell.Border = Rectangle.NO_BORDER;
                noteTable.AddCell(noteValueCell);
                boxCell.AddElement(noteTable);

                boxTable.AddCell(boxCell);
                document.Add(boxTable);

                Paragraph pickUpTitle = new Paragraph("Pick Up",
                    FontFactory.GetFont("Inter", 14, Font.NORMAL, new BaseColor(0, 0, 0)))
                { SpacingAfter = 15f };
                document.Add(pickUpTitle);

                if (routeData != null && routeData.Any())
                {
                    PdfPTable pickUpTable = new PdfPTable(7);
                    pickUpTable.WidthPercentage = 100;
                    pickUpTable.SetWidths(new float[] { 0.8f, 1.5f, 3f, 2.5f, 2.5f, 2f, 2f });

                    pickUpTable.DefaultCell.Border = Rectangle.BOX;
                    pickUpTable.DefaultCell.BorderColor = new BaseColor(238, 238, 240);
                    pickUpTable.DefaultCell.BorderWidth = 1f;

                    pickUpTable.AddCell(CreatePickupHeaderCell("Stop"));
                    pickUpTable.AddCell(CreatePickupHeaderCell("Street No."));
                    pickUpTable.AddCell(CreatePickupHeaderCell("Street"));
                    pickUpTable.AddCell(CreatePickupHeaderCell("First Name"));
                    pickUpTable.AddCell(CreatePickupHeaderCell("Last Name"));
                    pickUpTable.AddCell(CreatePickupHeaderCell("Dropoff Buildings"));
                    pickUpTable.AddCell(CreatePickupHeaderCell("Note"));

                    int stopCounter = 1;
                    foreach (var pickUp in routeData)
                    {
                        pickUpTable.AddCell(CreatePickupBodyCell(stopCounter.ToString(), true, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.StreetNumber.ToString() ?? "", false, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.StreetName ?? "", false, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.FirstName ?? "", false, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.LastName ?? "", false, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.BuildingAddress ?? "", false, false));
                        pickUpTable.AddCell(CreatePickupBodyCell(pickUp.Note ?? "", false, true)); // Last cell gets right border
                        stopCounter++;
                    }

                    document.Add(pickUpTable);
                }

                document.Close();
                return memoryStream.ToArray();
            }
        }

        private static PdfPCell CreateLabelTopCell(string text)
        {
            return new PdfPCell(new Phrase(text,
                FontFactory.GetFont("Inter", 10, new BaseColor(147, 148, 157))))
            {
                BorderWidth = 0,
                PaddingBottom = 0f
            };
        }

        private static PdfPCell CreateValueBottomCell(string text)
        {
            var font = FontFactory.GetFont("Inter", 10f, Font.NORMAL, new BaseColor(21, 21, 23));

            // Leading = font.Size * 1.6 (1.6x line-height)
            var para = new Paragraph(font.Size * 1.4f, text, font);
            para.Alignment = Element.ALIGN_LEFT;

            var cell = new PdfPCell();
            cell.BorderWidth = 0;
            cell.PaddingBottom = 12f;
            cell.NoWrap = false;        // ensure wrapping is allowed
            cell.AddElement(para);

            return cell;
        }


        private static PdfPCell CreatePickupHeaderCell(string text)
        {
            var cell = new PdfPCell(new Phrase(text,
                FontFactory.GetFont("Inter", 9, Font.NORMAL, new BaseColor(21, 21, 23))))
            {
                BackgroundColor = new BaseColor(247, 247, 248),
                BorderColor = new BaseColor(238, 238, 240),
                BorderWidth = 1f,
                Padding = 6f,
                HorizontalAlignment = Element.ALIGN_LEFT,
                NoWrap = true
            };
            return cell;
        }

        private static PdfPCell CreatePickupBodyCell(string text, bool isFirstCell = false, bool isLastCell = false)
        {
            var cell = new PdfPCell(new Phrase(text,
                FontFactory.GetFont("Inter", 9, Font.NORMAL, new BaseColor(51, 51, 51))))
            {
                BorderColor = new BaseColor(238, 238, 240),
                BorderWidthTop = 0f,
                BorderWidthBottom = 1f,
                BorderWidthLeft = isFirstCell ? 1f : 0f,
                BorderWidthRight = isLastCell ? 1f : 0f,
                Padding = 6f,
                HorizontalAlignment = Element.ALIGN_LEFT
            };
            return cell;
        }


        public class RoundedBorderCellEvent : IPdfPCellEvent
        {
            private readonly float radius;
            public RoundedBorderCellEvent(float radius) { this.radius = radius; }

            public void CellLayout(PdfPCell cell, Rectangle rect, PdfContentByte[] canvas)
            {
                PdfContentByte cb = canvas[PdfPTable.BACKGROUNDCANVAS];
                cb.RoundRectangle(rect.Left, rect.Bottom, rect.Width, rect.Height, radius);
                cb.SetLineWidth(1f);
                cb.SetColorStroke(new BaseColor(238, 238, 240));
                cb.Stroke();
            }
        }
    }
}