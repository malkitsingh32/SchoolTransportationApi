using Application.Handler.Students.Queries.ExportStudentsList;
using MediatR;

namespace Application.Handler.Drivers.Queries.ExportDriversList
{
    public class ExportDriversListQuery : IRequest<ExportFileResult>
    {
    }
}
