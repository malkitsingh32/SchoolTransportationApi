using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.DeletePredefinedColor
{
    public class DeletePredefinedColorCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
    }
}
