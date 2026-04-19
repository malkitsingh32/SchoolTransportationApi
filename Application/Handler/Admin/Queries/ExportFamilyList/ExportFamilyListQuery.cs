using Application.Handler.Students.Queries.ExportStudentsList;
using MediatR;

namespace Application.Handler.Admin.Queries.ExportFamilyList
{
    public class ExportFamilyListQuery : IRequest<ExportFileResult>
    {
    }
}
