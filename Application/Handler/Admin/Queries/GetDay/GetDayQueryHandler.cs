using Application.Abstraction.Services;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetDay
{
    public class GetDayQueryHandler : IRequestHandler<GetDayQuery, List<GetDaysResponseDto>>
    {
        private readonly IAdminService _service;

        public GetDayQueryHandler(IAdminService service)
        {
            _service = service;
        }

        public async Task<List<GetDaysResponseDto>> Handle(GetDayQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetDay();
        }
    }
}
