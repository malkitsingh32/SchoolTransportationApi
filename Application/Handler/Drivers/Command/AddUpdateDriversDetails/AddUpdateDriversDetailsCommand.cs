using DTO.Response;
using DTO.Response.Bus;
using MediatR;

namespace Application.Handler.Drivers.Command.AddUpdateDriversDetails
{
    public class AddUpdateDriversDetailsCommand : IRequest<CommonResultResponseDto<CheckDriverHasBusResponseDto>>
    {
        public int DriverID { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public int UserId { get; set; }
        public int? Route { get; set; }
        public int Bus { get; set; }
        public int? BusId { get; set; }
        public bool IsDelete { get; set; }
        public int? DriverType { get; set; }
        public bool IsOverWrite { get; set; }
        public int? TempBus { get; set; }
        public DateTime? TempBusStartTime { get; set; }
        public DateTime? TempBusEndTime { get; set; }
        public Decimal? PayRate { get; set; }
        public string? RunType { get; set; }

        //public int DefaultRouteId { get; set; }

    }
}
