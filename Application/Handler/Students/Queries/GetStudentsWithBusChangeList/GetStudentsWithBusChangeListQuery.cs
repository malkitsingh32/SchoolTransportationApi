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
    public class GetStudentsWithBusChangeListQuery : IRequest<CommonResultResponseDto<IList<GetStudentsResponseDto>>>
    {
    }
}
