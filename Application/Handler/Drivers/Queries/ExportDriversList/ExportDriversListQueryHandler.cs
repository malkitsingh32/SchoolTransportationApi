using Application.Abstraction.Services;
using Application.Handler.Students.Queries.ExportStudentsList;
using MediatR;

namespace Application.Handler.Drivers.Queries.ExportDriversList
{
    public class ExportDriversListQueryHandler : IRequestHandler<ExportDriversListQuery, ExportFileResult>
    {
        private readonly IDriversService _driversService;
        public ExportDriversListQueryHandler(IDriversService driversService)
        {
            _driversService = driversService;
        }
        public async Task<ExportFileResult> Handle(ExportDriversListQuery request, CancellationToken cancellationToken)
        {
            return await _driversService.ExportDriversList();
        }
    }
}
