using DTO.Response.Students;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Students.Queries.GetFamilyChargesForDropdown
{
    public class GetFamilyChargesForDropdownQuery : IRequest<List<ChargeDropdownDto>>
    {
        public int FamilyId { get; set; }
    }
}
