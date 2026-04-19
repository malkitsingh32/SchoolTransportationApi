using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddNT
{
    public class AddNTCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string NTName { get; set; }
        public int UserId { get; set; }
    }
}
