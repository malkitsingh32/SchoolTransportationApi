using Application.Abstraction.Repositories;
using Application.Abstraction.Services;
using DTO.Request.BackgroundServices;
using DTO.Response;
using DTO.Response.BackgroundServices;
using DTO.Response.CardknoxCustomers;
using DTO.Response.Routes;
using Helper.Constant;
using System.Xml;

namespace Infrastructure.Implementation.Services
{
    public class BackgroundsServices : IBackgroundServices
    {
        private readonly IBackgroundRepository _backgroundRepository;
        public BackgroundsServices(IBackgroundRepository backgroundRepository)
        {
            _backgroundRepository = backgroundRepository;
        }
        public async Task<CommonResultResponseDto<string>> AddHistory(AddHistoryRequestDto addHistoryRequestDto)
        {
            var request = ConvertProtocolsToXML(addHistoryRequestDto.History);
            var historyId = await _backgroundRepository.AddHistory(request);
            if (historyId > 0 )
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, historyId);
            }
            else if (historyId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, historyId);
            }
        }        
        
        public async Task<CommonResultResponseDto<string>> AddBuLocationData(BusLocationResponseDto busLocationResponseDto)
        {
            var request = ConvertBusLocationToXML(busLocationResponseDto.Data);
            var busLocationId = await _backgroundRepository.AddBuLocationData(request);
            if (busLocationId > 0 )
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, busLocationId);
            }
            else if (busLocationId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, busLocationId);
            }
        }
        public async Task<CommonResultResponseDto<string>> AddCardknoxCustomers(List<CardknoxCustomersResponseDto> cardknoxCustomersResponseDto)
        {
            var request = ConvertCardknoxCustomersToXML(cardknoxCustomersResponseDto);
            var historyId = await _backgroundRepository.AddCardknoxCustomers(request);
            if (historyId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, historyId);
            }
            else if (historyId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, historyId);
            }
        }
        public async Task<CommonResultResponseDto<string>> AddTransaction(List<TransactionDto> transactionDto)
        {
            var request = ConvertTransactionsToXML(transactionDto);
            var historyId = await _backgroundRepository.AddTransaction(request);
            if (historyId > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, historyId);
            }
            else if (historyId < 0)
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
            else
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Updated }, null, historyId);
            }
        }

        public async Task<IList<GetHistoryResponseDto>> GetHistory()
        {
            var history = await _backgroundRepository.GetHistory();
            return history;
        }
        public async Task<IList<GetMonthlyChargesByFamilyIdResponseDto>> getMonthlyChargesByFamilyId()
        {
            var charges = await _backgroundRepository.getMonthlyChargesByFamilyId();
            return charges;
        }

        public async Task<CommonResultResponseDto<string>> AddPayroll()
        {
            var payrollRes = await _backgroundRepository.AddPayroll();
            if (payrollRes > 0)
            {
                return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, payrollRes);
            }
            else 
            {
                return CommonResultResponseDto<string>.Failure(new string[] { "Something went wrong" }, null);
            }
        }

        public async Task<CommonResultResponseDto<string>> InsertNextDayRouteDetails()
        {
            var payrollRes = await _backgroundRepository.InsertNextDayRouteDetails();           
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, payrollRes);           
        }

        public async Task<IList<GetRoutesResponseDto>> GetTodayUpcomingRoutes()
        {
            var todayRoutesRes = await _backgroundRepository.GetTodayUpcomingRoutes();
            return todayRoutesRes;
        }

        public async Task<CommonResultResponseDto<string>> InsertStopPassedByBusOnRoute(int routeId, DateTime? routeDate, int lastPassedStop)
        {
            var stopPassedRes = await _backgroundRepository.InsertStopPassedByBusOnRoute(routeId, routeDate, lastPassedStop);
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, stopPassedRes);
        }
        public async Task<CommonResultResponseDto<string>> RemoveExpiredTempBusDrivers()
        {
            var removeExpired = await _backgroundRepository.RemoveExpiredTempBusDrivers();
            return CommonResultResponseDto<string>.Success(new string[] { ActionStatusConstant.Created }, null, removeExpired);
        }
        private static string ConvertProtocolsToXML(IList<GetHistoryResponseDto> getHistoryResponseDto)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in getHistoryResponseDto)
            {
                Guid commonId = Guid.NewGuid();
                XmlNode medicationsNode = xmlDocument.CreateElement("getHistoryResponseDto");
                XmlAttribute attribute = xmlDocument.CreateAttribute("Date");
                attribute.Value = item.Date != null ? item.Date.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("RouteId");
                attribute.Value = item.RouteId != null ? item.RouteId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("BusDetailId");
                attribute.Value = item.BusDetailId != null ? item.BusDetailId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("DefaultDriverId");
                attribute.Value = item.DefaultDriverId != null ? item.DefaultDriverId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("DriverId");
                attribute.Value = item.DriverId != null ? item.DriverId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);
            }
            return xmlDocument.OuterXml;
        }

        private static string ConvertBusLocationToXML(List<VehicleData> vehicleData)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in vehicleData)
            {
                Guid commonId = Guid.NewGuid();
                XmlNode medicationsNode = xmlDocument.CreateElement("vehicleData");
                XmlAttribute attribute = xmlDocument.CreateAttribute("Id");
                attribute.Value = item.Id != null ? item.Id.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Latitude");
                attribute.Value = item.Gps.Latitude != null ? item.Gps.Latitude.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Longitude");
                attribute.Value = item.Gps.Longitude != null ? item.Gps.Longitude.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("FormattedLocation");
                attribute.Value = item.Gps.ReverseGeo.FormattedLocation != null ? item.Gps.ReverseGeo.FormattedLocation.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Time");
                attribute.Value = item.Gps.Time != null ? item.Gps.Time.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Name");
                attribute.Value = item.Name != null ? item.Name.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);
            }
            return xmlDocument.OuterXml;
        }

        private static string ConvertCardknoxCustomersToXML(IList<CardknoxCustomersResponseDto> cardknoxCustomersResponseDto)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in cardknoxCustomersResponseDto)
            {
                Guid commonId = Guid.NewGuid();
                XmlNode medicationsNode = xmlDocument.CreateElement("cardknoxCustomersResponseDto");
                XmlAttribute attribute = xmlDocument.CreateAttribute("CustomerId");
                attribute.Value = item.CustomerId != null ? item.CustomerId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("Email");
                attribute.Value = item.Email != null ? item.Email.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("CustomerNumber");
                attribute.Value = item.CustomerNumber != null ? item.CustomerNumber.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("CustomerNotes");
                attribute.Value = item.CustomerNotes != null ? item.CustomerNotes.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("BillPhone");
                attribute.Value = item.BillPhone != null ? item.BillPhone.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);
            }
            return xmlDocument.OuterXml;
        }

        private static string ConvertTransactionsToXML(IList<TransactionDto> transactionDto)
        {
            XmlDocument xmlDocument = new();
            XmlNode rootNode = xmlDocument.CreateElement("Root");
            xmlDocument.AppendChild(rootNode);

            foreach (var item in transactionDto)
            {
                Guid commonId = Guid.NewGuid();
                XmlNode medicationsNode = xmlDocument.CreateElement("Transaction");
                XmlAttribute attribute = xmlDocument.CreateAttribute("CustomerId");
                attribute.Value = item.CustomerId != null ? item.CustomerId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("TransactionId");
                attribute.Value = item.TransactionId != null ? item.TransactionId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("ScheduleId");
                attribute.Value = item.ScheduleId != null ? item.ScheduleId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("TransactionDate");
                attribute.Value = item.TransactionDate != null ? item.TransactionDate.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("PaymentMethodId");
                attribute.Value = item.PaymentMethodId != null ? item.PaymentMethodId.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("GatewayStatus");
                attribute.Value = item.GatewayStatus != null ? item.GatewayStatus.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);

                attribute = xmlDocument.CreateAttribute("GatewayRefNum");
                attribute.Value = item.GatewayRefNum != null ? item.GatewayRefNum.ToString() : "";
                medicationsNode.Attributes.Append(attribute);
                rootNode.AppendChild(medicationsNode);
            }
            return xmlDocument.OuterXml;
        }

    }
}
