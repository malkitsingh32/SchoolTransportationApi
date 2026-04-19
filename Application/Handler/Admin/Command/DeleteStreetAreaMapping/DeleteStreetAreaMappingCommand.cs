using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteStreetAreaMapping
{
    public class DeleteStreetAreaMappingCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}
