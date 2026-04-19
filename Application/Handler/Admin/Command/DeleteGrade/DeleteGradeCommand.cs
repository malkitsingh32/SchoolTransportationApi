using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.DeleteGrade
{
    public class DeleteGradeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsDelete { get; set; }
    }
}
