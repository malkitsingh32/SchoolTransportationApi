using DTO.Response;
using MediatR;

namespace Application.Handler.Streets.Command.AddUpdateStreets
{
    public class AddUpdateStreetsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int ID { get; set; }
        public string StreetName { get; set; }
        public int? Area { get; set; }
        public int? FromNumber { get; set; }
        public int? ToNumber { get; set; }
        public int? NumberOfRoutes { get; set; }
        public int UserId { get; set; }
        public int? RouteId { get; set; }
        public bool IsDelete { get; set; }
        public bool AddStreet { get; set; }
    }
}
