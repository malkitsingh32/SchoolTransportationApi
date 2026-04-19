using DTO.Response;
using MediatR;

namespace Application.Handler.Streets.Command.DeleteStreet
{
    public class DeleteStreetsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
    
}
