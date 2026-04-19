using Application.Abstraction.Services;
using DTO.Response.Students;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Students.Queries.GetFamilyChargesForDropdown
{
    public class GetFamilyChargesForDropdownQueryHandler: IRequestHandler<GetFamilyChargesForDropdownQuery, List<ChargeDropdownDto>>
    {
        private readonly IStudentsService _studentsService;

        public GetFamilyChargesForDropdownQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        public async Task<List<ChargeDropdownDto>> Handle(
            GetFamilyChargesForDropdownQuery request,
            CancellationToken cancellationToken)
        {
            return await _studentsService.GetFamilyChargesForDropdown(request.FamilyId);
        }
    }
}
