using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Admin.Command.AddUpdatePredefinedColor
{
    public class AddUpdatePredefinedColorCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
    }
}
