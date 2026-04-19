using DTO.Response;
using MediatR;

namespace Application.Handler.Students.Command.UpdateStudentsIndex
{
    public class UpdateStudentsIndexCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int StudentId { get; set; }
        public int RowNumber { get; set; }
    }
}
