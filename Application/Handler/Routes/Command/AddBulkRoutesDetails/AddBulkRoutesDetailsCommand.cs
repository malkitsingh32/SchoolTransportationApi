using DTO.Request.Routes;
using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.AddBulkRoutesDetails
{
    public class AddBulkRoutesDetailsCommand : IRequest<CommonResultResponseDto<IList<string>>>
    {
        public int? RouteId { get; set; }
        public string? RouteNumber { get; set; }
        public string? Times { get; set; }
        public string? School { get; set; }
        public string? Grade { get; set; }
        public string? Branch { get; set; }
        public string? TotalBuses { get; set; }
        public string? DropOffBuilding { get; set; }
        public int? CreatedBy { get; set; }
        public string? Days { get; set; }
        public string? EndDate { get; set; }
        public int? Gender { get; set; }
        public string? RouteName { get; set; }
        public string? Areas { get; set; }
        public string? StartDate { get; set; }
        public int? Type { get; set; }
        public List<StudentBusDto> Students { get; set; }
        public int? PickUp { get; set; }
        public int? SeasonFolderId { get; set; }
        public bool? IsDraftRoute { get; set; }
        public string? BusTeacherName { get; set; }
        public string? BusTeacherPhone { get; set; }
        public string? Color { get; set; }
        public bool ExclusivelyPay { get; set; }
        public List<OverrideStudentDto> OverrideStudentList { get; set; }


    }
}
