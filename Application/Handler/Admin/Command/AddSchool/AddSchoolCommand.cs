using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSchool
{
    public class AddSchoolCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string LegalName { get; set; }
        public int UserId { get; set; }
    }
}
