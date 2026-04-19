using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using MediatR;

namespace Application.Handler.Students.Command.ImportBulkStudents
{
    public class ImportBulkStudentsCommand : IRequest<CommonResultResponseDto<ImportStudentsResult>>
    {
        public List<ImportBulkStudentsListDto> ImportBulkStudentsLists { get; set; }
    }
}
