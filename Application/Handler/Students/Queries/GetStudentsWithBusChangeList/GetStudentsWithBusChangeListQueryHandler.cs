using Application.Abstraction.Services;
using DTO.Response.Students;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Students.Queries.GetStudentsWithBusChangeList
{
    public class GetStudentsWithBusChangeListQueryHandler : IRequestHandler<GetStudentsWithBusChangeListQuery, CommonResultResponseDto<IList<GetStudentsResponseDto>>>
    {
        private readonly IStudentsService _studentsService;

        public GetStudentsWithBusChangeListQueryHandler(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        public async Task<CommonResultResponseDto<IList<GetStudentsResponseDto>>> Handle(GetStudentsWithBusChangeListQuery request, CancellationToken cancellationToken)
        {
            return await _studentsService.GetStudentsWithBusChangeList();
        }
    }
}
