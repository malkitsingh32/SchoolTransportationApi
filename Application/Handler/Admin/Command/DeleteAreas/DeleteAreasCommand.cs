using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteAreas
{
    public class DeleteAreasCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }

    }
}
