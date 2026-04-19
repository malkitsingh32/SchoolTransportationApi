using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSearchLocation
{
    public class AddSearchLocationCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string CurrentLocation { get; set; }
        public string CurrentLocationLongLat { get; set; }
        public int UserId { get; set; }
    }
}

