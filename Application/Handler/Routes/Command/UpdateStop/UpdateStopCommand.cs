using DTO.Response;
using MediatR;

namespace Application.Handler.Routes.Command.UpdateStop
{
    public class UpdateStopCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int? RouteId { get; set; }
        public int StudentId { get; set; }
        public int? RowNumber { get; set; }
        public string? UniqueId { get; set; }
    }
}
