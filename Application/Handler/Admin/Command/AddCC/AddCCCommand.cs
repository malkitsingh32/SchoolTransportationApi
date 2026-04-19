using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddCC
{
    public class AddCCCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public int CardnoxId { get; set; }
        public int Family { get; set; }
        public int UserId { get; set; }
    }
}
