using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddUpdateGrade
{
    public class AddUpdateGradeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string GradeName { get; set; }
        public int UserId { get; set; }
        public int Gender { get; set; }
        public List<int>? SchoolId { get; set; }

    }
}
