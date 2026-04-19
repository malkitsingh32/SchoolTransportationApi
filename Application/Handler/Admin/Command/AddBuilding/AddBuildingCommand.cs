using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddBuilding
{
    public class AddBuildingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
        public string? BuildingName { get; set; }
    }
}
