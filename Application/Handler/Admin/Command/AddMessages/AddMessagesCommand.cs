using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.AddMessages
{
    public class AddMessagesCommand : IRequest<CommonResultResponseDto<string>>
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public int CreatedBy { get; set; }
    }
}
