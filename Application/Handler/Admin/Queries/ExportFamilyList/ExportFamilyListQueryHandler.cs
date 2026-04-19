using Application.Abstraction.Services;
using Application.Handler.Students.Queries.ExportStudentsList;
using MediatR;

namespace Application.Handler.Admin.Queries.ExportFamilyList
{
    public class ExportFamilyListQueryHandler : IRequestHandler<ExportFamilyListQuery, ExportFileResult>
    {
        private readonly IAdminService _adminService;
        public ExportFamilyListQueryHandler(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public async Task<ExportFileResult> Handle(ExportFamilyListQuery request, CancellationToken cancellationToken)
        {
            return await _adminService.ExportFamilyList();
        }
    }
}
