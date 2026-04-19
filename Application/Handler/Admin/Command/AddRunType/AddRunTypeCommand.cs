using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddRunType
{
    public class AddRunTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string RunType { get; set; }
    }
}
