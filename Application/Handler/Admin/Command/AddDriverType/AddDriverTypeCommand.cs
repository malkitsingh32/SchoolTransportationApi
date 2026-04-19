using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddDriverType
{
    public class AddDriverTypeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string DriverType { get; set; }
        public decimal PayRate { get; set; }
        public int UserId { get; set; }
    }
}
