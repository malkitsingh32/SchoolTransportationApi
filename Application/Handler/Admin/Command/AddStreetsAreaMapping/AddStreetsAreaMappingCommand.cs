using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddStreetsAreaMapping
{
    public class AddStreetsAreaMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int AreaId { get; set; }
        public int UserId { get; set; }
        public int DistrictId { get; set; }

    }
}
