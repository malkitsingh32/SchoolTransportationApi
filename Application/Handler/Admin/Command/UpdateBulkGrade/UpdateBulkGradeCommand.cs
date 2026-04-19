using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.UpdateBulkGrade
{
    public class UpdateBulkGradeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int GenderId { get; set; }
        public int SchoolId { get; set; }
    }
}
