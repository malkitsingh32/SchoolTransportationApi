using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDistrict
{
    public class AddDistrictCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int UserId { get; set; }
    }
}
