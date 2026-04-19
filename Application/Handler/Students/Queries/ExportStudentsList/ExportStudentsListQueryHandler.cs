using Application.Abstraction.Services;
using Mapster;
using MediatR;

namespace Application.Handler.Students.Queries.ExportStudentsList
{
    public class ExportStudentsListQueryHandler : IRequestHandler<ExportStudentsListQuery, ExportFileResult>
    {
        private readonly IStudentsService _studentsService;
        public ExportStudentsListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<ExportFileResult> Handle(ExportStudentsListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.ExportStudentsList(request.Adapt<ExportStudentsListQuery>());

        }
    }

    public class ExportFileResult
    {
        public byte[] FileBytes { get; set; }
        public string FileName { get; set; }
    }

}
