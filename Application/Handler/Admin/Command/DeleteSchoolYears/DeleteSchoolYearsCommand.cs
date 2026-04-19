using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteSchoolYears
{
    public class DeleteSchoolYearsCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}
