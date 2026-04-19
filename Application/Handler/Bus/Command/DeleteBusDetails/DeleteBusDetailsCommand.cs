using DTO.Response;
using MediatR;


namespace Application.Handler.Bus.Command.DeleteBusDetails
{
    public class DeleteBusDetailsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}
