using Application.Abstraction.Services;
using DTO.Request.Students;
using DTO.Response;
using DTO.Response.Students;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Command.ImportBulkStudents
{
    public class ImportBulkStudentsCommandHandler : IRequestHandler<ImportBulkStudentsCommand, CommonResultResponseDto<ImportStudentsResult>>
    {
        private readonly IStudentsService _studentsService;
        public ImportBulkStudentsCommandHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<CommonResultResponseDto<ImportStudentsResult>> Handle(ImportBulkStudentsCommand importBulkStudentsCommand, CancellationToken cancellationToken)
        {
            var students = await _studentsService.ImportBulkStudents(importBulkStudentsCommand.Adapt<ImportBulkStudentsRequestDto>());
            return await Task.FromResult(students);
        }
    }
}
