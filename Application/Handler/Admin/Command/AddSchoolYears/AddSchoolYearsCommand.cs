using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddSchoolYears
{
    public class AddSchoolYearsCommand :IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public int SchoolYear { get; set; }
        public int NumberOfStudents { get; set; }
        public int UserId { get; set; }
    }
}
