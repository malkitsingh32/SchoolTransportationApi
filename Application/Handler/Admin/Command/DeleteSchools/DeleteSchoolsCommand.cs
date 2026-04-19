using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSchools
{
    public class DeleteSchoolsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }

    }
}
