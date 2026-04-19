using DTO.Response;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Handler.Admin.Command.UpdateChargeStructure
{
    public class UpdateChargeStructureCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public bool IsFunded { get; set; }
    }
}

