using Application.Common.Dtos;
using Application.Common.Response;
using DTO.Response;
using DTO.Response.Admin;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Queries.GetMessages
{
    public class GetMessagesQuery : ServerRowsRequest, IRequest<CommonResultResponseDto<PaginatedList<GetMessageResponseDto>>>
    {
        public ServerRowsRequest CommonRequest { get; set; }
    }
}
