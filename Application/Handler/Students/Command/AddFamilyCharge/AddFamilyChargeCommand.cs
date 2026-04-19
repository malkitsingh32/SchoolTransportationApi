using DTO.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handler.Students.Command.AddFamilyCharge
{
    public class AddFamilyChargeCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int FamilyId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
    }
}
