using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteDistrict
{
    public class DeleteDistrictCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
