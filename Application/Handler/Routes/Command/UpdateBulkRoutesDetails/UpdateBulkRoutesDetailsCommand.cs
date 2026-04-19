using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateBulkRoutesDetails
{
    public class UpdateBulkRoutesDetailsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int? RouteID { get; set; }
        public string? RouteNumber { get; set; }
        public string? Time { get; set; }
        public string? Mosdos { get; set; }
        public string? RouteName { get; set; }
        public string? Grade { get; set; }
        public int? Type { get; set; }
        public string? Days { get; set; }
        public string? DropOffBuilding { get; set; }
        public int? UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? TotalBuses { get; set; }
        public string? Areas { get; set; }
        public string? Branch { get; set; }
        public int? PickUp { get; set; }
        public int? SeasonFolderId { get; set; }
        public bool? ConfirmRoute { get; set; } = false;
        public decimal? Price { get; set; }
        public int? TodaysBus { get; set; }
        public Guid RouteGroupingID { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DriverNotes { get; set; }
    }
}
