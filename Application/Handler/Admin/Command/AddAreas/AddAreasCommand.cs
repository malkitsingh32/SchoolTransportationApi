using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddAreas
{
    public class AddAreasCommand : IRequest<CommonResultResponseDto<string>>
    {
        public string AreaName { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public string ShortName { get; set; }
    }
}
