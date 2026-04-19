using DTO.Response.Bus;
using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Response.Admin;

namespace Application.Handler.Admin.Queries.GetPredefinedSMSMessages
{
    public class GetPredefinedSMSMessagesQuery : IRequest<CommonResultResponseDto<IList<GetPredefinedSMSMessagesResponseDto>>>
    {
    }
}
