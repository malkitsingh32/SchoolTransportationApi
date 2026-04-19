using Application.Abstraction.Services;
using Application.Settings;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using DTO.Request.Routes;
using DTO.Request.Twilio;
using DTO.Response.BusStatus;
using DTO.Response.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using TTS_API.Services;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.TwiML.Voice;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    public class TwilioController : BaseController
    {
        private readonly IStudentsService _studentService;
        private readonly IAdminService _adminService;
        private readonly IBusDetailsService _busDetailsService;
        private const string TWILIO_VOICE = "Polly.Danielle-Generative";
        private readonly ITtsService _ttsService;
        private readonly TTSSetiings _ttsSettings;
        private readonly ISmsService _smsService;

        public TwilioController(IStudentsService studentsService, IAdminService adminService, IBusDetailsService busDetailsService, ITtsService ttsService, IOptions<TTSSetiings> ttsSettings, ISmsService smsService)
        {
            _studentService = studentsService;
            _adminService = adminService;
            _busDetailsService = busDetailsService;
            _ttsService = ttsService;
            _ttsSettings = ttsSettings.Value;
            _smsService = smsService;
        }

        [HttpPost("incoming-call")]
        [AllowAnonymous]
        public async Task<IActionResult> IncomingCall()
        {
            var response = new VoiceResponse();
            try
            {
                response.Say("Hello! This is an automated response from Twilio.");
            }
            catch (Exception ex)
            {
                response.Say("Something went wrong");
            }

            return Content(response.ToString(), "application/xml");
        }

        [HttpPost("handle-call")]
        [AllowAnonymous]
        public async Task<IActionResult> HandleIncomingCall([FromForm] TwilioRequest request)
        {
            var response = new VoiceResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                var routes = await _studentService.GetRecordsWithin30Minutes();
                if (routes.Data == false)
                {
                    response.Say("No route found in next or last 30 minutes");
                    return Content(response.ToString(), "application/xml");
                }
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);


                if (students.Data == null || students.Data.Count == 0)
                {
                    response.Say("Your number is not registered in our system.");
                }
                else if (students.Data.Count == 1)
                {
                    response.Say($"Fetching details for {students.Data[0].FirstName} {students.Data[0].LastName}.");
                    var student = await _studentService.GetStudentRouteInfo(students.Data[0].StudentID);
                    if (student?.Data == null)
                    {
                        response.Say("Student Information not found.");
                    }
                    else if (student?.Data?.Count == 0)
                    {
                        response.Say("There are no route assigned to this student.");
                    }
                    else
                    {
                        bool matched = false;
                        List<string> validMessages = new();
                        var today = DateTime.Today;
                        var now = DateTime.Now;
                        var windowEnd = now.AddMinutes(45);
                        var todayDay = today.ToString("ddd");
                        foreach (var route in student.Data)
                        {
                            var startDate = route?.StartDate;
                            var endDate = route?.EndDate;
                            var routeTime = route?.Time;
                            var days = route?.Days;
                            if (startDate != null && endDate != null && routeTime != null && !string.IsNullOrEmpty(days))
                            {
                                var start = DateTime.Parse(startDate.ToString());
                                var end = DateTime.Parse(endDate.ToString());

                                if (today >= start.Date && today <= end.Date)
                                {
                                    var daysList = days.Split(',').Select(d => d.Trim());

                                    if (daysList.Contains(todayDay))
                                    {
                                        TimeSpan routeTimeSpan = TimeSpan.Parse(routeTime.ToString());
                                        DateTime routeDateTime = today.Add(routeTimeSpan);
                                        if (routeDateTime >= now && routeDateTime <= windowEnd)
                                        {
                                            matched = true;
                                            validMessages.Add($"{route?.FirstName}  bus for route number {route?.RouteNumber} is at  {route?.FormattedLocation}.");
                                        }
                                    }
                                }
                            }
                        }
                        if (!matched)
                        {
                            var name = student.Data[0]?.FirstName ?? "The student";
                            response.Say($"{name} does not have any route detail for today.");
                        }
                        else
                        {
                            var message = string.Join("\n", validMessages);
                            response.Say(message);
                        }
                    }

                }
                else
                {
                    string processSelectionUrl = "https://darkaiyosherapi.datavanced.com/api/twilio/process-selection";
                    var gather = new Gather(new[] { Gather.InputEnum.Dtmf }, numDigits: 1, action: new Uri(processSelectionUrl));

                    for (int i = 0; i < students.Data.Count; i++)
                    {
                        gather.Say($"Press {i + 1} for {students.Data[i].FirstName}.");
                    }
                    response.Append(gather);
                    response.Redirect(new Uri(processSelectionUrl));
                }

                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                response.Say("Something went wrong");
                return Content(response.ToString(), "application/xml");
            }
        }

        [HttpPost("process-selection")]
        [AllowAnonymous]
        public async Task<IActionResult> ProcessSelection([FromForm] TwilioRequest request)
        {
            var response = new VoiceResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                string selectedOption = request.Digits;
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);
                if (students?.Data == null || students.Data.Count == 0)
                {
                    response.Say("No students found for this number.");
                    return Content(response.ToString(), "application/xml");
                }

                if (int.TryParse(selectedOption, out int index) && index > 0 && index <= students.Data.Count)
                {
                    var student = students.Data[index - 1];
                    response.Say($"You selected {student.FirstName} {student.LastName}. Retrieving details.");
                    var studentById = await _studentService.GetStudentRouteInfo(student.StudentID);
                    if (studentById?.Data == null)
                    {
                        response.Say("Student not found.");
                    }
                    else if (studentById?.Data?.Count == 0)
                    {
                        response.Say("There are no route assigned to this student.");
                    }
                    else
                    {
                        bool matched = false;
                        List<string> validMessages = new();
                        var today = DateTime.Today;
                        var now = DateTime.Now;
                        var windowEnd = now.AddMinutes(45);
                        var todayDay = today.ToString("ddd");
                        foreach (var route in studentById.Data)
                        {
                            var startDate = route?.StartDate;
                            var endDate = route?.EndDate;
                            var routeTime = route?.Time;
                            var days = route?.Days;
                            if (startDate != null && endDate != null && routeTime != null && !string.IsNullOrEmpty(days))
                            {
                                var start = DateTime.Parse(startDate.ToString());
                                var end = DateTime.Parse(endDate.ToString());

                                if (today >= start.Date && today <= end.Date)
                                {
                                    var daysList = days.Split(',').Select(d => d.Trim());

                                    if (daysList.Contains(todayDay))
                                    {
                                        TimeSpan routeTimeSpan = TimeSpan.Parse(routeTime.ToString());
                                        DateTime routeDateTime = today.Add(routeTimeSpan);
                                        if (routeDateTime >= now && routeDateTime <= windowEnd)
                                        {
                                            matched = true;
                                            validMessages.Add($"{route?.FirstName}  bus for route number {route?.RouteNumber} is at  {route?.FormattedLocation}.");
                                        }
                                    }
                                }
                            }
                        }
                        if (!matched)
                        {
                            var name = studentById.Data[0]?.FirstName ?? "The student";
                            response.Say($"{name} does not have any route detail for today.");
                        }
                        else
                        {
                            var message = string.Join("\n", validMessages);
                            response.Say(message);
                        }
                    }
                    return Content(response.ToString(), "application/xml");
                }

                response.Say("Invalid selection. Please try again.");
                response.Redirect(new Uri("https://darkaiyosherapi.datavanced.com/api/twilio/handle-call"));
                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                response.Say("Something went wrong");
                return Content(response.ToString(), "application/xml");
            }

        }

        [HttpPost("sms-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> ReceiveSms([FromForm] string From, [FromForm] string Body)
        {
            var response = new MessagingResponse();
            try
            {
                await _adminService.AddLogs(Body, From, "Inbound");
                string callerNumber = FormatPhoneNumber(From);
                var routes = await _studentService.GetRecordsWithin30Minutes();
                if (routes.Data == false)
                {
                    response.Message("No route found in next or last 30 minutes");
                    return Content(response.ToString(), "application/xml");
                }
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);

                if (students?.Data == null || students.Data.Count == 0)
                {
                    response.Message("Your number is not registered in our system.");
                    await _adminService.AddLogs("Your number is not registered in our system.", From, "Outbound");
                    return Content(response.ToString(), "application/xml");
                }

                if (int.TryParse(Body, out int selectedOption))
                {
                    if (selectedOption > 0 && selectedOption <= students.Data.Count)
                    {
                        var student = students.Data[selectedOption - 1];
                        var studentInfo = await _studentService.GetStudentRouteInfo(student.StudentID);

                        if (studentInfo?.Data == null)
                        {
                            response.Message("Student not found.");
                            await _adminService.AddLogs("Student not found.", From, "Outbound");
                        }
                        else if (studentInfo?.Data?.Count == 0)
                        {
                            response.Message("There are no route assigned to this student.");
                        }
                        else
                        {
                            bool matched = false;
                            List<string> validMessages = new();
                            var today = DateTime.Today;
                            var now = DateTime.Now;
                            var windowEnd = now.AddMinutes(45);
                            var todayDay = today.ToString("ddd");
                            foreach (var route in studentInfo.Data)
                            {
                                var startDate = route?.StartDate;
                                var endDate = route?.EndDate;
                                var routeTime = route?.Time;
                                var days = route?.Days;
                                if (startDate != null && endDate != null && routeTime != null && !string.IsNullOrEmpty(days))
                                {
                                    var start = DateTime.Parse(startDate.ToString());
                                    var end = DateTime.Parse(endDate.ToString());

                                    if (today >= start.Date && today <= end.Date)
                                    {
                                        var daysList = days.Split(',').Select(d => d.Trim());

                                        if (daysList.Contains(todayDay))
                                        {
                                            TimeSpan routeTimeSpan = TimeSpan.Parse(routeTime.ToString());
                                            DateTime routeDateTime = today.Add(routeTimeSpan);
                                            if (routeDateTime >= now && routeDateTime <= windowEnd)
                                            {
                                                matched = true;
                                                //response.Message($"{route?.FirstName}  bus for route number {route?.RowNumber} is at  {route?.FormattedLocation}.");
                                                validMessages.Add($"{route?.FirstName}  bus for route number {route?.RouteNumber} is at  {route?.FormattedLocation}.");
                                                await _adminService.AddLogs($"{route?.FirstName}  bus is at  {route?.FormattedLocation}.", From, "Outbound");
                                            }
                                        }
                                    }
                                }
                            }
                            if (!matched)
                            {
                                var name = studentInfo.Data[0]?.FirstName ?? "The student";
                                response.Message($"{name} does not have any route detail for today.");
                                await _adminService.AddLogs($"{name} does not have any route detail for today.", From, "Outbound");
                            }
                            else
                            {
                                var message = string.Join("\n", validMessages);
                                response.Message(message);
                            }

                        }
                    }
                    else
                    {
                        response.Message("Invalid selection. Please reply with a valid number.");
                        await _adminService.AddLogs("Invalid selection. Please reply with a valid number.", From, "Outbound");
                    }
                }
                else if (students.Data.Count == 1)
                {
                    var studentInfo = await _studentService.GetStudentRouteInfo(students.Data[0].StudentID);

                    if (studentInfo?.Data == null)
                    {
                        response.Message("Student not found.");
                        await _adminService.AddLogs("Student not found.", From, "Outbound");
                    }
                    else if (studentInfo?.Data?.Count == 0)
                    {
                        response.Message("There are no route assigned to this student.");
                    }
                    else
                    {
                        bool matched = false;
                        List<string> validMessages = new();
                        var today = DateTime.Today;
                        var now = DateTime.Now;
                        var windowEnd = now.AddMinutes(45);
                        var todayDay = today.ToString("ddd");
                        foreach (var route in studentInfo.Data)
                        {
                            var startDate = route?.StartDate;
                            var endDate = route?.EndDate;
                            var routeTime = route?.Time;
                            var days = route?.Days;
                            if (startDate != null && endDate != null && routeTime != null && !string.IsNullOrEmpty(days))
                            {
                                var start = DateTime.Parse(startDate.ToString());
                                var end = DateTime.Parse(endDate.ToString());

                                if (today >= start.Date && today <= end.Date)
                                {
                                    var daysList = days.Split(',').Select(d => d.Trim());

                                    if (daysList.Contains(todayDay))
                                    {
                                        TimeSpan routeTimeSpan = TimeSpan.Parse(routeTime.ToString());
                                        DateTime routeDateTime = today.Add(routeTimeSpan);
                                        if (routeDateTime >= now && routeDateTime <= windowEnd)
                                        {
                                            matched = true;
                                            //response.Message($"{route?.FirstName}  bus for route number {route?.RowNumber} is at  {route?.FormattedLocation}.");
                                            validMessages.Add($"{route?.FirstName}  bus for route number {route?.RouteNumber} is at  {route?.FormattedLocation}.");
                                            await _adminService.AddLogs($"{route?.FirstName}  bus is at  {route?.FormattedLocation}.", From, "Outbound");
                                        }
                                    }
                                }
                            }
                        }
                        if (!matched)
                        {
                            var name = studentInfo.Data[0]?.FirstName ?? "The student";
                            response.Message($"{name} does not have any route detail for today.");
                            await _adminService.AddLogs($"{name} does not have any route detail for today.", From, "Outbound");
                        }
                        else
                        {
                            var message = string.Join("\n", validMessages);
                            response.Message(message);
                        }
                    }
                }
                else
                {
                    string message = "Multiple students found. Reply with the number:\n";
                    for (int i = 0; i < students.Data.Count; i++)
                    {
                        message += $"{i + 1}. {students.Data[i].FirstName} {students.Data[i].LastName}\n";
                    }
                    response.Message(message);
                    await _adminService.AddLogs(message, From, "Outbound");
                }
            }
            catch (Exception ex)
            {
                response.Message("Something went wrong");
                await _adminService.AddLogs("Something went wrong", From, "Outbound");
            }
            return Content(response.ToString(), "application/xml");
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            phoneNumber = new string(phoneNumber.Where(char.IsDigit).ToArray());

            if (phoneNumber.Length == 11 && phoneNumber.StartsWith("1"))
            {
                return $"({phoneNumber.Substring(1, 3)}) {phoneNumber.Substring(4, 3)}-{phoneNumber.Substring(7)}";
            }

            return phoneNumber;
        }


        [HttpPost("check-stop-passed-sms-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckStopPassedSMS([FromForm] string From, [FromForm] string Body)
        {
            var response = new MessagingResponse();
            try
            {
                await _adminService.AddLogs(Body, From, "Inbound");
                string callerNumber = FormatPhoneNumber(From);

                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);

                if (students?.Data == null || students.Data.Count == 0)
                {
                    string msg = "Your number is not registered in our system.";
                    await _adminService.AddLogs(msg, From, "Outbound");
                    response.Message(msg);
                    return Content(response.ToString(), "application/xml");

                }

                List<string> allMessages = new();

                foreach (var student in students.Data)
                {
                    // Case 1: Tracking disabled
                    if (student.IsTracking == false && student.RecordType == "Student")
                    {
                        allMessages.Add(
                            $"{student.FirstName}: Please contact billing department to enable tracking"
                        );
                        continue;
                    }

                    //var studentInfo = await _studentService.GetStudentRouteInfo(student.StudentID);
                    var studentInfo = await _studentService.GetRouteInfo(student.StudentID, callerNumber, student.RecordType == "Student" ? false : true);

                    // Case 2: No routes
                    if (studentInfo?.Data == null || studentInfo.Data.Count == 0)
                    {
                        allMessages.Add(
                            $"{student.FirstName}: There are no route assigned to {student.FirstName}"
                        );
                        continue;
                    }
                    foreach (var route in studentInfo.Data)
                    {
                        if (!string.IsNullOrEmpty(route.FormattedLocation))
                        {
                            allMessages.Add(
                                $"{student.FirstName}: {route.FormattedLocation}"
                            );
                        }
                    }
                }

                string finalMessage = string.Join("\n", allMessages);
                response.Message(finalMessage);
                await _adminService.AddLogs(finalMessage, From, "Outbound");
                //await _adminService.AddLogs(Body, From, "Inbound");
                //string callerNumber = FormatPhoneNumber(From);

                //var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);

                //if (students?.Data == null || students.Data.Count == 0)
                //{
                //    string msg = "Your number is not registered in our system.";
                //    await _adminService.AddLogs(msg, From, "Outbound");
                //    response.Message($"{msg}\n{msg}");
                //    return Content(response.ToString(), "application/xml");
                //}

                //if (int.TryParse(Body, out int selectedOption))
                //{
                //    if (selectedOption > 0 && selectedOption <= students.Data.Count)
                //    {
                //        var student = students.Data[selectedOption - 1];

                //        if (student.IsTracking == false)
                //        {
                //            string msg = "Please call the billing department to enable your bus tracking.";
                //            response.Message($"{msg}\n{msg}");
                //            await _adminService.AddLogs(msg, From, "Outbound");
                //            return Content(response.ToString(), "application/xml");
                //        }

                //        var studentInfo = await _studentService.GetStudentRouteInfo(student.StudentID);

                //        if (studentInfo?.Data == null)
                //        {
                //            string msg = "Student not found.";
                //            response.Message($"{msg}\n{msg}");
                //            await _adminService.AddLogs("Student not found.", From, "Outbound");
                //        }
                //        else if (studentInfo?.Data?.Count == 0)
                //        {
                //            string msg = "There are no route assigned to this student.";
                //            await _adminService.AddLogs(msg, From, "Outbound");
                //            response.Message($"{msg}\n{msg}");
                //        }
                //        else
                //        {
                //            List<string> validMessages = new();
                //            foreach (var route in studentInfo.Data)
                //            {
                //                if (!string.IsNullOrEmpty(route.FormattedLocation))
                //                {
                //                    validMessages.Add(route.FormattedLocation);
                //                }
                //            }
                //            var locationMessage = string.Join("\n", validMessages);

                //            var message =
                //                $"{locationMessage}. \n{locationMessage}\n. " +
                //                $" " +
                //                "Press the star key to go back to main menu";
                //            //var message = string.Join("\n", validMessages);

                //            response.Message($"{message}");
                //            await _adminService.AddLogs(message, From, "Outbound");
                //        }

                //    }
                //    else
                //    {
                //        string msg = "Invalid selection. Please reply with a valid number.";
                //        response.Message($"{msg}\n{msg}");
                //        await _adminService.AddLogs("Invalid selection. Please reply with a valid number.", From, "Outbound");
                //    }
                //}
                //else if (students.Data.Count == 1)
                //{
                //    if (students.Data[0].IsTracking == false)
                //    {
                //        string msg = "Please call the billing department to enable your bus tracking.";
                //        response.Message($"{msg}\n{msg}");
                //        await _adminService.AddLogs(msg, From, "Outbound");
                //        return Content(response.ToString(), "application/xml");
                //    }
                //    var studentInfo = await _studentService.GetStudentRouteInfo(students.Data[0].StudentID);

                //    if (studentInfo?.Data == null)
                //    {
                //        string msg = "Student not found.";
                //        response.Message($"{msg}\n{msg}");
                //        await _adminService.AddLogs("Student not found.", From, "Outbound");
                //    }
                //    else if (studentInfo?.Data?.Count == 0)
                //    {
                //        string msg = "There are no route assigned to this student.";
                //        await _adminService.AddLogs(msg, From, "Outbound");
                //        response.Message($"{msg}\n{msg}");
                //    }
                //    else
                //    {
                //        List<string> validMessages = new();
                //        foreach (var route in studentInfo.Data)
                //        {
                //            if (!string.IsNullOrEmpty(route.FormattedLocation))
                //            {
                //                validMessages.Add(route.FormattedLocation);
                //            }
                //        }
                //        var message = string.Join("\n", validMessages);
                //        response.Message($"{message}\n{message}");
                //        await _adminService.AddLogs(message, From, "Outbound");                        
                //    }
                //}
                //else
                //{
                //    string message = "Multiple students found. Reply with the number:\n";
                //    for (int i = 0; i < students.Data.Count; i++)
                //    {
                //        message += $"{i + 1}. {students.Data[i].FirstName} {students.Data[i].LastName}\n";
                //    }
                //    response.Message($"{message}\n{message}");
                //    await _adminService.AddLogs(message, From, "Outbound");
                //}
            }
            catch (Exception ex)
            {
                string msg = "Something went wrong";
                response.Message($"{msg}\n{msg}");
                await _adminService.AddLogs(msg, From, "Outbound");
            }
            return Content(response.ToString(), "application/xml");
        }

        [HttpPost("check-stop-passed-handle-call")]
        [AllowAnonymous]
        public async Task<IActionResult> HandleCheckStopIncomingCall([FromForm] TwilioRequest request)
        {
            var response = new VoiceResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);

                if (students.Data == null || students.Data.Count == 0)
                {
                    var textNoStudent = "Your number is not registered in our system.";
                    var audioUrlNoStudent = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNoStudent)}";
                    response.Play(new Uri(audioUrlNoStudent));
                    response.Play(new Uri(audioUrlNoStudent));
                    await _adminService.AddLogs("Your number is not registered in our system.", request.From, "Outbound");
                }

                else if (students.Data.Count == 1)
                {
                    //response.Say($"Fetching details for {students.Data[0].FirstName} {students.Data[0].LastName}.", voice: TWILIO_VOICE);
                    if (students.Data[0].IsTracking == false && students.Data[0].RecordType == "Student")
                    {
                        //response.Say("Please call the billing department to enable your bus tracking.", voice: TWILIO_VOICE);
                        //response.Say("Please call the billing department to enable your bus tracking.", voice: TWILIO_VOICE);
                        
                        var textBilling = "Please call the billing department to enable your bus tracking.";
                        var audioUrlBilling = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textBilling)}";
                        response.Play(new Uri(audioUrlBilling));
                        response.Play(new Uri(audioUrlBilling));
                        await _adminService.AddLogs("Please call the billing department to enable your bus tracking.", request.From, "Outbound");
                        return Content(response.ToString(), "application/xml");
                    }
                    var student = await _studentService.GetRouteInfo(students.Data[0].StudentID, callerNumber, students.Data[0].RecordType == "Student" ? false : true);
                    if (student?.Data == null)
                    {
                        //response.Say("Student Information not found.", voice: TWILIO_VOICE);
                        //response.Say("Student Information not found.", voice: TWILIO_VOICE);

                        var textNotFound = "Student Information not found.";
                        var audioUrlNotFound = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNotFound)}";
                        response.Play(new Uri(audioUrlNotFound));
                        response.Play(new Uri(audioUrlNotFound));
                        await _adminService.AddLogs("Student Information not found.", request.From, "Outbound");
                    }
                    else if (student?.Data?.Count == 0)
                    {
                        //response.Say("There are no route assigned to this student.", voice: TWILIO_VOICE);
                        //response.Say("There are no route assigned to this student.", voice: TWILIO_VOICE);
                        
                        var textNoRoute = "There are no route assigned to this student.";
                        var audioUrlNoRoute = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNoRoute)}";
                        response.Play(new Uri(audioUrlNoRoute));
                        response.Play(new Uri(audioUrlNoRoute));
                        await _adminService.AddLogs("There are no route assigned to this student.", request.From, "Outbound");
                    }
                    else
                    {
                        List<string> validMessages = new();
                        foreach (var route in student.Data)
                        {
                            if (!string.IsNullOrEmpty(route.FormattedLocation))
                            {
                                validMessages.Add(route.FormattedLocation);
                            }
                        }
                        var message = string.Join("\n", validMessages);
                        //response.Say(message, voice: TWILIO_VOICE);
                        //response.Say(message, voice: TWILIO_VOICE);

                        var audioUrlNoRoute = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(message)}";
                        response.Play(new Uri(audioUrlNoRoute));
                        //response.Play(new Uri(audioUrlNoRoute));
                        await _adminService.AddLogs(message, request.From, "Outbound");
                    }

                }
                else
                {
                    //string processSelectionUrl = "https://darkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-process-selection";
                    //var gather = new Gather(new[] { Gather.InputEnum.Dtmf }, numDigits: 1, action: new Uri(processSelectionUrl));

                    //for (int i = 0; i < students.Data.Count; i++)
                    //{
                    //    gather.Say($"Press {i + 1} for {students.Data[i].FirstName} {students.Data[i].LastName}.", voice: TWILIO_VOICE);
                    //}
                    //for (int i = 0; i < students.Data.Count; i++)
                    //{
                    //    gather.Say($"Press {i + 1} for {students.Data[i].FirstName} {students.Data[i].LastName}.", voice: TWILIO_VOICE);
                    //}
                    //response.Append(gather);

                    // URL for processing the selection after DTMF input
                    string processSelectionUrl = _ttsSettings.BaseUrl + "check-stop-passed-process-selection";

                    // Combine all student options into a single string
                    List<string> studentMessages = new();
                    for (int i = 0; i < students.Data.Count; i++)
                    {
                        studentMessages.Add($"Press {i + 1} for {students.Data[i].FirstName}");
                    }

                    // Repeat the message for clarity
                    var messageText = string.Join(". ", studentMessages) + ". " +
                                      string.Join(". ", studentMessages) + ".";

                    // Generate TTS URL
                    var ttsUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(messageText)}";

                    // Create the Gather element
                    var gather = new Gather(
                        input: new[] { Gather.InputEnum.Dtmf },
                        numDigits: 1,
                        timeout: 60,
                        action: new Uri(processSelectionUrl)
                    );

                    // Play the custom TTS audio inside Gather
                    gather.Play(new Uri(ttsUrl), loop: 1); // loop 1 since we already repeated text

                    // Append Gather to the response
                    response.Append(gather);

                    // Log the message
                    await _adminService.AddLogs(messageText, request.From, "Outbound");

                }

                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                //response.Say("Something went wrong", voice: TWILIO_VOICE);

                var text = "Something went wrong";
                var audioUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(text)}";
                response.Play(new Uri(audioUrl));
                response.Play(new Uri(audioUrl));
                await _adminService.AddLogs("Something went wrong", request.From, "Outbound");
                return Content(response.ToString(), "application/xml");
            }
        }

        [HttpPost("check-stop-passed-process-selection")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        //[ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> ProcessCheckStopSelection([FromForm] TwilioRequest request)
        {
            var response = new VoiceResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                string selectedOption = request.Digits;
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);
                if (students?.Data == null || students.Data.Count == 0)
                {
                    //response.Say("No students found for this number.", voice: TWILIO_VOICE);
                    //response.Say("No students found for this number.", voice: TWILIO_VOICE);             

                    var textNoStudent = "No students found for this number";
                    var audioUrlNoStudent = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNoStudent)}";
                    response.Play(new Uri(audioUrlNoStudent));
                    response.Play(new Uri(audioUrlNoStudent));
                    await _adminService.AddLogs("No students found for this number.", request.From, "Outbound");
                    return Content(response.ToString(), "application/xml");
                }

                if (int.TryParse(selectedOption, out int index) && index > 0 && index <= students.Data.Count)
                {
                    var student = students.Data[index - 1];
                    if (student.IsTracking == false && student.RecordType == "Student")
                    {
                        //response.Say("Please call the billing department to enable your bus tracking.", voice: TWILIO_VOICE);
                        //response.Say("Please call the billing department to enable your bus tracking.", voice: TWILIO_VOICE);

                        var textBilling = "Please call the billing department to enable your bus tracking.";
                        var audioUrlBilling = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textBilling)}";
                        response.Play(new Uri(audioUrlBilling));
                        response.Play(new Uri(audioUrlBilling));
                        await _adminService.AddLogs("Please call the billing department to enable your bus tracking.", request.From, "Outbound");
                        return Content(response.ToString(), "application/xml");
                    }

                    //var studentById = await _studentService.GetStudentRouteInfo(student.StudentID);
                    var studentById = await _studentService.GetRouteInfo(student.StudentID, callerNumber, student.RecordType == "Student" ? false : true);
                    if (studentById?.Data == null)
                    {
                        //response.Say("Student not found.", voice: TWILIO_VOICE);
                        //response.Say("Student not found.", voice: TWILIO_VOICE);

                        var textNotFound = "Student not found.";
                        var audioUrlNotFound = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNotFound)}";
                        response.Play(new Uri(audioUrlNotFound));
                        response.Play(new Uri(audioUrlNotFound));
                        await _adminService.AddLogs("Student not found.", request.From, "Outbound");
                    }
                    else if (studentById?.Data?.Count == 0)
                    {
                        //response.Say("There are no route assigned to this student.", voice: TWILIO_VOICE);
                        //response.Say("There are no route assigned to this student.", voice: TWILIO_VOICE);

                        var textNoRoute = "There are no route assigned to this student.";
                        var audioUrlNoRoute = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(textNoRoute)}";
                        response.Play(new Uri(audioUrlNoRoute));
                        response.Play(new Uri(audioUrlNoRoute));
                        await _adminService.AddLogs("There are no route assigned to this student.", request.From, "Outbound");
                    }
                    else
                    {
                        List<string> validMessages = new();
                        foreach (var route in studentById.Data)
                        {
                            if (!string.IsNullOrEmpty(route.FormattedLocation))
                            {
                                validMessages.Add(route.FormattedLocation);
                            }
                        }
                        var locationMessage = string.Join("\n", validMessages);
                        //var message =
                        //   $"{locationMessage}." + "Press the star key to go back to main menu";

                        //var ttsUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(message)}";
                        //var baseUriGather = new Uri(_ttsSettings.BaseUrl);
                        //var actionUri = new Uri(baseUriGather, "check-stop-passed-process-selection");
                        //var gather = new Gather(
                        //    input: new[] { Gather.InputEnum.Dtmf },
                        //    numDigits: 1,
                        //    timeout: 60,
                        //    action: actionUri
                        //);

                        //gather.Play(new Uri(ttsUrl));
                        //response.Append(gather);

                        var message = $"{locationMessage}.";

                        var ttsUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(message)}";

                        // Play location message
                        response.Play(new Uri(ttsUrl));

                        // Wait 3 seconds
                        response.Pause(length: 3);

                        // End call
                        response.Hangup();

                        await _adminService.AddLogs(message, request.From, "Outbound");
                    }
                    return Content(response.ToString(), "application/xml");
                }
                else if (selectedOption == "*")
                {
                    // Go back to main menu (student selection)
                    var baseUriMenu = new Uri(_ttsSettings.BaseUrl);
                    var redirectUriMenu = new Uri(baseUriMenu, "check-stop-passed-handle-call");

                    response.Redirect(redirectUriMenu);
                    //response.Redirect(new Uri("https://darkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-handle-call"));
                    return Content(response.ToString(), "application/xml");
                }

                //response.Say("Invalid selection. Please try again.", voice: TWILIO_VOICE);
                //response.Say("Invalid selection. Please try again.", voice: TWILIO_VOICE);

                var text = "Invalid selection. Please try again.";
                var audioUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(text)}";
                response.Play(new Uri(audioUrl));
                response.Play(new Uri(audioUrl));
                await _adminService.AddLogs("Invalid selection. Please try again.", request.From, "Outbound");

                var baseUri = new Uri(_ttsSettings.BaseUrl);
                var redirectUri = new Uri(baseUri, "check-stop-passed-handle-call");

                response.Redirect(redirectUri);

                //response.Redirect(new Uri("https://darkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-handle-call"));
                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                //response.Say("Something went wrong", voice: TWILIO_VOICE);
                //response.Say("Something went wrong", voice: TWILIO_VOICE);

                var text = "Something went wrong";
                var audioUrl = $"{_ttsSettings.BaseUrl}speak?text={Uri.EscapeDataString(text)}";
                response.Play(new Uri(audioUrl));
                response.Play(new Uri(audioUrl));
                await _adminService.AddLogs("Something went wrong", request.From, "Outbound");
                return Content(response.ToString(), "application/xml");
            }

        }

        [HttpPost("check-stop-passed-handle-call-View")]
        [AllowAnonymous]
        public async Task<IActionResult> HandleCheckStopIncomingCallView([FromForm] TwilioRequest request)
        {
            var response = new MessagingResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);

                if (students.Data == null || students.Data.Count == 0)
                {
                    var textNoStudent = "Your number is not registered in our system.";
                    response.Message(textNoStudent);
                    response.Message(textNoStudent);
                    await _adminService.AddLogs("Your number is not registered in our system.", request.From, "Outbound");
                }
                else if (students.Data.Count == 1)
                {
                    if (students.Data[0].IsTracking == false && students.Data[0].RecordType == "Student")
                    {
                        var textBilling = "Please call the billing department to enable your bus tracking.";
                        response.Message(textBilling);
                        response.Message(textBilling);
                        await _adminService.AddLogs("Please call the billing department to enable your bus tracking.", request.From, "Outbound");
                        return Content(response.ToString(), "application/xml");
                    }
                    var student = await _studentService.GetRouteInfo(students.Data[0].StudentID, callerNumber, students.Data[0].RecordType == "Student" ? false : true);
                    if (student?.Data == null)
                    {
                        var textNotFound = "Student Information not found.";
                        response.Message(textNotFound);
                        response.Message(textNotFound);
                        await _adminService.AddLogs("Student Information not found.", request.From, "Outbound");
                    }
                    else if (student?.Data?.Count == 0)
                    {
                        var textNoRoute = "There are no route assigned to this student.";
                        response.Message(textNoRoute);
                        response.Message(textNoRoute);
                        await _adminService.AddLogs("There are no route assigned to this student.", request.From, "Outbound");
                    }
                    else
                    {
                        List<string> validMessages = new();
                        foreach (var route in student.Data)
                        {
                            if (!string.IsNullOrEmpty(route.FormattedLocation))
                            {
                                validMessages.Add(route.FormattedLocation);
                            }
                        }
                        var message = string.Join("\n", validMessages);
                        response.Message(message);
                        //response.Message(message);
                        await _adminService.AddLogs(message, request.From, "Outbound");
                    }

                }
                else
                {
                    string processSelectionUrl = "https://betadarkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-process-selection";

                    // Combine all student options into a single string
                    List<string> studentMessages = new();
                    for (int i = 0; i < students.Data.Count; i++)
                    {
                        studentMessages.Add($"Press {i + 1} for {students.Data[i].FirstName}");
                    }

                    // Repeat the message for clarity
                    var messageText = string.Join(". ", studentMessages) + ". " +
                                      string.Join(". ", studentMessages) + ".";

                    response.Message(messageText);

                    // Log the message
                    await _adminService.AddLogs(messageText, request.From, "Outbound");

                }

                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                //response.Say("Something went wrong", voice: TWILIO_VOICE);

                var text = "Something went wrong";
                response.Message(text);
                response.Message(text);
                await _adminService.AddLogs("Something went wrong", request.From, "Outbound");
                return Content(response.ToString(), "application/xml");
            }
        }

        [HttpPost("check-stop-passed-process-selection-View")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        //[ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> ProcessCheckStopSelectionView([FromForm] TwilioRequest request)
        {
            var response = new MessagingResponse();
            try
            {
                string callerNumber = FormatPhoneNumber(request.From);
                string selectedOption = request.Digits;
                var students = await _studentService.GetStudentsByPhoneNumber(callerNumber);
                if (students?.Data == null || students.Data.Count == 0)
                {
                    var textNoStudent = "No students found for this number";
                    response.Message(textNoStudent);
                    response.Message(textNoStudent);
                    await _adminService.AddLogs("No students found for this number.", request.From, "Outbound");
                    return Content(response.ToString(), "application/xml");
                }

                if (int.TryParse(selectedOption, out int index) && index > 0 && index <= students.Data.Count)
                {
                    var student = students.Data[index - 1];
                    if (student.IsTracking == false && student.RecordType == "Student")
                    {
                        var textBilling = "Please call the billing department to enable your bus tracking.";
                        response.Message(textBilling);
                        response.Message(textBilling);
                        await _adminService.AddLogs("Please call the billing department to enable your bus tracking.", request.From, "Outbound");
                        return Content(response.ToString(), "application/xml");
                    }

                    var studentById = await _studentService.GetRouteInfo(student.StudentID, callerNumber, student.RecordType == "Student" ? false : true); 
                    if (studentById?.Data == null)
                    {
                        var textNotFound = "Student not found.";
                        response.Message(textNotFound);
                        response.Message(textNotFound);
                        await _adminService.AddLogs("Student not found.", request.From, "Outbound");
                    }
                    else if (studentById?.Data?.Count == 0)
                    {
                        var textNoRoute = "There are no route assigned to this student.";
                        response.Message(textNoRoute);
                        response.Message(textNoRoute);
                        await _adminService.AddLogs("There are no route assigned to this student.", request.From, "Outbound");
                    }
                    else
                    {
                        List<string> validMessages = new();
                        foreach (var route in studentById.Data)
                        {
                            if (!string.IsNullOrEmpty(route.FormattedLocation))
                            {
                                validMessages.Add(route.FormattedLocation);
                            }
                        }
                        var locationMessage = string.Join("\n", validMessages);
                        var message =
                           $"{locationMessage}."; 
                           //+ "Press the star key to go back to main menu";

                        response.Message(message);

                        await _adminService.AddLogs(message, request.From, "Outbound");
                    }
                    return Content(response.ToString(), "application/xml");
                }
                else if (selectedOption == "*")
                {
                    // Go back to main menu (student selection)
                    response.Redirect(new Uri("https://betadarkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-handle-call"));
                    return Content(response.ToString(), "application/xml");
                }

                var text = "Invalid selection. Please try again.";
                response.Message(text);
                response.Message(text);
                await _adminService.AddLogs("Invalid selection. Please try again.", request.From, "Outbound");

                response.Redirect(new Uri("https://betadarkaiyosherapi.datavanced.com/api/twilio/check-stop-passed-handle-call"));
                return Content(response.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                var text = "Something went wrong";
                response.Message(text);
                response.Message(text);
                await _adminService.AddLogs("Something went wrong", request.From, "Outbound");
                return Content(response.ToString(), "application/xml");
            }

        }

        [HttpGet("speak")]
        [ProducesResponseType(typeof(FileContentResult), 200)]
        [AllowAnonymous]
        public IActionResult QuickSpeak([FromQuery] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return BadRequest(new { error = "Text is required" });
            }

            try
            {
                var audioData = _ttsService.Synthesize(text, _ttsSettings.DefaultVoice);
                return File(audioData, "audio/wav");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("voices")]
        [AllowAnonymous]
        public IActionResult GetVoices()
        {
            var voices = _ttsService.GetVoices();
            return Ok(voices);
        }

        [HttpPost("send-bulk-route-sms")]
        public async Task<IActionResult> SendRouteSMS([FromBody] RouteSmsRequestDto request)
        {
            try
            {
                var phoneNumbersResponse = await _studentService.GetFatherCellsByRouteId(request.RouteId);

                if (phoneNumbersResponse?.Data == null || phoneNumbersResponse.Data.Count == 0)
                    return BadRequest("No phone numbers found for this route");

                var studentMessages = phoneNumbersResponse.Data
                     .Where(x => !string.IsNullOrEmpty(x.FatherCell))
                     .Select(x => new StudentSmsDto
                     {
                         PhoneNumber = x.FatherCell,
                         Message = ReplaceMessageTags(request.Message, x) // personalized per student
                     })
                     .ToList();

                await _smsService.SendBulkSmsAsync(studentMessages);

                return Ok(new
                {
                    success = true,
                    message = "Bulk SMS sent successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to send SMS: {ex.Message}");
            }
        }

        private string ReplaceMessageTags(string message, GetFatherCellsListResponseDto student)
        {
            return message
                .Replace("{Student First Name}", student.FirstName ?? "")
                .Replace("{Student Last Name}", student.LastName ?? "")
                .Replace("{Student Address}", student.Address ?? "");
        }

        private string CheckStop(string routePath, string passengerStop, double busLat, double busLng, string busName, int routeId)
        {
            // 1. Validate passenger stop
            if (string.IsNullOrEmpty(passengerStop))
                return "We could not find your stop information.";

            // 2. Parse route and passenger stop
            List<(double lat, double lng)> routePathList;
            List<(double lat, double lng)> passengerStopList;

            try
            {
                routePathList = routePath
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c =>
                    {
                        var parts = c.Split(',');
                        return (lat: double.Parse(parts[0]), lng: double.Parse(parts[1]));
                    }).ToList();

                passengerStopList = passengerStop
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c =>
                    {
                        var parts = c.Split(',');
                        return (lat: double.Parse(parts[0]), lng: double.Parse(parts[1]));
                    }).ToList();
            }
            catch
            {
                return "Invalid route or stop data format.";
            }

            if (routePathList.Count == 0) return "Route data is empty.";
            if (passengerStopList.Count == 0) return "Stop data is empty.";

            // 3. Find passenger stop index in route
            int passengerIndex = -1;
            for (int i = 0; i < routePathList.Count; i++)
            {
                if (HaversineDistance(routePathList[i], passengerStopList[0]) < 50)
                {
                    passengerIndex = i;
                    break;
                }
            }

            // 4. Get recent bus positions of bus history
            List<BusPosition> recentPositions;
            try
            {
                var recentPositionsResult = _busDetailsService.GetRecentBusPositions(busName, routeId);
                recentPositions = recentPositionsResult.Result?.Data?.ToList() ?? new List<BusPosition>();
            }
            catch
            {
                recentPositions = new List<BusPosition>();
            }

            // 5. Get bus progress - track last passed stop
            int lastPassedStop = GetLastPassedStop(routePathList, (busLat, busLng), recentPositions);
            double distanceFromFirstStop = HaversineDistance((busLat, busLng), routePathList[0]);

            // 6. Handle route not started (Case 1)
            if (distanceFromFirstStop > 500 && lastPassedStop == -1)
                return "The bus has not started the route yet.";

            // 7. Check if bus is currently at any stop
            int currentStopIndex = -1;
            for (int i = 0; i < routePathList.Count; i++)
            {
                double distance = HaversineDistance(routePathList[i], (busLat, busLng));
                if (distance <= 50 && i >= lastPassedStop && i <= lastPassedStop + 1)
                {
                    currentStopIndex = i;
                    break;
                }
            }

            // 8. Determine status relative to passenger stop
            //double distanceToPassengerStop = HaversineDistance((busLat, busLng), passengerStopList[0]);

            // Case 2: Bus is at passenger stop (currently within 50m)
            //if (currentStopIndex == passengerIndex || distanceToPassengerStop <= 50)
            if (currentStopIndex == passengerIndex)
                return "The bus is at your stop right now!";

            // Case 3: Bus has passed passenger stop (last passed stop is beyond passenger stop)
            if (lastPassedStop > passengerIndex)
            {
                // Find recent bus positions that were close to the passenger stop (<= 50m)
                var closePositions = recentPositions
                    .Where(pos => HaversineDistance(passengerStopList[0], (pos.Latitude, pos.Longitude)) <= 50)
                    .Where(pos => pos.Timestamp != null)
                    .ToList();

                if (closePositions.Any())
                {
                    // Pick the most recent position (largest timestamp)
                    var mostRecent = closePositions
                        .OrderByDescending(p => p.Timestamp)
                        .First();

                    int minutesAgo = (int)Math.Round((DateTime.UtcNow - mostRecent.Timestamp.ToUniversalTime()).TotalMinutes);
                    if (minutesAgo < 0) minutesAgo = 0; // guard

                    return $"The bus has already passed your stop {minutesAgo} minute{(minutesAgo == 1 ? "" : "s")} ago.";
                }

                // Fallback if we couldn't find a recent close position with a timestamp
                return "The bus has already passed your stop.";
            }

            // Case 4: Bus hasn't reached passenger stop yet
            if (lastPassedStop < passengerIndex)
            {
                int stopsAway = passengerIndex - Math.Max(lastPassedStop, 0);
                return $"The bus has not yet reached your stop. It is approximately {stopsAway} stop(s) away.";
            }

            // Special case: Bus is exactly at the last passed stop (not passenger stop)
            if (lastPassedStop == passengerIndex && currentStopIndex != passengerIndex)
            {
                // Bus passed this stop historically but is not currently here
                return "The bus has already passed your stop.";
            }

            return "Bus status unknown.";
        }

        private int GetLastPassedStop(
            List<(double lat, double lng)> route,
            (double lat, double lng) currentBus,
            List<BusPosition> history)
        {
            int lastPassedStop = -1;

            // 1. Check historical positions to find stops the bus has definitely passed
            if (history != null && history.Count > 0)
            {
                history.Reverse();
                foreach (var pos in history)
                {
                    int nextStop = lastPassedStop + 1;
                    // If lastPassedStop = -1, then nextStop = 0 (first stop)
                    // If lastPassedStop = 0, then nextStop = 1 (second stop)
                    // If lastPassedStop = 1, then nextStop = 2 (third stop), etc.

                    if (nextStop < route.Count)
                    {
                        double distance = HaversineDistance(route[nextStop], (pos.Latitude, pos.Longitude));

                        if (distance < 50)
                        {
                            lastPassedStop = nextStop;
                        }
                    }
                }
            }

            // 2. Check current position against next stop

            int currentNextStop = lastPassedStop + 1;

            if (currentNextStop < route.Count)
            {
                double distance = HaversineDistance(route[currentNextStop], currentBus);
                if (distance < 50)
                {
                    lastPassedStop = currentNextStop;
                }
            }

            // 3. If no stops found via proximity, determine progress along route segments
            //if (lastPassedStop == -1)
            //{
            //    // Find which segment the bus is closest to
            //    double minDistance = double.MaxValue;
            //    int closestSegment = -1;

            //    for (int i = 0; i < route.Count - 1; i++)
            //    {
            //        double distance = DistancePointToSegment(currentBus, route[i], route[i + 1]);
            //        if (distance < minDistance && distance < 200) // Within reasonable distance of route
            //        {
            //            minDistance = distance;
            //            closestSegment = i;
            //        }
            //    }

            //    if (closestSegment != -1)
            //    {
            //        // Bus is between stops, consider it has passed the starting stop of that segment
            //        lastPassedStop = closestSegment;
            //    }
            //}

            return lastPassedStop;
        }

        private double DistancePointToSegment((double lat, double lng) p, (double lat, double lng) v, (double lat, double lng) w)
        {
            // Convert to radians for proper geographic calculations
            const double R = 6371000; // Earth radius in meters

            double px = p.lat, py = p.lng;
            double vx = v.lat, vy = v.lng;
            double wx = w.lat, wy = w.lng;

            // Calculate segment length squared in degrees
            double l2 = Math.Pow(wx - vx, 2) + Math.Pow(wy - vy, 2);

            // If segment is a point, return distance to that point
            if (l2 == 0) return HaversineDistance(p, v);

            // Find projection parameter t (where point projects onto segment)
            // t=0 means projection is at v, t=1 means at w
            double t = ((px - vx) * (wx - vx) + (py - vy) * (wy - vy)) / l2;
            t = Math.Max(0, Math.Min(1, t)); // Clamp to segment

            // Calculate projected point coordinates
            double projLat = vx + t * (wx - vx);
            double projLng = vy + t * (wy - vy);

            // Return Haversine distance from point to projected point (in meters)
            return HaversineDistance(p, (projLat, projLng));
        }

        private double HaversineDistance((double lat, double lng) p1, (double lat, double lng) p2)
        {
            const double R = 6371000; // Earth radius in meters
            double lat1Rad = p1.lat * Math.PI / 180.0;
            double lat2Rad = p2.lat * Math.PI / 180.0;
            double dLat = (p2.lat - p1.lat) * Math.PI / 180.0;
            double dLon = (p2.lng - p1.lng) * Math.PI / 180.0;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // distance in meters
        }

        private string CleanFormattedLocation(string formattedLocation)
        {
            if (string.IsNullOrWhiteSpace(formattedLocation))
                return string.Empty;

            // Split into segments and take the FIRST meaningful part
            var first = formattedLocation
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(first))
                return string.Empty;

            // Remove ZIP/PIN codes if they were in the first segment accidentally
            first = Regex.Replace(first, @"\b\d{5,6}\b", "").Trim();

            // Normalize spacing
            first = Regex.Replace(first, @"\s{2,}", " ").Trim();

            return first;
        }


    }

}
