using DTO.Response;
using MediatR;

namespace Application.Handler.Admin.Command.AddChargeStructure
{
    public class AddChargeStructureCommand : IRequest<CommonResultResponseDto<string>>
    {
        public int Id { get; set; }
        public int DistrictId { get; set; }
        public int? NtId { get; set; }
        public bool IsFunded { get; set; }
        public int UseId { get; set; }
        public decimal Price { get; set; }
        public int? SchoolId { get; set; }
    }
}
